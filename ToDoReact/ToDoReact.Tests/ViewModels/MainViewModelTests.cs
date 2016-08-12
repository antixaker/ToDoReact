using System;
using NUnit.Framework;
using Moq;
using ToDoReact.Services;
using ToDoReact.Models;
using System.Collections.Generic;
using FreshMvvm;
using ViewModels;

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
            _mockTODOService.Setup(x => x.GetAll()).Returns(new List<TODOModel>());
            _mainViewModel.Init();
        }

        private List<TODOModel> ArrangeTestWithNotEmptyList()
        {
            var collection = new List<TODOModel> { _testModel, _testModel };
            _mockTODOService.Setup(x => x.GetAll()).Returns(collection);
            _mainViewModel.Init();
            return collection;
        }

        private void SetMockedCoreMethodsToViewModel()
        {
            _coreMethods = new Mock<IPageModelCoreMethods>();
            _mainViewModel.CoreMethods = _coreMethods.Object;
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
            List<TODOModel> collection = ArrangeTestWithNotEmptyList();

            // Act
            // Assert
            Assert.AreEqual(collection, _mainViewModel.Items.Value);
        }

        [Test]
        public void ItemsAreEmpty_ItemsCountZero_PropertySetTrue()
        {
            // Arrange
            ArrangeTestWithEmptyList();
            // Act
            // Assert
            Assert.IsTrue(_mainViewModel.ItemsAreEmpty.Value);
        }

        [Test]
        public void ItemsAreEmpty_PropertyFalseAfterAddingToList_True()
        {
            // Arrange
            ArrangeTestWithNotEmptyList();
            // Act
            // Assert
            Assert.IsFalse(_mainViewModel.ItemsAreEmpty.Value);
        }

        [Test]
        public void AddTODOCommand_RedirectToTargetView_Success()
        {
            // Arrange
            SetMockedCoreMethodsToViewModel();
            // Act
            _mainViewModel.AddTODOCommand.Execute();
            // Assert
            _coreMethods.Verify(x => x.PushPageModel<AddTODOViewModel>(true), Times.Once());
        }

        [Test]
        public void EditTODOCommand_RedirectToTargetView_Success()
        {
            // Arrange
            SetMockedCoreMethodsToViewModel();
            var fakeModel = It.IsAny<TODOModel>();
            // Act
            _mainViewModel.EditTODOCommand.Execute();
            // Assert
            _coreMethods.Verify(x => x.PushPageModel<EditTODOViewModel>(fakeModel, false, true), Times.Once());
        }
    }
}

