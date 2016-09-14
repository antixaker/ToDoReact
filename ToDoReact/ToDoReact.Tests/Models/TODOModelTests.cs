using System;
using NUnit.Framework;
using ToDoReact.Models;

namespace ToDoReact.Tests
{
    [TestFixture]
    public class TODOModelTests
    {
        [Test]
        public void TODOModel_SetValuesInCtor_SameValuesInProperties()
        {
            // Arrange
            var date = DateTime.MinValue;
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
        [TestCase("")]
        [TestCase("  ")]
        public void TODOModel_SetDecsriptionEmptyOrWhiteSpace_ItemHasDefaultDescription(string description)
        {
            // Arrange
            var date = DateTime.MinValue;
            var title = "StudyTDD";
            var descriptionDefaultValue = "Empty";

            // Act
            var item = new TODOModel(date, title, description);

            // Assert
            Assert.AreEqual(date, item.Date);
            Assert.AreEqual(title, item.Title);
            Assert.AreEqual(descriptionDefaultValue, item.Description);
        }
    }
}

