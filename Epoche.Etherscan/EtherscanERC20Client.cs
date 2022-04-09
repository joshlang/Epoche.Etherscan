namespace Epoche.Etherscan;
public class EtherscanERC20Client
{
    readonly EtherscanClient EtherscanClient;
    readonly string ContractAddress;

    string? Name, Symbol;
    int? Decimals;

    public EtherscanERC20Client(EtherscanClient etherscanClient, string contractAddress)
    {
        EtherscanClient = etherscanClient ?? throw new ArgumentNullException(nameof(etherscanClient));
        ContractAddress = contractAddress ?? throw new ArgumentNullException(nameof(contractAddress));
    }

    public async Task<string> GetNameAsync(CancellationToken cancellationToken = default) =>
        Name ??= (await EtherscanClient.CallAsync(to: ContractAddress, inputData: "0x06fdde03", cancellationToken).ConfigureAwait(false)).BytesToString();
    public async Task<string> GetSymbolAsync(CancellationToken cancellationToken = default) =>
        Symbol ??= (await EtherscanClient.CallAsync(to: ContractAddress, inputData: "0x95d89b41", cancellationToken).ConfigureAwait(false)).BytesToString();
    public async Task<int> GetDecimalsAsync(CancellationToken cancellationToken = default) =>
        Decimals ??= (int)(await EtherscanClient.CallAsync(to: ContractAddress, inputData: "0x313ce567", cancellationToken).ConfigureAwait(false)).HexToLong();
    public async Task<BigFraction> GetTotalSupplyAsync(int? decimals, CancellationToken cancellationToken = default)
    {
        decimals ??= await GetDecimalsAsync(cancellationToken).ConfigureAwait(false);
        return (await EtherscanClient.CallAsync(to: ContractAddress, inputData: "0x18160ddd", cancellationToken).ConfigureAwait(false)).HexToWei(decimals.Value);
    }
    public async Task<BigFraction> GetBalanceOfAsync(string address, int? decimals, CancellationToken cancellationToken = default)
    {
        decimals ??= await GetDecimalsAsync(cancellationToken).ConfigureAwait(false);
        var input = "0x70a08231000000000000000000000000" + address[2..].ToLower();
        return (await EtherscanClient.CallAsync(to: ContractAddress, inputData: input, cancellationToken).ConfigureAwait(false)).HexToWei(decimals.Value);
    }
}
