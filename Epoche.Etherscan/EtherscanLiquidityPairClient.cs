namespace Epoche.Etherscan;
public class EtherscanLiquidityPairClient
{
    readonly EtherscanCallClient Client;
    readonly string ContractAddress;

    string? Token0, Token1;

    public EtherscanLiquidityPairClient(EtherscanCallClient client, string contractAddress)
    {
        Client = client ?? throw new ArgumentNullException(nameof(client));
        ContractAddress = contractAddress ?? throw new ArgumentNullException(nameof(contractAddress));
    }

    public async Task<string> GetToken0Async(CancellationToken cancellationToken = default) =>
        Token0 ??= await Client.GetAddressAsync(ContractAddress, "token0()", null, cancellationToken).ConfigureAwait(false);
    public async Task<string> GetToken1Async(CancellationToken cancellationToken = default) =>
        Token1 ??= await Client.GetAddressAsync(ContractAddress, "token1()", null, cancellationToken).ConfigureAwait(false);

    EtherscanERC20Client? erc20;
    public EtherscanERC20Client ERC20 => erc20 ??= new(Client, ContractAddress);
}
