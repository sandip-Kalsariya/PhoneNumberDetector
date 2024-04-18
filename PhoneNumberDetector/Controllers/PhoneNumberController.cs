using Microsoft.AspNetCore.Mvc;
using PhoneNumberDetector.Service.IServvices;

namespace PhoneNumberDetector.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneNumberController : ControllerBase
    {
        private readonly IPhoneNumberDetectorService _phoneNumberDetector;

        public PhoneNumberController(IPhoneNumberDetectorService phoneNumberDetector)
        {
            _phoneNumberDetector = phoneNumberDetector;
        }

        [HttpPost]
        public IActionResult DetectPhoneNumbers([FromBody] string PhoneNumber)
        {
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                return BadRequest("No PhoneNumber provided.");
            }
            if (PhoneNumber.Length < 10)
            {
                return BadRequest("Invalid Mobile number");
            }
            string detectedNumbers = _phoneNumberDetector.DetectPhoneNumbers(PhoneNumber);

            return Ok(detectedNumbers);

           
        }
    }
}
