using System;
using NUnit.Framework;
using Moq;
using ToDoReact.Services;
using ToDoReact.Models;
using FreshMvvm;
using ViewModels;
using System.Collections.ObjectModel;

namespace ToDoReact.Tests
{
    [TestFixture]
    public class MainViewModelTests
    {
        [SetUp]
        public void SetUp()
        {
            _mockTODOService = new Mock<ITODOService>();
            _mainViewModel = new MainViewModel(_mockTODOService.Object);
        }

        private TODOModel _testModel = new TODOModel(DateTime.Now, string.Empty, string.Empty);
        private MainViewModel _mainViewModel;
        private Mock<ITODOService> _mockTODOService;
        private Mock<IPageModelCoreMethods> _coreMethods;

        private void ArrangeTestWithEmptyList()
        {
            _mockTODOService.Setup(x => x.GetAll()).Returns(new ObservableCollection<TODOModel>());
            _mainViewModel.Init();
        }

        private ObservableCollection<TODOModel> ArrangeTestWithNotEmptyList()
        {
            var collection = new ObservableCollection<TODOModel> { _testModel, _testModel };
            _mockTODOService.Setup(x => x.GetAll()).Returns(collection);
            _mainViewModel.Init();
            return collection;
        }

        [Test]
        public void MainViewModel_HaveNotNullListAfterCreation_True()
        {
            // Arrange
            ArrangeTestWithEmptyList();

            // Act
            // Assert
            Assert.IsNotNull(_mainViewModel.Items);
        }

        [Test]
        public void Items_ValuesAsInTodoServiceGetAll_True()
        {
            // Arrange
            var collection = ArrangeTestWithNotEmptyList();

            // Act
            // Assert
            Assert.AreEqual(collection, _mainViewModel.Items.Value);
        }

        [Test]
        public void AddTODOCommand_RedirectToTargetView_Success()
        {
            // Arrange
            _coreMethods = new Mock<IPageModelCoreMethods>();
            _mainViewModel.CoreMethods = _coreMethods.Object;

            // Act
            _mainViewModel.AddTODOCommand.Execute();

            // Assert
            _coreMethods.Verify(x => x.PushPageModel<AddTODOViewModel>(true), Times.Once());
        }

        [Test]
        public void EditTODOCommand_RedirectToTargetView_Success()
        {
            // Arrange
            _coreMethods = new Mock<IPageModelCoreMethods>();
            _mainViewModel.CoreMethods = _coreMethods.Object;
            var fakeModel = It.IsAny<TODOModel>();

            // Act
            _mainViewModel.EditTODOCommand.Execute();

            // Assert
            _coreMethods.Verify(x => x.PushPageModel<EditTODOViewModel>(fakeModel, false, true), Times.Once());
        }

        [Test]
        public void ItemsAreEmpty_AfterPageCreation_IsTrue()
        {
            // Arrange
            ArrangeTestWithEmptyList();

            // Act
            // Assert
            Assert.IsTrue(_mainViewModel.ItemsAreEmpty.Value);
        }

        [Test]
        public void ItemsAreEmpty_AfterItemAdd_IsFalse()
        {
            // Arrange
            ArrangeTestWithNotEmptyList();

            // Act
            // Assert
            Assert.IsFalse(_mainViewModel.ItemsAreEmpty.Value);
        }
    }
}

