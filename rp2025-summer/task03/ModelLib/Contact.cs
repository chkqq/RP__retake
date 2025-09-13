namespace ModelLib;

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