﻿namespace Epoche.Etherscan;

public class ERC721TokenTransferResult
{
    [JsonInclude] public string gasPrice { private get; init; } = default!;

    public long BlockNumber { get; init; }
    public long Timestamp { get; init; }
    public long Nonce { get; init; }    
    public long Gas { get; init; }
    BigFraction? _gasPrice;
    [JsonIgnore] public BigFraction GasPrice => _gasPrice ??= gasPrice.FromWei();
    public long GasUsed { get; init; }
    public long CumulativeGasUsed { get; init; }
    public long Confirmations { get; init; }
    public int TokenDecimal { get; init; }
    public int TransactionIndex { get; init; }
    public string Hash { get; init; } = default!;
    public string TokenID { get; init; } = default!;

    public string? BlockHash { get; init; }
    public string From { get; init; } = default!;
    public string To { get; init; } = default!;
    public string ContractAddress { get; init; } = default!;
    public string TokenName { get; init; } = default!;
    public string TokenSymbol { get; init; } = default!;
}
