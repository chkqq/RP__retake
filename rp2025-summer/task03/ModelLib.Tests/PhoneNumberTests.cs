using ModelLib;

using Xunit;

namespace ModelLibTests;

public class PhoneNumberTests
{
    [Theory]
    [InlineData("+7 (8362) 45-02-72", "+78362450272", "")]
    [InlineData("7 (8362) 45-02-72", "+78362450272", "")]
    [InlineData("8 (8362) 45-02-72", "+88362450272", "")]
    [InlineData("1-234-567-8901 x1234", "+12345678901", "1234")]
    [InlineData("+1-234-567-8901x1234", "+12345678901", "1234")]
    public void Constructor_ValidNumbers_ParsesCorrectly(string input, string expectedNumber, string expectedExt)
    {
        PhoneNumber phone = new PhoneNumber(input);

        Assert.Equal(expectedNumber, phone.Number);
        Assert.Equal(expectedExt, phone.Ext);
        Assert.Equal(expectedExt == "" ? expectedNumber : $"{expectedNumber}x{expectedExt}", phone.ToString());
    }

    [Theory]
    [InlineData("")]
    [InlineData("abc")]
    [InlineData("+")]
    [InlineData("12")]
    [InlineData("+x123")]
    public void Constructor_InvalidNumbers_ThrowsException(string input)
    {
        Assert.Throws<ArgumentException>(() => new PhoneNumber(input));
    }
}

public class ContactTests
{
    [Fact]
    public void Constructor_ValidName_SetsProperties()
    {
        Contact contact = new Contact("John", "Middle", "Doe");

        Assert.Equal("John", contact.FirstName);
        Assert.Equal("Middle", contact.MiddleName);
        Assert.Equal("Doe", contact.LastName);
        Assert.Empty(contact.PhoneNumbers);
        Assert.Null(contact.PrimaryPhoneNumber);
    }

    [Fact]
    public void FirstName_Empty_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new Contact(""));
        Contact contact = new Contact("Valid");
        Assert.Throws<ArgumentException>(() => contact.FirstName = "");
    }

    [Fact]
    public void AddPhoneNumber_AddsNumberAndSetsPrimary()
    {
        Contact contact = new Contact("John");
        PhoneNumber phone1 = new PhoneNumber("+1234567890");
        PhoneNumber phone2 = new PhoneNumber("+0987654321");

        contact.AddPhoneNumber(phone1);
        Assert.Single(contact.PhoneNumbers);
        Assert.Equal(phone1, contact.PrimaryPhoneNumber);

        contact.AddPhoneNumber(phone2);
        Assert.Equal(2, contact.PhoneNumbers.Count);
        Assert.Equal(phone1, contact.PrimaryPhoneNumber);
    }

    [Fact]
    public void RemovePhoneNumber_RemovesNumberAndUpdatesPrimary()
    {
        Contact contact = new Contact("John");
        PhoneNumber phone1 = new PhoneNumber("+1234567890");
        PhoneNumber phone2 = new PhoneNumber("+0987654321");

        contact.AddPhoneNumber(phone1);
        contact.AddPhoneNumber(phone2);

        contact.RemovePhoneNumber(phone1);
        Assert.Single(contact.PhoneNumbers);
        Assert.Equal(phone2, contact.PrimaryPhoneNumber);

        contact.RemovePhoneNumber(phone2);
        Assert.Empty(contact.PhoneNumbers);
        Assert.Null(contact.PrimaryPhoneNumber);
    }

    [Fact]
    public void SetPrimaryPhoneNumber_SetsPrimary()
    {
        Contact contact = new Contact("John");
        PhoneNumber phone1 = new PhoneNumber("+1234567890");
        PhoneNumber phone2 = new PhoneNumber("+0987654321");

        contact.AddPhoneNumber(phone1);
        contact.AddPhoneNumber(phone2);

        contact.SetPrimaryPhoneNumber(phone2);
        Assert.Equal(phone2, contact.PrimaryPhoneNumber);
    }

    [Fact]
    public void SetPrimaryPhoneNumber_NotInList_ThrowsException()
    {
        Contact contact = new Contact("John");
        PhoneNumber phone1 = new PhoneNumber("+1234567890");
        PhoneNumber phone2 = new PhoneNumber("+0987654321");

        contact.AddPhoneNumber(phone1);

        Assert.Throws<ArgumentException>(() => contact.SetPrimaryPhoneNumber(phone2));
    }
}