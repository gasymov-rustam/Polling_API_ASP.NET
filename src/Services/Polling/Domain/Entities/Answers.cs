using Shared.Abstractions;

namespace Polling.Domain.Entities
{
    public class Answers : BaseEntity
    {
        public string AnswerText { get; set; } = string.Empty;
        public int QuestionId { get; set; }
        public Questions? Questions { get; set; }
    }
}
