using Microsoft.AspNetCore.Identity;

namespace Forum.Entities
{
    public class User(string userName) : IdentityUser<Guid>(userName)
    {
    }
}
