using System;
namespace GameShowApi.Dto
{
    public class PresentersDto
    {
        public PresentersDto(string id, string name, int birthYear, int deathYear)
        {
            Id = id;
            Name = name;
            BirthYear = birthYear;
            DeathYear = deathYear;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int BirthYear { get; set; }
        public int DeathYear { get; set; }


    }
}

