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
    public static long HexToLong(this string s)
    {
        if (s.StartsWith("0x"))
        {
            s = s[2..];
        }
        return long.Parse(s, NumberStyles.HexNumber);
    }
    public static BigFraction HexToWei(this string s, int decimals = 18)
    {
        if (s.StartsWith("0x"))
        {
            s = s[2..];
        }
        BigFraction bf = BigInteger.Parse(s, NumberStyles.HexNumber);
        return bf.DividePow10(decimals);
    }
    public static string BytesToString(this string encoded)
    {
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
    public static string BytesToAddress(this string encoded) => "0x" + encoded[26..];
}
