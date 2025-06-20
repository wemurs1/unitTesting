namespace GameLibrary.UnitTests;

public class TreasureChestTests
{
    // MethodName_StateUnderTest_ExpectedBehaviour

    [Fact]
    public void CanOpen_ChestIsLockedAndHasKey_ReturnsTrue()
    {
        // Arrange
        var chest = new TreasureChest(true);

        // Act
        var result = chest.CanOpen(true);

        // Assert
        Assert.True(result);
    }
}
