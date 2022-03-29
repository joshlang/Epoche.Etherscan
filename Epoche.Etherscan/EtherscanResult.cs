namespace Epoche.Etherscan;
public class EtherscanResult<T> where T : class
{
    public string Status { get; init; } = default!;
    public string Message { get; init; } = default!;
    public T? Result { get; init; }
}
