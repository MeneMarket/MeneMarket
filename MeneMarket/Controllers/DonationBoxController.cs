﻿using MeneMarket.Models.Foundations.DonationBoxes;
using MeneMarket.Services.Orchestrations.DonationBoxes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace MeneMarket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonationBoxController : RESTFulController
    {
        private readonly IDonationBoxOrchestrationService donationBoxOrchestrationService;

        public DonationBoxController(IDonationBoxOrchestrationService donationBoxOrchestrationService)
        {
            this.donationBoxOrchestrationService = donationBoxOrchestrationService;
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async ValueTask<ActionResult<DonationBox>> PostDonationBoxAsync(DonationBox donationBox) =>
            await this.donationBoxOrchestrationService.AddDonationBoxAsync(donationBox);

        [HttpGet]
        [Authorize(Roles = "User,Admin,Owner")]
        public ActionResult<IQueryable<DonationBox>> GelAllDonationBoxs()
        {
            IQueryable<DonationBox> allUDonationBoxs =
                this.donationBoxOrchestrationService.RetrieveAllDonationBoxes();

            return Ok(allUDonationBoxs);
        }

        [HttpGet("ById")]
        [Authorize(Roles = "Admin,User,Owner")]
        public async ValueTask<ActionResult<DonationBox>> GetDonationBoxByIdAsync(Guid id) =>
            await this.donationBoxOrchestrationService.RetrieveDonationBoxByIdAsync(id);

        [HttpDelete]
        [Authorize(Roles = "Owner")]
        public async ValueTask<ActionResult<DonationBox>> DeleteDonationBoxAsync(Guid id) =>
            await this.donationBoxOrchestrationService.RemoveDonationBoxByIdAsync(id);
    }
}