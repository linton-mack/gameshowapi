using System.Collections.Generic;
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
    }
}
