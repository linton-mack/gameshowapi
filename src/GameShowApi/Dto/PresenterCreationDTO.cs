using System.ComponentModel.DataAnnotations;

namespace GameShowApi.Dto
{
    public class PresenterCreationDTO
    {
        public PresenterCreationDTO( string name, int birthYear, int deathYear)
        {
            Name = name;
            BirthYear = birthYear;
            DeathYear = deathYear;
        }
        
        public PresenterCreationDTO() {}
        
        [Required]
        public string Name { get; set; }
        public int BirthYear { get; set; }
        public int DeathYear { get; set; }

    }
}