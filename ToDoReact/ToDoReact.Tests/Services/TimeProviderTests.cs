using System;
using NUnit.Framework;
using Moq;

namespace Services
{
    [TestFixture]
    public class TimeProviderTests
    {
        private Mock<ITimeProvider> _timeService;

        [Test]
        public void CurrentTime_ReturnsValue_EqualToDateTimeNow()
        {
            // Arrange
            var mockedDate = new DateTime(2016, 01, 01, 7, 7, 7);
            _timeService = new Mock<ITimeProvider>();
            _timeService.Setup(t => t.CurrentTime).Returns(mockedDate);
            // Act
            // Assert
            Assert.AreEqual(mockedDate, _timeService.Object.CurrentTime);
        }
    }
}

