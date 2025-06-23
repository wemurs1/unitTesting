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
        Assert.Equal(2, sut.Level);
        Assert.InRange(sut.Level, 2, 100);
    }

    [Fact]
    public void Greet_ValidGreeting_ReturnsGreetingWithName()
    {
        // Arrange
        Player sut = new("Alice", 1, DateTime.Now);

        // Act
        var result = sut.Greet("Hello");

        // Assert
        Assert.Equal("Hello, Alice!", result);
        Assert.Contains("Alice", result);
        Assert.EndsWith("Alice!", result);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void Constructor_OnNewInstance_SetsJoinDate()
    {
        // Arrange
        var currentDate = DateTime.Now;

        // Act
        var sut = new Player("Alice", 1, currentDate);

        // Assert
        Assert.Equal(currentDate, sut.JoinDate);
    }
}
