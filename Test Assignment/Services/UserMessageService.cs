using Microsoft.EntityFrameworkCore;
using Test_Assignment.Context;
using Test_Assignment.Models;
using Test_Assignment.Common;

namespace Test_Assignment.Services
{
    public class UserMessageService : IUserMessageService
    {
        private readonly TestDbContext _dbContext;
        public UserMessageService(TestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        async public Task AddMessageAsync(string message)
        {
            var newUser = new User
            {
                UserName = CommonOptions.currentMachineName,
                Message = message,
                Time = DateTime.Now,
            };

            if(newUser != null)
            {
                await _dbContext.Users.AddAsync(newUser);
                await _dbContext.SaveChangesAsync();
            }
        }

        async public Task<List<User>> GetAllUsersMessagesAsync()
        {
            var messages = await _dbContext.Users.ToListAsync();
            if (messages.Count >= 20)
            {
                var oldestMessage = messages[0];
                _dbContext.Users.Remove(oldestMessage);
                await _dbContext.SaveChangesAsync();
            }
            return messages;
        }

        async public Task<List<User>> GetCurrentUserMessagesAsync()
        {
            var filteredMessages = await _dbContext.Users
                .Where(u => u.UserName == CommonOptions.currentMachineName)
                .ToListAsync();

            if (filteredMessages.Count >= 10)
            {
                var oldestMessage = filteredMessages[0];
                _dbContext.Users.Remove(oldestMessage);
                await _dbContext.SaveChangesAsync();
            }

            return filteredMessages;
        }
    }
}
