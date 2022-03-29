namespace Epoche.Etherscan;

public class EtherscanClientOptions
{
    public static readonly TimeSpan DefaultMinCallIntervalWithoutKey = TimeSpan.FromMilliseconds(6000);
    public static readonly TimeSpan DefaultMinCallIntervalWithKey = TimeSpan.FromMilliseconds(250);

    public string Endpoint { get; set; } = KnownEndpoints.Ethereum;
    public string? ApiKey { get; set; }
    public TimeSpan? MinCallInterval { get; set; }
}
