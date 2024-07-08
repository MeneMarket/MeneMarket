using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Vonage;
using Vonage.Messaging;
using Vonage.Request;

namespace MeneMarket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SMSController : RESTFulController
    {
        private readonly IConfiguration _configuration;
        private static readonly Dictionary<string, string> _verificationCodes = new Dictionary<string, string>();
        private static readonly Random random = new Random();

        public SMSController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("send-verification-code")]
        public async Task<IActionResult> SendVerificationCode(string phoneNumber)
        {
            string code = GenerateRandomCode();
            _verificationCodes[phoneNumber] = code;

            var credentials = Credentials.FromApiKeyAndSecret(
                "eb754b43",
                "QZACPf5HwwSco1ro"
            );
            var vonageClient = new VonageClient(credentials);

            var response = await vonageClient.SmsClient.SendAnSmsAsync(new SendSmsRequest
            {
                To = phoneNumber,
                From = "MeneMarket.uz",
                Text = $"Your verification code is: {code}"
            });

            if (response.Messages[0].Status == "0")
            {
                return Ok("Verification code sent successfully.");
            }
            else
            {
                return StatusCode(500, $"Failed to send verification code: {response.Messages[0].ErrorText}");
            }
        }

        [HttpPost("verify-code")]
        public IActionResult VerifyCode(string phoneNumber, string code)
        {
            if (_verificationCodes.TryGetValue(phoneNumber, out string expectedCode))
            {
                if (code == expectedCode)
                {
                    _verificationCodes.Remove(phoneNumber);
                    return Ok("Verification successful!");
                }
                else
                {
                    return BadRequest("Invalid verification code.");
                }
            }
            else
            {
                return BadRequest("Verification code expired or not sent.");
            }
        }

        private string GenerateRandomCode()
        {
            const int codeLength = 4;
            const string digits = "0123456789";
            char[] code = new char[codeLength];

            for (int i = 0; i < codeLength; i++)
            {
                code[i] = digits[random.Next(digits.Length)];
            }

            return new string(code);
        }
    }
}