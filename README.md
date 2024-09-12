## What is *NetCoreUtils*?

*NetCoreUtils* is a collection of C# reusable functions and methods.

![NuGet Version](https://img.shields.io/nuget/v/FioBankApiClient?style=flat-square&link=https://www.nuget.org/packages/Dlouhy.NetCoreUtils)


## Requirements

*NetCoreUtils* can be used with .NET 6, .NET 7 or .NET 8

## How to install

Install *NetCoreUtils* via NuGet package

    PM> Install-Package Dlouhy.NetCoreUtils

## Examples of using

### DateTimeRange value object example

    string[] formats = { "dd/MM/yyyy" };
    Result<DateTimeRange> rangeOrFailure = DateTimeRange.Create("01/06/2024", "01/07/2024", formats);

    if (rangeOrFailure.IsSuccess)
    {
       rangeOrFailure.Value;
    }

### Email value object example

    string emailValue = "john.doe@example.com";
    Result<string> emailOrFailure = Email.Create(emailValue);

    if (emailOrFailure.IsSuccess)
    {
       emailOrFailure.Value;
    }

### ByteHelper example

    string hex = "FF0A1B";
    byte[] result = hex.HexStringToByteArray();

### StringHelper example

    string input = "·ÈÕÛ˙Ò—Á";
    string outputWithoutDiacritics = input.RemoveDiacritics();

    string input = "hello world";
    string output = input.FirstCharToUpper();

### IPAddressHelper example

    uint testIPAddress = 0x01020304; // Represents IP 1.2.3.4
    string actualResult = testIPAddress.ConvertFromIntegerToIPAddress();

### FileHelper example

    string fileName = "te#st.txt";
    string actualResult = fileName.RemoveInvalidFilenameCharacters();

## Submitting bugs and feature requests

Bugs and feature request are tracked on [GitHub](https://github.com/dlouhy/netcoreutils/issues)
