using System.Collections.Generic;
using Xunit;
using Moq;
using GameShowApi.Dto;
using GameShowApi.Model;

namespace tests
{
    public class PresenterControllerTest
    {
        [Fact]
        public void PresenterAPI_Returns200()
        {
            var mockRepo = new Mock<IDataStore>();
            PresentersDto mockPresenter = new PresentersDto(1, "Bibble", 1985, 2007);
            List<PresentersDto> tester = new List<PresentersDto>();
            tester.Add(mockPresenter);
            mockRepo.Setup(x => x.GetPresenters()).Returns(tester);
        }
    }
}
