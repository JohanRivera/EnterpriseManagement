using EnterpriseManagement.Core.Entities.Auth;

namespace EnterpriseManagement.Core.Interfaces.Auth
{
    public interface IJwtService
    {
        string GenerateToken(AppUser user);
    }
}
