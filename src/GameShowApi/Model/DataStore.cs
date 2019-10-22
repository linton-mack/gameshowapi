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
        }
        public List<PresentersDto> Presenters { get; }
        public PresentersDto GetPresentersById(string id)
        {
            return Presenters.FirstOrDefault((presenter) => presenter.Id == id);
        }

    }
}
