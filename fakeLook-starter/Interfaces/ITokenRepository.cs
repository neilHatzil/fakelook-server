using fakeLook_models.Models;

namespace fakeLook_starter.Interfaces
{
    public interface ITokenRepository
    {
        public string CreateToken(User user);
        public string GetPayload(string token);
    }
}
