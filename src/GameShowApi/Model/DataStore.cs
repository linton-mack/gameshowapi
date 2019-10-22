using System.Collections.Generic;
using GameShowApi.Dto;

namespace GameShowApi.Model
{

    public class DataStore : IDataStore
    {
        public DataStore()
        {
            Presenters = new List<PresentersDto>()
            {
                new PresentersDto(1, "Bibble", 1985, 2007)
            };
        }
        public List<PresentersDto> Presenters { get; }
    
    }
}
