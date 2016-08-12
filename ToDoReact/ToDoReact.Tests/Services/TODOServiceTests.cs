using NUnit.Framework;
using System;
using System.Collections.Generic;
using ToDoReact.Services;
using ToDoReact.Models;
using System.Linq;

namespace ToDoReact.Tests
{
    [TestFixture]
    public class TODOServiceTests
    {
        [SetUp]
        public void SetUp()
        {
            _todoService = new TODOService();
        }

        private ITODOService _todoService;

        private TODOModel GetTODOInstance()
        {
            return new TODOModel(DateTime.Now, string.Empty, string.Empty);
        }

        [Test]
        public void GetAll_GotEmptyList_Success()
        {
            // Arrange
            // Act
            var todosList = _todoService.GetAll();
            // Assert
            Assert.AreEqual(new List<TODOModel>(), todosList);
        }

        [Test]
        public void Add_AddTODOItem_ListNotEmptyAfterAdd()
        {
            // Arrange
            var todoItem = GetTODOInstance();
            // Act
            _todoService.Add(todoItem);
            // Assert
            Assert.IsTrue(_todoService.GetAll().Contains(todoItem));
        }

        [Test]
        public void DeleteItem_DeleteExistTODOFromList_ListDoesntContainItem()
        {
            // Arrange
            var todoItem = GetTODOInstance();
            _todoService.Add(todoItem);
            // Act
            _todoService.DeleteItem(todoItem);
            // Assert
            Assert.IsFalse(_todoService.GetAll().Contains(todoItem));
        }

        [Test]
        public void DeleteItem_TryDeleteNotExistItem_Success()
        {
            // Arrange
            var item = GetTODOInstance();
            // Act
            try
            {
                _todoService.DeleteItem(item);
            }
            // Assert
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
            Assert.IsTrue(true);
        }
    }
}

