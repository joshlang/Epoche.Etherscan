namespace Epoche.Etherscan;
public class EtherscanException : Exception
{
    public readonly EtherscanResult<string>? EtherscanError;

    public EtherscanException(string message) : base(message)
    {
    }
    public EtherscanException(EtherscanResult<string> etherscanError) : base(etherscanError?.Result)
    {
        EtherscanError = etherscanError;
    }
}
