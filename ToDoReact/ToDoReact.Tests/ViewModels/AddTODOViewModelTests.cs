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
    public class AddTODOViewModelTests
    {
        [SetUp]
        public void SetUp()
        {
            _mockTodoService = new Mock<ITODOService>();
            _mockTimeProvider = new Mock<ITimeProvider>();
            _addTodoViewModel = new AddTODOViewModel(_mockTodoService.Object, _mockTimeProvider.Object);
        }

        private AddTODOViewModel _addTodoViewModel;
        private Mock<ITODOService> _mockTodoService;
        private Mock<ITimeProvider> _mockTimeProvider;

        [Test]
        public void CreationDate_ReturnCurrentDate_SameAsDateTimeNow()
        {
            // Arrange
            var testDate = new DateTime(2016, 1, 1);
            _mockTimeProvider.Setup(t => t.CurrentTime).Returns(testDate);
            _addTodoViewModel = new AddTODOViewModel(_mockTodoService.Object, _mockTimeProvider.Object);
            // Act
            var dateFromVM = _addTodoViewModel.CreationDate;
            // Assert
            Assert.AreEqual(testDate, dateFromVM);
        }

        [Test]
        public void AddItemCommand_EnteredDataSaveToStorage_ItemSaved()
        {
            // Arrange
            // Act
            _addTodoViewModel.AddItemCommand.Execute();
            // Assert
            _mockTodoService.Verify(x => x.Add(It.IsAny<TODOModel>()), Times.Once());
        }

        [Test]
        public void AddItemCommand_AddedItemHasPropertiesFromViewModel_True()
        {
            // Arrange
            var title = "Sample title";
            var description = "Description";
            var date = new DateTime(2016, 1, 1);

            var model = new TODOModel(date, title, description);
            _mockTimeProvider.Setup(t => t.CurrentTime).Returns(date);
            _mockTodoService.Setup(x => x.GetAll()).Returns(new List<TODOModel>() { model });

            _addTodoViewModel.Title.Value = title;
            _addTodoViewModel.Description.Value = description;

            // Act
            _addTodoViewModel.AddItemCommand.Execute();
            var res = _mockTodoService.Object.GetAll();
            // Assert
            Assert.Contains(model, res);
        }

        [Test]
        public void AddItemCommand_LeftEmptyDescription_DefaultDescriptionExists()
        {
            // Arrange
            var title = "Sample title";
            var date = new DateTime(2016, 1, 1);

            var model = new TODOModel(date, title);
            _mockTimeProvider.Setup(t => t.CurrentTime).Returns(date);
            _mockTodoService.Setup(x => x.GetAll()).Returns(new List<TODOModel>() { model });

            _addTodoViewModel.Title.Value = title;

            // Act
            _addTodoViewModel.AddItemCommand.Execute();
            var res = _mockTodoService.Object.GetAll();
            // Assert
            Assert.AreEqual("Empty", res.FirstOrDefault().Description);
        }

        [Test]
        public void AddItemCommand_AddNoInfoOnPage_CanExecuteFalse()
        {
            // Arrange
            // Act
            // Assert
            Assert.IsFalse(_addTodoViewModel.AddItemCommand.CanExecute());
        }

        [Test]
        public void AddItemCommand_SetTitle_CommandCanExecute()
        {
            // Arrange
            _addTodoViewModel.Title.Value = "Title";
            // Act
            // Assert
            Assert.IsTrue(_addTodoViewModel.AddItemCommand.CanExecute());
        }


    }
}

