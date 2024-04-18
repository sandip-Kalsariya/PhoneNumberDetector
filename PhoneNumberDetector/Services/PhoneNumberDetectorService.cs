using PhoneNumberDetector.IServvices;
using System.Linq;
using System.Text.RegularExpressions;

namespace PhoneNumberDetector.Services
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
        //private static readonly Regex PhoneNumberPattern = new Regex(
        //   @"\(?\d{1,4}\)?[-. ]?\d{1,4}[-. ]?\d{1,4}(?:[-. ]?\d{1,4})?",
        //   RegexOptions.Compiled | RegexOptions.IgnoreCase);

        //public string[] DetectPhoneNumbers(string PhoneNumber)
        //{
        //    if (string.IsNullOrEmpty(PhoneNumber))
        //    {
        //        throw new ArgumentException("Input text cannot be null or empty.", nameof(PhoneNumber));
        //    }

        //    return PhoneNumberPattern.Matches(PhoneNumber).Select(match => match.Value).ToArray();
        //}

        public string DetectPhoneNumbers(string number)
        {
            //if (number.Contains(" "))
            //{
            //    string[] separatedStrings = number.Split(' ');
            //    separatedStrings.Any(x => x.Contains(" "));
            //    var res =  number.ToLower().Contains(ReplaceNumber.Select(x => x.Item2));
            //}
            //ReplaceNumber.Where(x=>x.Item1)

            foreach (var item in ReplaceNumber)
            {
                number = number.ToLower().Replace(item.Item1.ToString(), item.Item2.ToString());
            }

            number = number.Replace(" ", "");
            //var regex = new Regex(@"\b(?:\+?\d{1,3}[-.●]?)?\(?(?:\d{2,3})?\)?[-.● ]?\d{1,4}[-.● ]?\d{1,4}[-.●]?\d{1,4}\b");
            //var regex = new Regex(@"\b(\+?\d{1,3}[-.●]?)?\(?(?:\d{2,3})?\)?[-.● ]?\d{1,4}[-.● ]?\d{1,4}[-.●]?\d{1,4}\b");
            //var regex = new Regex(@"\b\+?(\d{1,3})?[-. (]*(\d{1,4})[-. )]*(\d{1,4})[-. ]*(\d{1,4})\b");
            //var regex = new Regex(@"\b(\+?(\d{1,3})?[-. (]*(\d{1,4})[-. )]*(\d{1,4})[-. ]*(\d{1,4}))\b");
            //var regex = new Regex(@"\b\+?\d{1,3}[-.●]?\(?(?:\d{2,3})?\)?[-.● ]?\d{1,4}[-.● ]?\d{1,4}[-.● ]?\d{1,4}\b");

            //string pattern = @"^(?:(?:\+?\d{1,3}[-.●]?)?\(?(?:\d{2,3})?\)?[-.● ]?\d{1,4}[-.● ]?\d{1,4}[-.●]?\d{1,4}|\d{10})$";
            //Regex regex = new Regex(pattern);

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
                //return "With Country Code";
            }
            else if (number.StartsWith("0"))
            {
                result += $"{number} (Without Country Code)\n";
            }

            if (number.Contains("(") && number.Contains(")"))
            {
                result += $"{number} (With Parentheses)\n";
                //return "With Parentheses";
            }
            if (number.Contains("-"))
            {
                result += $"{number} (With Dashes)\n";
                //return "With Dashes";
            }
            if (number.Contains(" "))
            {
                result += $"{number} (With Spaces)\n";
                //return "With Spaces";
            }
            if (number.Length == 10)
            {
                result += $"{number} (10-digit)\n";
                //return "10-digit";
            }
            return result;
        }
    }
}
