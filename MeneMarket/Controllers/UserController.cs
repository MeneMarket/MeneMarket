﻿using MeneMarket.Models.Foundations.Users;
using MeneMarket.Models.Orchestrations.UsersWithCountss;
using MeneMarket.Models.Orchestrations.UserWithImages;
using MeneMarket.Services.Orchestrations.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace MeneMarket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : RESTFulController
    {
        private readonly IUserOrchestrationService userOrchestrationService;

        public UserController(IUserOrchestrationService userOrchestrationService) =>
            this.userOrchestrationService = userOrchestrationService;

        [HttpGet]
        [Authorize(Roles = "Admin,Owner")]
        public ActionResult<UsersWithCount> GelAllUsers(int pageNumber)
        {
            UsersWithCount allUsers =
                this.userOrchestrationService.RetrieveUsersByPage(pageNumber);

            return Ok(allUsers);
        }

        [HttpGet("Profile")]
        [Authorize(Roles = "Admin,User,Owner")]
        public async ValueTask<ActionResult<User>> GetUserProfileAsync()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");

            if (!Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return BadRequest("UserId is not in a valid format.");
            }

            return await this.userOrchestrationService.RetrieveUserProfileAsync(User);
        }

        [HttpPut]
        [Authorize(Roles = "Admin,User,Owner")]
        public async ValueTask<ActionResult<User>> PutUserAsync(UserWithImages userWithImages) =>
            await this.userOrchestrationService.ModifyUserAsync(userWithImages);

        [HttpPut]
        [Route("Archive and UnArchive")]
        [Authorize(Roles = "Admin,Owner")]
        public async ValueTask<ActionResult<User>> ArchiveAndUnArchiveUser(Guid id) =>
            await this.userOrchestrationService.ArchiveAndUnArchiveUser(id);

        [HttpDelete]
        [Authorize(Roles = "Owner")]
        public async ValueTask<ActionResult<User>> DeleteUserAsync(Guid id) =>
            await this.userOrchestrationService.RemoveUserByIdAsync(id);
    }
}