using System;
using NUnit.Framework;
using Moq;
using ToDoReact.Services;
using ToDoReact.Models;
using System.Collections.Generic;

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
        public void Item_ItemContainsDateTitleDescription_True()
        {
            // Arrange
            var date = DateTime.Now;
            var title = "StudyTDD";
            var description = "TDD is very interesting and usefull thing.";
            // Act
            var item = new TODOModel(date, title, description);
            // Assert
            Assert.AreEqual(date, item.Date);
            Assert.AreEqual(title, item.Title);
            Assert.AreEqual(description, item.Description);
        }

        [Test]
        public void Item_CanCreateItemWithoutDescription_Success()
        {
            // Arrange
            var date = DateTime.Now;
            var title = "StudyTDD";
            // Act
            var item = new TODOModel(date, title);
            // Assert
            Assert.AreEqual(date, item.Date);
            Assert.AreEqual(title, item.Title);
            Assert.AreEqual("Empty", item.Description);
        }
    }
}

