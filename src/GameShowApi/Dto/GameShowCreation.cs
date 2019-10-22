using System.ComponentModel.DataAnnotations;

namespace GameShowApi.Dto
{
    public class GameShowCreationDto
    {
        public GameShowCreationDto(string title, string channel, int year)
        {
            Title = title;
            Channel = channel;
            Year = year;
        }

        [Required]
        public string Title { get; set; }
        public string Channel { get; set; }
        public int Year { get; set; }
    }
}