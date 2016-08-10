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
            _mainViewModel = new MainViewModel(_mockTODOService.Object);
        }

        private TODOModel _testModel = new TODOModel();
        private MainViewModel _mainViewModel;
        private Mock<ITODOService> _mockTODOService = new Mock<ITODOService>();

        [Test]
        public void MainViewModel_HaveNotNullListAfterCreation_True()
        {
            // Arrange
            _mainViewModel.Init();
            // Act
            // Assert
            Assert.IsNotNull(_mainViewModel.Items);
        }

        [Test]
        public void Items_ValuesAsInTodoServiceGetAll_True()
        {
            // Arrange
            var collection = new List<TODOModel> { _testModel, _testModel };
            _mockTODOService.Setup(x => x.GetAll()).Returns(collection);
            _mainViewModel.Init();
            //var vm = new MainViewModel(_mockTODOService.Object);

            // Act
            // Assert
            Assert.AreEqual(collection, _mainViewModel.Items.Value);
        }

        [Test]
        public void ItemsAreEmpty_PropertySetTrueWhenItemsCountZero_True()
        {
            // Arrange
            _mainViewModel.Init();
            // Act
            // Assert
            Assert.IsTrue(_mainViewModel.ItemsAreEmpty.Value);
        }
    }
}

