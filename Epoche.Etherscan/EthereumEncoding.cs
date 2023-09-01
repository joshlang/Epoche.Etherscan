using System.Globalization;
using System.Numerics;
using System.Text;

namespace Epoche.Etherscan;
public static class EthereumEncoding
{
    public static string EncodedBytesToString(string encodedBytes)
    {
        encodedBytes = encodedBytes.Strip0x();
        if (encodedBytes.Length < 128)
        {
            throw new EtherscanException($"'{encodedBytes}' is not encoded as a string");
        }

        var raw = encodedBytes[128..].ToHexBytes().AsSpan();
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
    public static string EncodedBytesToAddress(string encodedBytes)
    {
        encodedBytes = encodedBytes.Strip0x();
        if (encodedBytes.Length != 64 || encodedBytes[..24].Any(x => x != '0'))
        {
            throw new EtherscanException($"'{encodedBytes}' is not an address");
        }
        return "0x" + encodedBytes[24..];
    }
    public static BigInteger EncodedHexToBigInteger(string encodedBytes)
    {
        encodedBytes = encodedBytes.Strip0x();
        if (encodedBytes.Length != 64)
        {
            throw new EtherscanException($"'{encodedBytes}' is not encoded as a uint256");
        }
        return BigInteger.Parse("0" + encodedBytes, NumberStyles.HexNumber);
    }
    public static BigFraction EncodedHexToWei(string s, int decimals = 18)
    {
        BigFraction bf = EncodedHexToBigInteger(s);
        return bf.DividePow10(decimals);
    }
}
