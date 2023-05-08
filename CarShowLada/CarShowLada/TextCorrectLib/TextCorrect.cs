using System.Text.RegularExpressions;

namespace TextCorrectLib
{
    public class TextCorrect
    {
        public static bool IsTextCorrect(string text)
        {
            Regex letter = new Regex(@"^[А-ЯЁ][а-яё]*$");
            if (letter.IsMatch(text))
            {
                return true;
            }
            else
                return false;
        }

        public static bool IsNumberPhoneCorrect(string text)
        {
            Regex number = new Regex(@"^\d{11}$");
            if (number.IsMatch(text))
            {
                return true;
            }
            else
                return false;
        }
    }
}
