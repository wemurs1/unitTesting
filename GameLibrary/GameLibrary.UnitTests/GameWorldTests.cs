using FluentAssertions;
using Moq;

namespace GameLibrary.UnitTests;

public class GameWorldTests
{
    [Fact]
    public void GetPlayerReport_PlayerExists_ReturnsExpectedReport()
    {
        //Arrange
        var player = new Player("Alice", 10, new DateTime(2020, 1, 1));

        var stats = new PlayerStatistics
        {
            PlayerName = player.Name,
            GamesPlayed = 10,
            TotalScore = 1000
        };

        var statisticsServiceStub = new Mock<IPlayerStatisticsService>();
        statisticsServiceStub.Setup(s => s.GetPlayerStatistics(player.Name)).Returns(stats);

        var expected = new PlayerReportDto(
            player.Name,
            player.Level,
            player.JoinDate,
            stats.GamesPlayed,
            stats.TotalScore,
            stats.TotalScore / stats.GamesPlayed
        );
        var sut = new GameWorld(statisticsServiceStub.Object);

        // Act
        var actual = sut.GetPlayerReport(player);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void RecordPlayerGameWin_ValidPlayerAndScore_UpdatesPlayerStatistics()
    {
        // Arrange
        var player = new Player("Alice", 10, new DateTime(2020, 1, 1));
        var stats = new PlayerStatistics
        {
            PlayerName = player.Name,
            GamesPlayed = 10,
            TotalScore = 1000
        };
        var statisticsServiceMock = new Mock<IPlayerStatisticsService>();
        statisticsServiceMock.Setup(s => s.GetPlayerStatistics(player.Name)).Returns(stats);
        var sut = new GameWorld(statisticsServiceMock.Object);

        // Act
        sut.RecordPlayerGameWin(player, 20);

        // Assert
        statisticsServiceMock.Verify(s => s.UpdatePlayerStatistics(It.Is<PlayerStatistics>(stats =>
            stats.PlayerName == player.Name && stats.GamesPlayed == 11 && stats.TotalScore == 1020)));
    }
}
