using OmniClinicAPIv1.Models;

namespace OmniClinicAPIv1.JWT.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
