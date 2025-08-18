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

public class Contact
{
    private string _firstName = null!;
    private readonly List<PhoneNumber> _phoneNumbers = new();
    private PhoneNumber? _primaryPhoneNumber;

    public Contact(string firstName, string? middleName = null, string? lastName = null)
    {
        FirstName = firstName;
        MiddleName = middleName ?? string.Empty;
        LastName = lastName ?? string.Empty;
    }

    public string FirstName
    {
        get => _firstName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("First name cannot be empty", nameof(value));
            }
            _firstName = value;
        }
    }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

    public IReadOnlyList<PhoneNumber> PhoneNumbers => _phoneNumbers.AsReadOnly();

    public PhoneNumber? PrimaryPhoneNumber => _primaryPhoneNumber;

    public void AddPhoneNumber(PhoneNumber value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (!_phoneNumbers.Contains(value))
        {
            _phoneNumbers.Add(value);

            // Если это первый номер, делаем его основным
            if (_phoneNumbers.Count == 1)
            {
                _primaryPhoneNumber = value;
            }
        }
    }

    public void RemovePhoneNumber(PhoneNumber value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (_phoneNumbers.Remove(value))
        {
            // Если удаляемый номер был основным, выбираем новый основной
            if (_primaryPhoneNumber != null && _primaryPhoneNumber.Equals(value))
            {
                _primaryPhoneNumber = _phoneNumbers.FirstOrDefault();
            }
        }
    }

    public void SetPrimaryPhoneNumber(PhoneNumber value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (!_phoneNumbers.Contains(value))
        {
            throw new ArgumentException("Phone number is not in the contact's list", nameof(value));
        }

        _primaryPhoneNumber = value;
    }
}