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
            var sut = new ApiGameShowsController(mockRepo.Object);

            //Act
            IActionResult result = sut.GetGameShowsById("1");
            // Asset
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public void PostToCreateGameShowIsValid()
        {
            var mockNewGameShow = new GameShowCreationDto("Bulls Eye", "ITV", 1924);
            var mockCreatedGameShow = new GameShowDto("2", "Bulls Eye", "ITV", 1924);
            var mockRepo = new Mock<IDataStore>();
            mockRepo.Setup((p) => p.AddNewGameShow(mockNewGameShow)).Returns(mockCreatedGameShow);

            var sut = new ApiGameShowsController(mockRepo.Object);
            IActionResult result = sut.PostNewGameShow(mockNewGameShow);
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public void PostToCreateGameShowIsInvalid()
        {
            var mockNewGameShow = new GameShowCreationDto(null, "BBC1", 1995);
            var mockRepo = new Mock<IDataStore>();
            var sut = new ApiGameShowsController(mockRepo.Object);
            sut.ModelState.AddModelError("Error", "Oh no something went wrong creating that!");
            IActionResult result = sut.PostNewGameShow(mockNewGameShow);
            Assert.IsType<BadRequestObjectResult>(result);
        }

    }
}