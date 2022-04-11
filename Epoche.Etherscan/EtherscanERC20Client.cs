namespace Epoche.Etherscan;
public class EtherscanERC20Client
{
    readonly EtherscanCallClient Client;
    readonly string ContractAddress;

    string? Name, Symbol;
    int? Decimals;

    public EtherscanERC20Client(EtherscanCallClient client, string contractAddress)
    {
        Client = client ?? throw new ArgumentNullException(nameof(client));
        ContractAddress = contractAddress ?? throw new ArgumentNullException(nameof(contractAddress));
    }

    public async Task<string> GetNameAsync(CancellationToken cancellationToken = default) =>
        Name ??= await Client.GetStringAsync(ContractAddress, "name()", null, cancellationToken).ConfigureAwait(false);
    public async Task<string> GetSymbolAsync(CancellationToken cancellationToken = default) =>
        Symbol ??= await Client.GetStringAsync(ContractAddress, "symbol()", null, cancellationToken).ConfigureAwait(false);
    public async Task<int> GetDecimalsAsync(CancellationToken cancellationToken = default) =>
        Decimals ??= await Client.GetInt32Async(ContractAddress, "decimals()", null, cancellationToken).ConfigureAwait(false);
    public async Task<BigFraction> GetTotalSupplyAsync(int? decimals, CancellationToken cancellationToken = default)
    {
        decimals ??= await GetDecimalsAsync(cancellationToken).ConfigureAwait(false);
        return await Client.GetDecimalAsync(ContractAddress, "totalSupply()", null, decimals.Value, cancellationToken).ConfigureAwait(false);
    }
    public async Task<BigFraction> GetBalanceOfAsync(string address, int? decimals, CancellationToken cancellationToken = default)
    {
        decimals ??= await GetDecimalsAsync(cancellationToken).ConfigureAwait(false);
        return await Client.GetDecimalAsync(ContractAddress, "balanceOf(address)", "000000000000000000000000" + address[2..].ToLower(), decimals.Value, cancellationToken).ConfigureAwait(false);
    }
}
