using System;
using NUnit.Framework;
using Moq;
using ToDoReact.Services;
using ToDoReact.Models;
using System.Collections.Generic;
using System.Linq;

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

            _editVM = new EditTODOViewModel(_todoService.Object, _model);
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
            _todoService.Setup(x => x.GetAll()).Returns(new List<TODOModel> { _model });
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
            _todoService.Setup(x => x.GetAll()).Returns(new List<TODOModel> { _model });
            // Act
            _editVM.Completed.Value = true;
            _editVM.SaveChangesCommand.Execute();
            // Assert
            _todoService.Verify(x => x.DeleteItem(_model), Times.Once());
        }

        [Test]
        public void DeleteItemCommand_InvokesOnce_True()
        {
            // Arrange
            // Act
            _editVM.DeleteItemCommand.Execute();
            // Assert
            _todoService.Verify(x => x.DeleteItem(_model), Times.Once());
        }
    }
}

