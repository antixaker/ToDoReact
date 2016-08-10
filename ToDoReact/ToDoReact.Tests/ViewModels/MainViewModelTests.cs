using System;
using NUnit.Framework;

namespace ToDoReact.Tests
{
    [TestFixture]
    public class MainViewModelTests
    {
        [SetUp]
        public void SetUp()
        {
            _mainViewModel = new MainViewModel();
        }

        private MainViewModel _mainViewModel;

        [Test]
        public void MainViewModel_HaveNotNullListAfterCreating_True()
        {
            // Arrange
            // Act
            // Assert
            Assert.IsNotNull(_mainViewModel.Items);
        }
    }
}

