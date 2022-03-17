using fakeLook_models.Models;

namespace fakeLook_starter.Contract
{
    public class UserContract
    {
        public string Token { get; set; }
        public User User { get; set; }

        public static UserContract CreateInstance(string token, User user)
        {
            UserContract userContract = new UserContract()
            {
                Token = token,
                User = user
            };
            return userContract;
        }
    }
}
