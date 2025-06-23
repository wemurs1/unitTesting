using Xunit.Abstractions;

namespace GameLibrary.UnitTests;

public class TreasureChestTests : IDisposable
{
    private readonly Stack<TreasureChest> chests;
    private readonly ITestOutputHelper output;

    public TreasureChestTests(ITestOutputHelper output)
    {
        chests = new();
        this.output = output;
        output.WriteLine($"Initial chest count: {chests.Count}");
    }

    [Theory]
    [InlineData(true, true, true)]
    [InlineData(true, false, false)]
    [InlineData(false, true, true)]
    [InlineData(false, false, true)]
    public void CanOpen_WhenCalled_ReturedsExpectedOutocme(bool isLocked, bool hasKey, bool expected)
    {
        // Arrange
        var sut = new TreasureChest(isLocked);
        chests.Push(sut);
        output.WriteLine($"Chest count: {chests.Count}");

        // Act
        var actual = sut.CanOpen(hasKey);

        // Assert
        Assert.Equal(expected, actual);
        Assert.Single(chests);
    }

    [Fact]
    public void CanOpen_ChestIsLockedAndHasKey_ReturnsTrue()
    {
        // Arrange
        var sut = new TreasureChest(true);
        chests.Push(sut);
        output.WriteLine($"Chest count: {chests.Count}");

        // Act
        var result = sut.CanOpen(true);

        // Assert
        Assert.True(result);
        Assert.Single(chests);
    }

    [Fact]
    public void CanOpen_ChestIsLockedAndHasNoKey_ReturnsFalse()
    {
        // Arrange
        var sut = new TreasureChest(true);
        chests.Push(sut);
        output.WriteLine($"Chest count: {chests.Count}");

        // Act
        var result = sut.CanOpen(false);

        // Assert
        Assert.False(result);
        Assert.Single(chests);
    }

    [Fact]
    public void CanOpen_ChestIsUnLockedAndHasKey_ReturnsTrue()
    {
        // Arrange
        var sut = new TreasureChest(false);
        chests.Push(sut);
        output.WriteLine($"Chest count: {chests.Count}");

        // Act
        var result = sut.CanOpen(true);

        // Assert
        Assert.True(result);
        Assert.Single(chests);
    }

    [Fact]
    public void CanOpen_ChestIsUnLockedAndHasNoKey_ReturnsTrue()
    {
        // Arrange
        var sut = new TreasureChest(false);
        chests.Push(sut);
        output.WriteLine($"Chest count: {chests.Count}");

        // Act
        var result = sut.CanOpen(false);

        // Assert
        Assert.True(result);
        Assert.Single(chests);
    }

    public void Dispose()
    {
        chests.Pop();
        Assert.Empty(chests);
    }
}
