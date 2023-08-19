namespace Epoche.Etherscan;

public class TransactionResult
{
    [JsonInclude] public string value { private get; init; } = default!;
    [JsonInclude] public string gasPrice { private get; init; } = default!;
    [JsonInclude] public string isError { private get; init; } = default!;
    [JsonInclude] public string? txreceipt_status { private get; init; } = default;

    public long BlockNumber { get; init; }
    public long Timestamp { get; init; }
    public long Nonce { get; init; }
    BigFraction? _value;
    [JsonIgnore] public BigFraction Value => _value ??= value.FromWei();
    public long Gas { get; init; }
    BigFraction? _gasPrice;
    [JsonIgnore] public BigFraction GasPrice => _gasPrice ??= gasPrice.FromWei();
    [JsonIgnore] public bool IsError => isError != "0" || txreceipt_status == "0";
    public long GasUsed { get; init; }
    public long CumulativeGasUsed { get; init; }
    public long Confirmations { get; init; }
    public int TransactionIndex { get; init; }
    public string Hash { get; init; } = default!;
    public string? BlockHash { get; init; }
    public string From { get; init; } = default!;
    public string To { get; init; } = default!;
    public string Input { get; init; } = default!;
    public string ContractAddress { get; init; } = default!;
}
