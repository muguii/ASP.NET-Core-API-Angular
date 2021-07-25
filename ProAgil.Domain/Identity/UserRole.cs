using Microsoft.AspNetCore.Identity;

namespace ProAgil.Domain.Identity
{
    public class UserRole : IdentityUserRole<int> //DETERMINANDO QUE A CHAVE SERÁ INT. Default = HASH/GUID
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
