using Test_Assignment.Models;

namespace Test_Assignment.Services
{
    public interface IUserMessageService
    {
        Task<List<User>> GetAllUsersMessagesAsync();
        Task<List<User>> GetCurrentUserMessagesAsync();
        Task AddMessageAsync(string message);
    }
}
