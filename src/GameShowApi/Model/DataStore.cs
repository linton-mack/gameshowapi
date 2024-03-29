using System;
using System.Collections.Generic;
using GameShowApi.Dto;
using System.Linq;

namespace GameShowApi.Model
{
    public class DataStore : IDataStore
    {
        public DataStore()
        {
            Presenters = new List<PresentersDto>()
            {
                new PresentersDto("1", "Bibble", 1985, 2007),
                new PresentersDto("2", "Babble", 1930, 1935)
            };

            GameShows = new List<GameShowDto>()
            {
                new GameShowDto("1","The Generation Game", "BBC1", 1971)

            };
        }

        public List<PresentersDto> Presenters { get; }
        public List<GameShowDto> GameShows { get; }

        public PresentersDto GetPresentersById(string id)
        {
            return Presenters.FirstOrDefault((presenter) => presenter.Id == id);
        }

        public PresentersDto UpdatePresenterName(string id)
        {
            return GetPresentersById(id);

        }

        public PresentersDto AddNewPresenter(PresenterCreationDTO newPresenter)
        {
            int id = Int32.Parse(Presenters.Max((presenter) => presenter.Id)) + 1;


            PresentersDto presenterToAdd = new PresentersDto(id.ToString(), newPresenter.Name, newPresenter.BirthYear,
                newPresenter.DeathYear);

            Presenters.Add(presenterToAdd);
            return presenterToAdd;
        }

        public GameShowDto GetGameShowsById(string id)
        {
            return GameShows.FirstOrDefault((show) => show.Id == id);
        }

        public GameShowDto AddNewGameShow(GameShowCreationDto newGameShow)
        {
            int id = Int32.Parse(GameShows.Max((gameshow) => gameshow.Id)) + 1;

            GameShowDto gameShowToAdd = new GameShowDto(id.ToString(), newGameShow.Title, newGameShow.Channel, newGameShow.Year);

            GameShows.Add(gameShowToAdd);
            return gameShowToAdd;
        }
    }
}