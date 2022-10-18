namespace Polling.Dto
{
    public class CreatePollDto
    {
        public string Name { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; }
    }
}
