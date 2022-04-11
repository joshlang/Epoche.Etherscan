namespace Epoche.Etherscan;
public class EtherscanCallClient
{
    readonly EtherscanClient EtherscanClient;

    public EtherscanCallClient(EtherscanClient etherscanClient)
    {
        EtherscanClient = etherscanClient ?? throw new ArgumentNullException(nameof(etherscanClient));
    }

    public static string GetInputData(string functionSignature, string? data)
    {
        var input = Keccak256.ComputeEthereumFunctionSelector(functionSignature, true);
        if (!string.IsNullOrEmpty(data))
        {
            input += data.StartsWith("0x") ? data[2..] : data;
        }
        return input;
    }

    public Task<string> GetRawAsync(string contractAddress, string inputData, CancellationToken cancellationToken = default) =>
        EtherscanClient.GetResultAsync<string>("proxy", "eth_call", cancellationToken, ("to", contractAddress), ("data", inputData), ("tag", "latest"));

    public Task<string> GetRawAsync(string contractAddress, string functionSignature, string? data, CancellationToken cancellationToken = default) =>
        GetRawAsync(contractAddress, GetInputData(functionSignature, data), cancellationToken);

    public async Task<string> GetStringAsync(string contractAddress, string inputData, CancellationToken cancellationToken = default) =>
        (await GetRawAsync(contractAddress, inputData, cancellationToken).ConfigureAwait(false)).BytesToString();

    public Task<string> GetStringAsync(string contractAddress, string functionSignature, string? data, CancellationToken cancellationToken = default) =>
        GetStringAsync(contractAddress, GetInputData(functionSignature, data), cancellationToken);

    public async Task<int> GetInt32Async(string contractAddress, string inputData, CancellationToken cancellationToken = default) =>
        (int)(await GetRawAsync(contractAddress, inputData, cancellationToken).ConfigureAwait(false)).HexToLong();

    public Task<int> GetInt32Async(string contractAddress, string functionSignature, string? data, CancellationToken cancellationToken = default) =>
        GetInt32Async(contractAddress, GetInputData(functionSignature, data), cancellationToken);

    public async Task<long> GetInt64Async(string contractAddress, string inputData, CancellationToken cancellationToken = default) =>
        (await GetRawAsync(contractAddress, inputData, cancellationToken).ConfigureAwait(false)).HexToLong();

    public Task<long> GetInt64Async(string contractAddress, string functionSignature, string? data, CancellationToken cancellationToken = default) =>
        GetInt64Async(contractAddress, GetInputData(functionSignature, data), cancellationToken);

    public async Task<string> GetAddressAsync(string contractAddress, string inputData, CancellationToken cancellationToken = default) =>
        (await GetRawAsync(contractAddress, inputData, cancellationToken).ConfigureAwait(false)).BytesToAddress();

    public Task<string> GetAddressAsync(string contractAddress, string functionSignature, string? data, CancellationToken cancellationToken = default) =>
        GetAddressAsync(contractAddress, GetInputData(functionSignature, data), cancellationToken);

    public async Task<BigFraction> GetDecimalAsync(string contractAddress, string inputData, int decimals, CancellationToken cancellationToken = default) =>
        (await GetRawAsync(contractAddress, inputData, cancellationToken).ConfigureAwait(false)).HexToWei(decimals);

    public Task<BigFraction> GetDecimalAsync(string contractAddress, string functionSignature, string? data, int decimals, CancellationToken cancellationToken = default) =>
        GetDecimalAsync(contractAddress, GetInputData(functionSignature, data), decimals, cancellationToken);

    public async Task<bool> GetBooleanAsync(string contractAddress, string inputData, CancellationToken cancellationToken = default) =>
        (await GetRawAsync(contractAddress, inputData, cancellationToken)).Skip(2).Any(x => x != '0');

    public Task<bool> GetBooleanAsync(string contractAddress, string functionSignature, string? data, CancellationToken cancellationToken = default) =>
        GetBooleanAsync(contractAddress, GetInputData(functionSignature, data), cancellationToken);
}
