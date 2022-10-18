using Microsoft.EntityFrameworkCore;
using Polling.Domain.Entities;

namespace Polling.Dal
{
    public class PollingContext:DbContext
    {
        public virtual DbSet<Poll>? Poll { get; set; }
        public virtual DbSet<Answers>? Answers { get; set; }
        public virtual DbSet<Questions>? Questions  { get; set; }
        public PollingContext(DbContextOptions<PollingContext> options): base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Questions>(options =>
            {
                options.HasOne(e => e.Poll).WithMany(x => x.Questions).HasForeignKey(x => x.PollId);
            });
            
            builder.Entity<Answers>(options =>
            {
                options.HasOne(e => e.Questions).WithMany(x => x.Answers).HasForeignKey(x => x.QuestionId);
            });

            base.OnModelCreating(builder);
        }
    }
}
