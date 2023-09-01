using System.Globalization;
using System.Numerics;

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

    public static BigInteger EncodedHexToBigInteger(this string encoded) => EthereumEncoding.EncodedHexToBigInteger(encoded);
    public static BigFraction EncodedHexToWei(this string s, int decimals = 18) => EthereumEncoding.EncodedHexToWei(s, decimals);
    public static string EncodedBytesToString(this string encoded) => EthereumEncoding.EncodedBytesToString(encoded);
    public static string EncodedBytesToAddress(this string encoded) => EthereumEncoding.EncodedBytesToAddress(encoded);
    public static string Strip0x(this string? s) => s is null
        ? throw new EtherscanException("encoded value is missing")
        : s.StartsWith("0x")
        ? s[2..]
        : s;
}
