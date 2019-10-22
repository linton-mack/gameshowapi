using System.Collections.Generic;
using GameShowApi.Dto;
// using GameShowApi.Contollers;

namespace GameShowApi.Model
{
    public interface IDataStore
    {
        List<PresentersDto> Presenters { get; }

        // List<PresentersDto> GetPresenters();
    }
}

