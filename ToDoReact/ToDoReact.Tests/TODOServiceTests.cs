﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using ToDoReact.Services;
using ToDoReact.Models;

namespace ToDoReact.Tests
{
    [TestFixture]
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

