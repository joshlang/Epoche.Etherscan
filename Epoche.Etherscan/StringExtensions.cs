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
}
