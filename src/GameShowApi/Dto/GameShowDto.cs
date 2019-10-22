
namespace GameShowApi.Dto
{
    public class GameShowDto
    {
        public GameShowDto(string id, string title, string channel, int year)
        {
            Id = id;
            Title = title;
            Channel = channel;
            Year = year;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Channel { get; set; }
        public int Year { get; set; }
    }
}