using Shared.Abstractions;

namespace Polling.Domain.Entities
{
    public class Questions : BaseEntity
    {
        public string QuestionName { get;set;} = string.Empty;
        public IEnumerable<Answers>? Answers { get; set; }
        public int? PollId { get; set; }
        public Poll? Poll { get; set; }
    }
}
