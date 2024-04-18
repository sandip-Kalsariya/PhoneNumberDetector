using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneNumberDetector.Service.IServvices;
using PhoneNumberDetector.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneNumberDetector.Test
{
    [TestClass()]
    public class PhoneNumberDetectorTests
    {
        PhoneNumberDetectorService phoneDetectorService = new PhoneNumberDetectorService();

        [TestMethod()]
        public void DetectPhoneNumbersTest()
        {
            var result = phoneDetectorService.DetectPhoneNumbers("");
            Assert.AreEqual("Mobile number required", result);
        }

        [TestMethod("")]
        public void DetectPhoneNumbers_EmptyInput_ReturnsNoNumbers()
        {
            var result = phoneDetectorService.DetectPhoneNumbers("");
            Assert.AreEqual("Mobile number required", result);
        }

        [TestMethod()]
        public void DetectPhoneNumbers_WithCountryCode_ReturnsNumbers()
        {
            var result = phoneDetectorService.DetectPhoneNumbers("+91-1234567890");
            Assert.AreEqual("Detected Phone Numbers:\n+91-1234567890 (With Country Code)\n+91-1234567890 (With Dashes)\n\n", result);
        }

        [TestMethod()]
        public void DetectPhoneNumbers_WithoutCountryCode_ReturnsNumbers()
        {
            var result = phoneDetectorService.DetectPhoneNumbers("01234567890");
            Assert.AreEqual("Detected Phone Numbers:\n01234567890 (Without Country Code)\n\n", result);
        }

        [TestMethod()]
        public void DetectPhoneNumbers_CombinationOfEnglishHindi_ReturnsNumbers()
        {
            var result = phoneDetectorService.DetectPhoneNumbers("ONE दो तीन FOUR FIVE छह SEVEN EIGHT NINE शून्य");
            Assert.AreEqual("Detected Phone Numbers:\n1234567890 (10-digit)\n\n", result);
        }
    }
}