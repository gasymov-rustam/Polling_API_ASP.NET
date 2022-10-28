using Shared.Abstractions;

namespace Polling.Security.Domain.Entities
{
    public sealed class Role : BaseEntity
    {
        public Role(string name)
        {
            Name = name;
        }

        public Role()
        {
            Users = new HashSet<User>();
        }

        public string Name { get; set; }
        public ICollection<User> Users { get; }
    }
}
