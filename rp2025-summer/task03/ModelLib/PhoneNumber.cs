using System.Text.RegularExpressions;

namespace ModelLib;

public class PhoneNumber
{
    private static readonly Regex PhoneRegex = new(
        @"^\+?(\d+)(?:x(\d+))?$",
        RegexOptions.Compiled);

    public PhoneNumber(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Phone number cannot be empty", nameof(text));
        }

        // Удаляем все нецифровые символы, кроме '+' и 'x'
        string cleaned = Regex.Replace(text, @"[^\d+x]", "");

        Match match = PhoneRegex.Match(cleaned);
        if (!match.Success)
        {
            throw new ArgumentException("Invalid phone number format", nameof(text));
        }

        string mainNumber = match.Groups[1].Value;
        string ext = match.Groups[2].Value;

        if (mainNumber.Length < 3)
        {
            throw new ArgumentException("Phone number is too short", nameof(text));
        }

        Number = $"+{mainNumber}";
        Ext = ext;
    }

    public string Number { get; }

    public string Ext { get; }

    public override string ToString()
    {
        return string.IsNullOrEmpty(Ext) ? Number : $"{Number}x{Ext}";
    }

    public override bool Equals(object? obj)
    {
        return obj is PhoneNumber other &&
               Number == other.Number &&
               Ext == other.Ext;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Number, Ext);
    }
}