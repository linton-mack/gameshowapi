using System.Collections.Generic;
// using GameShowApi.Contollers;

namespace GameShowApi.Dto
{
    public interface IDataStore
    {
       List <PresentersDto> DataStore {get; set;} 

      List <PresentersDto> GetPresenters();
    }
}

