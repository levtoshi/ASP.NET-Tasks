using System.Text.RegularExpressions;

public class UserValidator
{
    public bool ValidateName(string name)
    {
        if (string.IsNullOrEmpty(name))
            return false;

        return Regex.IsMatch(name, @"^[a-zA-Z0-9_]+$");
    }

    public bool ValidatePassword(string password)
    {
        if (string.IsNullOrEmpty(password))
            return false;

        if (password.Length <= 8)
            return false;

        if (!password.Any(char.IsUpper) || !password.Any(char.IsLower))
            return false;

        if (!password.Any(c => "!;%@*".Contains(c)))
            return false;

        string[] zodiacSigns = { "♈", "♉", "♊", "♋", "♌", "♍", "♎", "♏", "♐", "♑", "♒", "♓" };
        if (!zodiacSigns.Any(z => password.Contains(z)))
            return false;

        if (!password.Any(c => "IVXLCDM".Contains(char.ToUpper(c))))
            return false;

        return true;
    }
}