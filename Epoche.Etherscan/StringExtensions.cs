using System.Globalization;
using System.Numerics;
using System.Text;

namespace Epoche.Etherscan;

static class StringExtensions
{
    public static BigFraction FromWei(this string s) => FromWei(s, 18);
    public static BigFraction FromWei(this string s, int decimals)
    {
        if (!BigFraction.TryParse(s, out var bf))
        {
            throw new EtherscanException($"'{s}' is not an integer");
        }
        if (bf.Sign < 0)
        {
            throw new EtherscanException($"'{s}' is negative");
        }
        return bf.DividePow10(decimals);
    }
    public static long HexToLong(this string s) => long.Parse("0" + Strip0x(s), NumberStyles.HexNumber);
    public static BigFraction HexToWei(this string s, int decimals = 18)
    {
        BigFraction bf = BigInteger.Parse("0" + s, NumberStyles.HexNumber);
        return bf.DividePow10(decimals);
    }

    public static BigInteger EncodedHexToBigInteger(this string encoded)
    {
        encoded = Strip0x(encoded);
        if (encoded.Length != 64)
        {
            throw new EtherscanException($"'{encoded}' is not encoded as a uint256");
        }
        return BigInteger.Parse("0" + encoded, NumberStyles.HexNumber);
    }
    public static BigFraction EncodedHexToWei(this string s, int decimals = 18)
    {
        BigFraction bf = EncodedHexToBigInteger(s);
        return bf.DividePow10(decimals);
    }
    public static string EncodedBytesToString(this string encoded)
    {
        encoded = Strip0x(encoded);
        if (encoded.Length < 128)
        {
            throw new EtherscanException($"'{encoded}' is not encoded as a string");
        }

        var raw = encoded[128..].ToHexBytes().AsSpan();
        for (var x = 1; x < raw.Length; ++x)
        {
            if (raw[x] == 0)
            {
                raw = raw[..x];
                break;
            }
        }
        return Encoding.UTF8.GetString(raw);
    }
    public static string EncodedBytesToAddress(this string encoded)
    {
        encoded = Strip0x(encoded);
        if (encoded.Length != 64 || encoded[..24].Any(x => x != '0'))
        {
            throw new EtherscanException($"'{encoded}' is not an address");
        }
        return "0x" + encoded[24..];
    }
    static string Strip0x(string? s) => s is null
        ? throw new EtherscanException("encoded value is missing")
        : s.StartsWith("0x") == true
        ? s[2..]
        : s;
}
