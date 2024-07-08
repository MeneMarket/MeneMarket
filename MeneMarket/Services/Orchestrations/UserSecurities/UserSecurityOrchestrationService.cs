﻿using MeneMarket.Models.Foundations.Users;
using MeneMarket.Models.Orchestrations.LoginUsers;
using MeneMarket.Models.Orchestrations.Tokens;
using MeneMarket.Services.Foundations.Tokens;
using MeneMarket.Services.Processings.Users;

namespace MeneMarket.Services.Orchestrations.Users
{
    public class UserSecurityOrchestrationService : IUserSecurityOrchestrationService
    {
        private readonly IUserProcessingService userProcessingService;
        private readonly ITokenService tokenService;

        public UserSecurityOrchestrationService(
            IUserProcessingService userProcessingService,
            ITokenService tokenService)
        {
            this.userProcessingService = userProcessingService;
            this.tokenService = tokenService;
        }

        public async ValueTask<User> AddUserAsync(User user)
        {
            user.UserId = Guid.NewGuid();
            return await this.userProcessingService.AddUserAsync(user);
        }

        public UserToken LoginUser(LoginUser loginUser)
        {
            IQueryable<User> allUsers = this.userProcessingService.RetrieveAllUsers();

            var result = allUsers.FirstOrDefault(retrievedUser =>
                retrievedUser.PhoneNumber.Equals(loginUser.PhoneNumber) &&
                retrievedUser.Password.Equals(loginUser.Password) &&
                retrievedUser.IsArchived == false);

            if (result != null)
            {
                return this.tokenService.AddToken(result);
            }
            else
                throw new Exception("User Not Found!");
        }
    }
}