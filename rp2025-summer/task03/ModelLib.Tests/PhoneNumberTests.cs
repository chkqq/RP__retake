using ModelLib;

using Xunit;

namespace ModelLibTests;

public class PhoneNumberTests
{
    public static TheoryData<string, string, string> ValidPhoneNumberData => new()
    {
        { "+7 (8362) 45-02-72", "+78362450272", "" },
        { "7 (8362) 45-02-72", "+78362450272", "" },
        { "8 (8362) 45-02-72", "+88362450272", "" },
        { "1-234-567-8901 x1234", "+12345678901", "1234" },
        { "+1-234-567-8901x1234", "+12345678901", "1234" }
    };

    public static TheoryData<string> InvalidPhoneNumberData => new()
    {
        "",
        "abc",
        "+",
        "12",
        "+x123"
    };

    [Theory]
    [MemberData(nameof(ValidPhoneNumberData))]
    public void Constructor_ValidNumbers_ParsesCorrectly(string input, string expectedNumber, string expectedExt)
    {
        PhoneNumber phone = new PhoneNumber(input);

        Assert.Equal(expectedNumber, phone.Number);
        Assert.Equal(expectedExt, phone.Ext);
        Assert.Equal(expectedExt == "" ? expectedNumber : $"{expectedNumber}x{expectedExt}", phone.ToString());
    }

    [Theory]
    [MemberData(nameof(InvalidPhoneNumberData))]
    public void Constructor_InvalidNumbers_ThrowsException(string input)
    {
        Assert.Throws<ArgumentException>(() => new PhoneNumber(input));
    }
}