using Xunit;
using Moq;
using GameShowApi;
using GameShowApi.Dto;
using GameShowApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace tests
{
    public class PresenterControllerTest
    {
        [Fact]
        public void PresenterAPIById_Returns200()
        {
            //Arrange
            var mockRepo = new Mock<IDataStore>();
            PresentersDto mockPresenter = new PresentersDto("1", "Bibble", 1985, 2007);
            mockRepo.Setup((repo) => repo.GetPresentersById("2"))
            .Returns(mockPresenter);
            var sut = new ApiPresentersController(mockRepo.Object);

            //Act
            IActionResult result = sut.GetPresentersById("2");
            // Asset
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void PostToCreatePresenterIsValid()
        {
            var mockNewPresenter = new PresenterCreationDTO("Happy Davies", 1990, 2020);
            var mockCreatedPresenter = new PresentersDto("3", "Happy Davies", 1990, 2020);
            var mockRepo = new Mock<IDataStore>();
            mockRepo.Setup((p) => p.AddNewPresenter(mockNewPresenter)).Returns(mockCreatedPresenter);

            var sut = new ApiPresentersController(mockRepo.Object);
            IActionResult result = sut.PostNewPresenter(mockNewPresenter);
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public void PostToCreatePresenterIsInvalid()
        {
            var mockNewPresenter = new PresenterCreationDTO(null, 1990, 2020);
            var mockRepo = new Mock<IDataStore>();
            var sut = new ApiPresentersController(mockRepo.Object);
            sut.ModelState.AddModelError("error", "Oops something went wrong");
            IActionResult result = sut.PostNewPresenter(mockNewPresenter);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void PatchToUpdatePresenterName()
        {
            var mockRepo = new Mock<IDataStore>();
            PresentersDto mockPresenter = new PresentersDto("1", "Bibble", 1985, 2007);
            PresentersDto mockCreatedPresenter = new PresentersDto("1", "Bob", 1985, 2007);
            
            mockRepo.Setup((patch) => patch.GetPresentersById("1"))
            .Returns(mockCreatedPresenter);

            var sut = new ApiPresentersController(mockRepo.Object);

            IActionResult result = sut.GetPresentersById("1");
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
