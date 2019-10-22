using System.Collections.Generic;
using GameShowApi.Dto;

namespace GameShowApi.Model
{
    public interface IDataStore
    {
        List<PresentersDto> Presenters { get; }

        PresentersDto GetPresentersById(string id);
    }
}

