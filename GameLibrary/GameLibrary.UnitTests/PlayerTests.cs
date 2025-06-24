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
        sut.JoinDate.Should().BeCloseTo(currentDate, TimeSpan.FromMilliseconds(500));
        sut.JoinDate.Should().BeWithin(TimeSpan.FromMilliseconds(500)).Before(currentDate);
    }

    [Fact]
    public void AddItemToInventory_WithValidItem_AddsTheItem()
    {
        // Arrange
        var sut = new Player("Alice", 1, DateTime.Now);
        var item = new InventoryItem(101, "Sword", "A sharp blade.");

        // Act
        sut.AddItemToInventory(item);

        // Assert
        sut.InventoryItems.Should().HaveCount(1);
        sut.InventoryItems.Should().NotBeEmpty();
        sut.InventoryItems.Should().Contain(item);
        sut.InventoryItems.Should().ContainSingle(item => item.Id == 101 && item.Name == "Sword");
    }

    [Fact]
    public void Greet_WithNullOrEmptyGreeting_ThrowsArgumentException()
    {
        // Arrange
        var sut = new Player("Alice", 1, DateTime.Now);

        // Act
        Action act = () => sut.Greet("");

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void IncreaseLevel_WhenCalled_RaisesLevelUpEvent()
    {
        // Arrange
        var sut = new Player("Alice", 1, DateTime.Now);
        using var monitoredSut = sut.Monitor();

        // Act
        sut.IncreaseLevel();

        // Assert
        monitoredSut.Should().Raise(nameof(sut.LevelUp));
    }
}
