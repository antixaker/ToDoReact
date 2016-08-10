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
            _mockTODOService.Setup(x => x.GetAll()).Returns(new List<TODOModel>() { _testModel, _testModel });
            _mainViewModel = new MainViewModel(_mockTODOService.Object);
        }

        private TODOModel _testModel = new TODOModel();
        private MainViewModel _mainViewModel;
        private Mock<ITODOService> _mockTODOService = new Mock<ITODOService>();

        [Test]
        public void MainViewModel_HaveNotNullListAfterCreation_True()
        {
            // Arrange
            // Act
            // Assert
            Assert.IsNotNull(_mainViewModel.Items);
        }

        [Test]
        public void Items_ValuesAsInTodoServiceGetAll_True()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(new List<TODOModel>() { _testModel, _testModel }, _mainViewModel.Items.Value);
        }
    }
}

