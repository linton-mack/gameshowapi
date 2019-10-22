using System.Collections.Generic;
using GameShowApi.Dto;

namespace GameShowApi.Model
{
    public interface IDataStore
    {
        List<PresentersDto> Presenters { get; }
        List<GameShowDto> GameShows { get; }

        PresentersDto GetPresentersById(string id);
        PresentersDto AddNewPresenter(PresenterCreationDTO newPresenter);

        GameShowDto GetGameShowsById(string id);

        GameShowDto AddNewGameShow(GameShowCreationDto newGameShow);
    }
}

