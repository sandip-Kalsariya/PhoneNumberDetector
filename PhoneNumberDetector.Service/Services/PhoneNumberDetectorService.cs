using PhoneNumberDetector.Service.IServvices;
using System.Linq;
using System.Text.RegularExpressions;

namespace PhoneNumberDetector.Service.Services
{
    public class PhoneNumberDetectorService : IPhoneNumberDetectorService
    {
        List<Tuple<string, int>> ReplaceNumber = new List<Tuple<string, int>>();
        public PhoneNumberDetectorService()
        {
            // English
            ReplaceNumber.Add(new Tuple<string, int>("one", 1));
            ReplaceNumber.Add(new Tuple<string, int>("two", 2));
            ReplaceNumber.Add(new Tuple<string, int>("three", 3));
            ReplaceNumber.Add(new Tuple<string, int>("four", 4));
            ReplaceNumber.Add(new Tuple<string, int>("five", 5));
            ReplaceNumber.Add(new Tuple<string, int>("six", 6));
            ReplaceNumber.Add(new Tuple<string, int>("seven", 7));
            ReplaceNumber.Add(new Tuple<string, int>("eight", 8));
            ReplaceNumber.Add(new Tuple<string, int>("nine", 9));
            ReplaceNumber.Add(new Tuple<string, int>("zero", 0));

            // Hindi
            ReplaceNumber.Add(new Tuple<string, int>("एक", 1));
            ReplaceNumber.Add(new Tuple<string, int>("दो", 2));
            ReplaceNumber.Add(new Tuple<string, int>("तीन", 3));
            ReplaceNumber.Add(new Tuple<string, int>("चार", 4));
            ReplaceNumber.Add(new Tuple<string, int>("पांच", 5));
            ReplaceNumber.Add(new Tuple<string, int>("छह", 6));
            ReplaceNumber.Add(new Tuple<string, int>("सात", 7));
            ReplaceNumber.Add(new Tuple<string, int>("आठ", 8));
            ReplaceNumber.Add(new Tuple<string, int>("नौ", 9));
            ReplaceNumber.Add(new Tuple<string, int>("शून्य", 0));
        }

        public string DetectPhoneNumbers(string number)
        {
            if (string.IsNullOrEmpty(number))
            {
                return "Mobile number required";
            }

            if (number.Length < 10)
            {
                return "Invalid Mobile number";
            }
            foreach (var item in ReplaceNumber)
            {
                number = number.ToLower().Replace(item.Item1.ToString(), item.Item2.ToString());
            }

            number = number.Replace(" ", "");

            string pattern = @"^(?:(?:\+?\d{1,3}[-.●]?)?\(?(?:\d{1,4})?\)?[-.● ]?\d{1,4}[-.● ]?\d{1,4}[-.●]?\d{1,4}|\d{10,15})$";
            Regex regex = new Regex(pattern);

            var matches = regex.Matches(number);

            var result = "Detected Phone Numbers:\n";

            if (matches.Count > 0)
            {

                foreach (Match match in matches)
                {
                    result += $"{GetNumberFormat(match.Value)}\n";
                }
            }
            else
            {
                result = "Invalid phone number.";
            }
            return result;
        }

        private string GetNumberFormat(string number)
        {
            var result = "";
            if (number.StartsWith("+"))
            {
                result += $"{number} (With Country Code)\n";
            }
            else if (number.StartsWith("0"))
            {
                result += $"{number} (Without Country Code)\n";
            }

            if (number.Contains("(") && number.Contains(")"))
            {
                result += $"{number} (With Parentheses)\n";
            }
            if (number.Contains("-"))
            {
                result += $"{number} (With Dashes)\n";
            }
            if (number.Contains(" "))
            {
                result += $"{number} (With Spaces)\n";
            }
            if (number.Length == 10)
            {
                result += $"{number} (10-digit)\n";
            }
            return result;
        }
    }
}
