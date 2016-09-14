using System;
using NUnit.Framework;
using Moq;
using ToDoReact.Services;

namespace Services
{
    [TestFixture]
    public class TimeProviderTests
    {
        [Test]
        public void CurrentTime_ReturnsValue_EqualToDateTimeNow()
        {
            // Arrange
            var timeProvider = new TimeProvider();
            var ticksInSecond = TimeSpan.TicksPerSecond;

            // Act
            var dateFromProvider = timeProvider.CurrentTime;
            var dateNow = DateTime.Now;
            var delta = Math.Abs(dateNow.Ticks - dateFromProvider.Ticks);

            // Assert
            Assert.Less(delta, ticksInSecond);
        }
    }
}

