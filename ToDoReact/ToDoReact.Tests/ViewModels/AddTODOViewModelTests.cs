using System;
using NUnit.Framework;
using Moq;
using ToDoReact.Services;
using ToDoReact.Models;
using System.Linq;
using System.Collections.ObjectModel;
using FreshMvvm;
using ToDoReact;

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
            var date = new DateTime(2016, 1, 1);

            var model = new TODOModel(date, title, description);
            _mockTimeProvider.Setup(t => t.CurrentTime).Returns(date);
            _mockTodoService.Setup(x => x.GetAll()).Returns(new ObservableCollection<TODOModel>() { model });

            _addTodoViewModel.Title.Value = title;
            _addTodoViewModel.Description.Value = description;
            InitializeCoreMethods();
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
            _mockTodoService.Setup(x => x.GetAll()).Returns(new ObservableCollection<TODOModel>() { model });

            _addTodoViewModel.Title.Value = title;
            InitializeCoreMethods();
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

