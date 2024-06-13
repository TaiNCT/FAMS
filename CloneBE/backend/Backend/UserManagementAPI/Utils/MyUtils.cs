using System.Text.RegularExpressions;

namespace UserManagementAPI.Utils
{
    public class MyUtils
    {
        public static bool ValidateGmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@gmail\.com$";
            return Regex.IsMatch(email, pattern);
        }

        public static bool GreaterThan18(DateTime bornIn)
        {
            return (bornIn.AddYears(18) >= DateTime.Now);
        }

        public static bool IsOver18(DateTime dob)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dob.Year;
            if (dob.Date > today.AddYears(-age))
                age--;
            return age >= 18;
        }

        public static bool ValidStatus(string status)
        {
            if (status == null)
                return false;
            if (status.ToUpper() == "INACTIVE")
                return false;
            return true;
        }

        public static bool NoSpecialCharacter(string obj)
        {
            Regex regex = new Regex("[@_!#$%^&*()<>?/|}{~:]");
            if (regex.IsMatch(obj))
                return false;
            else
                return true;
        }

        public static bool IsOnlyCharacters(string val)
        {
            return Regex.IsMatch(val, @"^[a-zA-Z\sÀ-ỹ]*$");
        }

        public static bool ValidatePhone(string phoneNumber)
        {
            // Define the regex pattern
            string pattern = @"^0\d{9}$";

            // Create a Regex object
            Regex regex = new Regex(pattern);

            // Use the Regex object to match the input phone number
            Match match = regex.Match(phoneNumber);

            // Return true if the input phone number matches the pattern, otherwise false
            return match.Success;
        }

        public static bool ValidatePassword(string password)
        {
            // Define the regex pattern for password validation
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$";

            // Create a Regex object with the defined pattern
            Regex regex = new Regex(pattern);

            // Use the Regex object to match the input password
            Match match = regex.Match(password);

            // Return true if the input password matches the pattern, otherwise false
            return match.Success;
        }

        public static string GenerateRandomCode(int length)
        {
            var random = new Random();
            return random.Next(0, 999999).ToString($"D{length}");
        }

    }
}
