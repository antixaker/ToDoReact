using System;
using System.Threading.Tasks;
using FreshMvvm;
using Moq;
using NUnit.Framework;
using ToDoReact;
using ToDoReact.Models;
using ToDoReact.Services;

namespace ViewModels
{
    [TestFixture]
    public class EditDOTOViewModelTests
    {
        [SetUp]
        public void SetUp()
        {
            _model = CreateTODOModel();
            _todoService = new Mock<ITODOService>();

            _editVM = new EditTODOViewModel(_todoService.Object);
            _editVM.Init(_model);
        }

        private Mock<ITODOService> _todoService;
        private Mock<IPageModelCoreMethods> _coreMethods;
        private EditTODOViewModel _editVM;
        private TODOModel _model;

        [Test]
        public void EditTODOViewModel_PropertiesFilledAfterCreationVM_True()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(_model.Date, _editVM.CreationDate.Value);
            Assert.AreEqual(_model.Title, _editVM.Title.Value);
            Assert.AreEqual(_model.Description, _editVM.Description.Value);
        }

        [Test]
        public void SaveChangesCommand_ItemChangesSavedToService_True()
        {
            // Arrange
            var newTitle = "New title";
            _editVM.Title.Value = newTitle;
            InitializeCoreMethods();

            // Act
            _editVM.SaveChangesCommand.Execute();

            // Assert
            Assert.AreEqual(newTitle, _model.Title);
        }

        [Test]
        public void Title_SetEmpty_CannotSaveChanges()
        {
            // Arrange
            // Act
            _editVM.Title.Value = string.Empty;

            // Assert
            Assert.IsFalse(_editVM.SaveChangesCommand.CanExecute());
        }

        [Test]
        public void Completed_ItemWillRemoveAfterSave_True()
        {
            // Arrange
            InitializeCoreMethods();

            // Act
            _editVM.IsCompleted.Value = true;
            _editVM.SaveChangesCommand.Execute();

            // Assert
            _todoService.Verify(x => x.DeleteItem(_model), Times.Once());
        }

        [Test]
        public void DeleteItemCommand_PromptWasShowed_True()
        {
            // Arrange
            InitializeCoreMethods();

            // Act
            _editVM.DeleteItemCommand.Execute();

            // Assert
            _coreMethods.Verify(x => x.DisplayAlert(string.Empty, "Are you sure?", "Yes", "No"), Times.Once());
        }

        [Test]
        public void DeleteItemCommand_AcceptDeletion_ItemDeleted()
        {
            // Arrange
            InitializeCoreMethods();
            _coreMethods.Setup(x => x.DisplayAlert(string.Empty, "Are you sure?", "Yes", "No")).Returns(Task.FromResult(true));

            // Act
            _editVM.DeleteItemCommand.Execute();

            // Assert
            _todoService.Verify(x => x.DeleteItem(_model), Times.Once());
        }

        [Test]
        public void DeleteItemCommand_AcceptDeletion_RedirectToMainPage()
        {
            // Arrange
            InitializeCoreMethods();
            _coreMethods.Setup(x => x.DisplayAlert(string.Empty, "Are you sure?", "Yes", "No")).Returns(Task.FromResult(true));

            // Act
            _editVM.DeleteItemCommand.Execute();

            // Assert
            _coreMethods.Verify(x => x.PushPageModel<MainViewModel>(true), Times.Once);
        }

        [Test]
        public void DeleteItemCommand_DeclineDeletion_ItemNotDeleted()
        {
            // Arrange
            InitializeCoreMethods();
            _coreMethods.Setup(x => x.DisplayAlert(string.Empty, "Are you sure?", "Yes", "No")).Returns(Task.FromResult(false));

            // Act
            _editVM.DeleteItemCommand.Execute();

            // Assert
            _todoService.Verify(x => x.DeleteItem(_model), Times.Never());
        }

        [Test]
        public void DeleteItemCommand_DeclineDeletion_NoRedirections()
        {
            // Arrange
            InitializeCoreMethods();
            _coreMethods.Setup(x => x.DisplayAlert(string.Empty, "Are you sure?", "Yes", "No")).Returns(Task.FromResult(false));

            // Act
            _editVM.DeleteItemCommand.Execute();

            // Assert
            _coreMethods.Verify(x => x.PushPageModel<MainViewModel>(true), Times.Never);
        }


        [Test]
        public void SaveChangesCommand_RedirectToMainPageAfterSave_True()
        {
            // Arrange
            InitializeCoreMethods();

            // Act
            _editVM.SaveChangesCommand.Execute();

            // Assert
            _coreMethods.Verify(x => x.PushPageModel<MainViewModel>(true), Times.Once);
        }

        private void InitializeCoreMethods()
        {
            _coreMethods = new Mock<IPageModelCoreMethods>();
            _editVM.CoreMethods = _coreMethods.Object;
        }

        private TODOModel CreateTODOModel()
        {
            var date = DateTime.MinValue;
            var title = "Title";
            var description = string.Empty;

            return new TODOModel(date, title, description);
        }
    }
}

