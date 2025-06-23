using FluentAssertions;

namespace GameLibrary.UnitTests;

public class PlayerTests
{
    [Fact]
    public void IncreaseLevel_WhenCalled_HasExpectedLevel()
    {
        // Arrange
        Player sut = new("Alice", 1, DateTime.Now);

        // Act
        sut.IncreaseLevel();

        // Assert
        // Assert.Equal(2, sut.Level);
        sut.Level.Should().Be(2);

        // Assert.InRange(sut.Level, 2, 100);
        sut.Level.Should().BeGreaterThan(1);
        sut.Level.Should().BeGreaterThanOrEqualTo(2);
        sut.Level.Should().BePositive();
        sut.Level.Should().NotBe(1);
        sut.Level.Should().BeInRange(2, 100);
    }

    [Fact]
    public void Greet_ValidGreeting_ReturnsGreetingWithName()
    {
        // Arrange
        Player sut = new("Alice", 1, DateTime.Now);

        // Act
        var result = sut.Greet("Hello");

        // Assert
        // Assert.Equal("Hello, Alice!", result);
        result.Should().Be("Hello, Alice!");

        // Assert.Contains("Alice", result);
        result.Should().Contain("Alice");

        // Assert.EndsWith("Alice!", result );
        result.Should().EndWith("Alice!");

        // Assert.NotNull(result);
        // Assert.NotEmpty(result);
        result.Should().NotBeNullOrEmpty();
        result.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void Constructor_OnNewInstance_SetsJoinDate()
    {
        // Arrange
        var currentDate = DateTime.Now;

        // Act
        var sut = new Player("Alice", 1, currentDate);

        // Assert
        // Assert.Equal(currentDate, sut.JoinDate);
        sut.JoinDate.Should().Be(currentDate);
    }
}
