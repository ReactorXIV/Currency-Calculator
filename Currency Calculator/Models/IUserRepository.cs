namespace Currency_Calculator.Models
{
    public interface IUserRepository
    {
        Task<string?> LoginUser(UserDto userDto);
        Task RegisterUser(UserDto userDto);
    }
}
