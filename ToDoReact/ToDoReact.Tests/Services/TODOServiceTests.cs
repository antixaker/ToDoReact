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
        [Test]
        public void GetAll_GotEmptyList_Success()
        {
            // Arrange
            var todoService = CreateToDoService();
            var emptyList = new List<TODOModel>();

            // Act
            var todosList = todoService.GetAll();

            // Assert
            Assert.AreEqual(emptyList, todosList);
        }

        [Test]
        public void Add_AddTODOItem_ListContainsAddedItem()
        {
            // Arrange
            var todoService = CreateToDoService();
            var todoItem = CreateToDoItem();

            // Act
            todoService.Add(todoItem);

            // Assert
            Assert.IsTrue(todoService.GetAll().Contains(todoItem));
        }

        [Test]
        public void DeleteItem_DeleteExistTODOFromList_ListDoesntContainItem()
        {
            // Arrange
            var todoService = CreateToDoService();
            var todoItem = CreateToDoItem();
            todoService.Add(todoItem);

            // Act
            todoService.DeleteItem(todoItem);

            // Assert
            Assert.IsFalse(todoService.GetAll().Contains(todoItem));
        }

        [Test]
        public void DeleteItem_TryDeleteNotExistItem_NoException()
        {
            // Arrange
            var todoService = CreateToDoService();
            var item = CreateToDoItem();

            // Act
            todoService.DeleteItem(item);

            // Assert
            Assert.IsTrue(true);
        }

        private TODOModel CreateToDoItem()
        {
            return new TODOModel(DateTime.MinValue, string.Empty, string.Empty);
        }

        private ITODOService CreateToDoService()
        {
            return new TODOService();
        }
    }
}

