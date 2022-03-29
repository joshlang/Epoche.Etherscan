namespace Epoche.Etherscan;
public class EtherscanException : Exception
{
    public readonly EtherscanResult<string>? EtherscanError;

    public EtherscanException(EtherscanResult<string> etherscanError) : base(etherscanError?.Result)
    {
        EtherscanError = etherscanError;
    }
}
