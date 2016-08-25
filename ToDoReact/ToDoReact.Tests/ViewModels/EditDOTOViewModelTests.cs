using System;
using NUnit.Framework;
using Moq;
using ToDoReact.Services;
using ToDoReact.Models;
using System.Collections.Generic;
using System.Linq;
using FreshMvvm;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ViewModels
{
    [TestFixture]
    public class EditDOTOViewModelTests
    {
        [SetUp]
        public void SetUp()
        {
            var date = new DateTime(2016, 1, 1);
            var title = "Title";
            _model = new TODOModel(date, title);
            _todoService = new Mock<ITODOService>();

            _editVM = new EditTODOViewModel(_todoService.Object);
            _editVM.Init(_model);
        }

        private Mock<ITODOService> _todoService;
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
            _todoService.Setup(x => x.GetAll()).Returns(new ObservableCollection<TODOModel> { _model });
            var newTitle = "New title";
            _editVM.Title.Value = newTitle;
            // Act
            _editVM.SaveChangesCommand.Execute();
            // Assert
            Assert.IsTrue(_todoService.Object.GetAll().Where(x => x.Title == newTitle).Any());
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
            _todoService.Setup(x => x.GetAll()).Returns(new ObservableCollection<TODOModel> { _model });
            // Act
            _editVM.Completed.Value = true;
            _editVM.SaveChangesCommand.Execute();
            // Assert
            _todoService.Verify(x => x.DeleteItem(_model), Times.Once());
        }

        [Test]
        public void DeleteItemCommand_PromptWasShowed_True()
        {
            // Arrange
            var coreMethods = new Mock<IPageModelCoreMethods>();
            _editVM.CoreMethods = coreMethods.Object;
            // Act
            _editVM.DeleteItemCommand.Execute();
            // Assert
            coreMethods.Verify(x => x.DisplayAlert(string.Empty, "Are you sure?", "Yes", "No"), Times.Once());
        }

        [Test]
        public void DeleteItemCommand_AcceptDeletion_ItemDeleted()
        {
            // Arrange
            var coreMethods = new Mock<IPageModelCoreMethods>();
            coreMethods.Setup(x => x.DisplayAlert(string.Empty, "Are you sure?", "Yes", "No")).Returns(Task.FromResult(true));
            _editVM.CoreMethods = coreMethods.Object;

            // Act
            _editVM.DeleteItemCommand.Execute();
            // Assert
            _todoService.Verify(x => x.DeleteItem(_model), Times.Once());
        }

        [Test]
        public void DeleteItemCommand_DeclineDeletion_ItemNotDeleted()
        {
            // Arrange
            var coreMethods = new Mock<IPageModelCoreMethods>();
            coreMethods.Setup(x => x.DisplayAlert(string.Empty, "Are you sure?", "Yes", "No")).Returns(Task.FromResult(false));
            _editVM.CoreMethods = coreMethods.Object;

            // Act
            _editVM.DeleteItemCommand.Execute();
            // Assert
            _todoService.Verify(x => x.DeleteItem(_model), Times.Never());
        }


    }
}

