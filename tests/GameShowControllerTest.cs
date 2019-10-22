using Xunit;
using Moq;
using GameShowApi;
using GameShowApi.Dto;
using GameShowApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace tests
{
    public class GameShowControllerTest
    {
        [Fact]
        public void GameShowAPIById_Returns200()
        {
            //Arrange
            var mockRepo = new Mock<IDataStore>();
            GameShowDto mockGameShow = new GameShowDto("1", "The Generation Game", "BBC1", 1971);

            mockRepo.Setup((repo) => repo.GetGameShowsById("1"))
            .Returns(mockGameShow);

            var sut = new GameShowsController(mockRepo.Object);

            //Act
            IActionResult result = sut.GetGameShowsById("1");
            // Asset
            Assert.IsType<OkObjectResult>(result);

        }

    }
}