using NUnit.Framework;
using System;
using System.Collections.ObjectModel;
namespace ToDoReact.Tests
{
    [TestFixture()]
    public class TODOServiceTests
    {
        [Test]
        public void GetAll_GotEmptyList_Success()
        {
            // Arrange
            var todoService = new TODOService();

            // Act
            var todosList = todoService.GetAll();
            // Assert
            Assert.AreEqual(new List<TODOModel>(), todosList);
        }
    }
}

