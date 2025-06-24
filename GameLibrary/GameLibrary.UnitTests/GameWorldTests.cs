using FluentAssertions;
using GameLibrary.UnitTests.Fakes;

namespace GameLibrary.UnitTests;

public class GameWorldTests
{
    [Fact]
    public void GetPlayerReport_PlayerExists_ReturnsExpectedReport()
    {
        //Arrange
        var player = new Player("Alice", 10, new DateTime(2020, 1, 1));
        var playerStatisticsService = new FakePlayerStatisticsService();
        var stats = new PlayerStatistics
        {
            PlayerName = player.Name,
            GamesPlayed = 10,
            TotalScore = 1000
        };
        playerStatisticsService.UpdatePlayerStatistics(stats);
        var expected = new PlayerReportDto(
            player.Name,
            player.Level,
            player.JoinDate,
            stats.GamesPlayed,
            stats.TotalScore,
            stats.TotalScore / stats.GamesPlayed
        );
        var sut = new GameWorld(playerStatisticsService);

        // Act
        var actual = sut.GetPlayerReport(player);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}
