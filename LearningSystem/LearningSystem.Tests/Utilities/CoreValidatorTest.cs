namespace LearningSystem.Tests.Utilities
{
    using System;
    using LearningSystem.Utilities.Common;
    using Xunit;

    public class CoreValidatorTest
    {
        [Fact]
        public void EnsureNotNull_WithNullObject_ShouldReturnException()
        {
            // Arrange && Act && Assert
            Assert.Throws<ArgumentNullException>(() => CoreValidator.EnsureNotNull(null));
        }

        [Fact]
        public void EnsureNotNull_WithNullObject_ShouldReturnExceptionMessage()
        {
            // Arrange && Act && Assert
            var exception = Assert.Throws<ArgumentNullException>(() => CoreValidator.EnsureNotNull(null, "Null message"));
            Assert.Contains("Null message", exception.Message);
        }

        [Fact]
        public void EnsureStringIsNotNullOrEmpty_WithNullString_ShouldThrowException()
        {
            // Arrange && Act && Assert
            Assert.Throws<ArgumentException>(() => CoreValidator.EnsureStringIsNotNullOrEmpty(null));
        }

        [Fact]
        public void EnsureStringIsNotNullOrEmpty_WithEmptyString_ShouldThrowException()
        {
            // Arrange
            var emptyString = string.Empty;

            // Act && Assert
            Assert.Throws<ArgumentException>(() => CoreValidator.EnsureStringIsNotNullOrEmpty(emptyString));
        }

        [Fact]
        public void EnsureNotNull_WithNullString_ShouldReturnExceptionMessage()
        {
            // Arrange && Act && Assert
            var exception = Assert.Throws<ArgumentException>(() => CoreValidator.EnsureStringIsNotNullOrEmpty(null, "Null message"));
            Assert.Contains("Null message", exception.Message);
        }
    }
}