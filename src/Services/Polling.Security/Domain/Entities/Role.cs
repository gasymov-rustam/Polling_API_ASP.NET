using Shared.Abstractions;

namespace Polling.Security.Domain.Entities
{
    internal sealed class Role : BaseEntity
    {
        public Role(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public List<User> Users { get; }
    }
}
