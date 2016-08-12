using System;
using NUnit.Framework;
using ToDoReact.Models;

namespace ToDoReact.Tests
{
    [TestFixture]
    public class TODOModelTests
    {
        [Test]
        public void TODOModel_ItemContainsDateTitleDescription_True()
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
        public void TODOModel_CanCreateItemWithoutDescription_Success()
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

        [Test]
        public void TODOModel_LeftDecsriptionEmpty_ItemHasDefaultDescription()
        {
            // Arrange
            var date = DateTime.Now;
            var title = "StudyTDD";
            var description = string.Empty;
            // Act
            var item = new TODOModel(date, title, description);
            // Assert
            Assert.AreEqual(date, item.Date);
            Assert.AreEqual(title, item.Title);
            Assert.AreEqual("Empty", item.Description);
        }

        [Test]
        public void TODOModel_AddToDescriptionWhiteSpaces_ItemHasDefaultDescription()
        {
            // Arrange
            var date = DateTime.Now;
            var title = "StudyTDD";
            var description = "  ";
            // Act
            var item = new TODOModel(date, title, description);
            // Assert
            Assert.AreEqual(date, item.Date);
            Assert.AreEqual(title, item.Title);
            Assert.AreEqual("Empty", item.Description);
        }

    }
}

