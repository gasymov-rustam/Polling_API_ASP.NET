using System.ComponentModel.DataAnnotations;

namespace Polling.Dto
{
    public class CreatePollDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime Duration { get; set; }
    }
}
