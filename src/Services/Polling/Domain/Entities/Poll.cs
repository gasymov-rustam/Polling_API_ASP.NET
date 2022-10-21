using Shared.Abstractions;

namespace Polling.Domain.Entities
{
    public class Poll : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime Duration { get; set; }
        public IEnumerable<Questions>? Questions { get; set; }
    }
}
