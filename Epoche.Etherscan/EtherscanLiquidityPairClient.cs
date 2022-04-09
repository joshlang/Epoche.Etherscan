namespace Epoche.Etherscan;
public class EtherscanLiquidityPairClient
{
    readonly EtherscanClient EtherscanClient;
    readonly string ContractAddress;

    string? Token0, Token1;

    public EtherscanLiquidityPairClient(EtherscanClient etherscanClient, string contractAddress)
    {
        EtherscanClient = etherscanClient ?? throw new ArgumentNullException(nameof(etherscanClient));
        ContractAddress = contractAddress ?? throw new ArgumentNullException(nameof(contractAddress));
    }

    public async Task<string> GetToken0Async(CancellationToken cancellationToken = default) =>
        Token0 ??= (await EtherscanClient.CallAsync(to: ContractAddress, inputData: "0x0dfe1681", cancellationToken).ConfigureAwait(false)).BytesToAddress();
    public async Task<string> GetToken1Async(CancellationToken cancellationToken = default) =>
        Token1 ??= (await EtherscanClient.CallAsync(to: ContractAddress, inputData: "0xd21220a7", cancellationToken).ConfigureAwait(false)).BytesToAddress();

    EtherscanERC20Client? erc20;
    public EtherscanERC20Client ERC20 => erc20 ??= new(EtherscanClient, ContractAddress);
}
