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
            throw new FormatException($"'{s}' is not an integer");
        }
        if (bf.Sign < 0)
        {
            throw new FormatException($"'{s}' is negative");
        }
        return bf.DividePow10(decimals);
    }
    public static long HexToLong(this string s) => (long)HexToBigInteger(s);
    public static BigInteger HexToBigInteger(this string s)
    {
        if (s is null)
        {
            throw new ArgumentNullException(nameof(s));
        }
        if (s.StartsWith("0x"))
        {
            s = s[2..];
        }
        return BigInteger.Parse(s, NumberStyles.HexNumber);
    }
    public static BigFraction HexToWei(this string s, int decimals = 18)
    {
        BigFraction bf = HexToBigInteger(s);
        return bf.DividePow10(decimals);
    }
    public static string EncodedBytesToString(this string encoded)
    {
        if (encoded is null || encoded.Length < 130)
        {
            throw new FormatException($"'{encoded}' is not encoded as a string");
        }

        var raw = encoded[130..].ToHexBytes().AsSpan();
        if (raw.Length == 0 || raw[0] == 0)
        {
            return "";
        }
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
    public static string EncodedBytesToAddress(this string encoded) =>
        encoded?.Length != 66 || encoded[2..26].Any(x => x != '0') ?
        throw new FormatException($"'{encoded}' is not an address") :
        "0x" + encoded[26..];
}
