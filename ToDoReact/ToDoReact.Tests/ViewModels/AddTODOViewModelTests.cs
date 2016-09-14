using System;
using FreshMvvm;
using Moq;
using NUnit.Framework;
using ToDoReact;
using ToDoReact.Models;
using ToDoReact.Services;

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
        private Mock<IPageModelCoreMethods> _coreMethods;

        [Test]
        public void CreationDate_ReturnsDate_SameValueAsFromTimeProvider()
        {
            // Arrange
            var testDate = DateTime.MinValue;
            _mockTimeProvider.Setup(t => t.CurrentTime).Returns(testDate);

            // Act
            var dateFromVM = _addTodoViewModel.CreationDate;

            // Assert
            Assert.AreEqual(testDate, dateFromVM);
        }

        [Test]
        public void AddItemCommand_EnteredDataSaveToStorage_ItemSaved()
        {
            // Arrange
            InitializeCoreMethods();

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
            var date = DateTime.MinValue;
            _mockTimeProvider.Setup(t => t.CurrentTime).Returns(date);
            _addTodoViewModel.Title.Value = title;
            _addTodoViewModel.Description.Value = description;
            InitializeCoreMethods();

            // Act
            _addTodoViewModel.AddItemCommand.Execute();

            // Assert
            _mockTodoService.Verify(f => f.Add(It.Is<TODOModel>((item) => item.Date.Equals(date) && item.Description.Equals(description) && item.Title.Equals(title))), Times.Once);
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

        [Test]
        public void AddItemCommand_RedirectToMainPageAfterSave_True()
        {
            // Arrange
            _addTodoViewModel.Title.Value = "Sample";
            InitializeCoreMethods();

            // Act
            _addTodoViewModel.AddItemCommand.Execute();

            // Assert
            _coreMethods.Verify(x => x.PushPageModel<MainViewModel>(true), Times.Once);
        }

        private void InitializeCoreMethods()
        {
            _coreMethods = new Mock<IPageModelCoreMethods>();
            _addTodoViewModel.CoreMethods = _coreMethods.Object;
        }
    }
}

