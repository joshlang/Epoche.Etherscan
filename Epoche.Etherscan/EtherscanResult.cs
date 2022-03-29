namespace Epoche.Etherscan;
public sealed record EtherscanResult<T>(string Status, string Message, T? Result) where T : class;