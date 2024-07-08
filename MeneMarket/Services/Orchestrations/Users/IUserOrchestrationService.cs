﻿using System.Security.Claims;
using MeneMarket.Models.Foundations.Users;
using MeneMarket.Models.Orchestrations.UsersWithCountss;
using MeneMarket.Models.Orchestrations.UserWithImages;

namespace MeneMarket.Services.Orchestrations.Users
{
    public interface IUserOrchestrationService
    {
        UsersWithCount RetrieveUsersByPage(int pageNumber);
        ValueTask<User> RetrieveUserByIdAsync(Guid id);
        ValueTask<User> ArchiveAndUnArchiveUser(Guid id);
        ValueTask<User> RetrieveUserProfileAsync(ClaimsPrincipal user);
        ValueTask<User> ModifyUserAsync(UserWithImages userWithImages);
        ValueTask<User> RemoveUserByIdAsync(Guid id);
    }
}