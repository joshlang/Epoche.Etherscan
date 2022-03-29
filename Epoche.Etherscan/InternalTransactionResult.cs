namespace Epoche.Etherscan;

public class InternalTransactionResult
{
    [JsonInclude] public string value { private get; init; } = default!;
    [JsonInclude] public string isError { private get; init; } = default!;

    public long BlockNumber { get; init; }
    public long Timestamp { get; init; }
    BigFraction? _value;
    [JsonIgnore] public BigFraction Value => _value ??= value.FromWei();
    public long Gas { get; init; }
    public long GasUsed { get; init; }
    [JsonIgnore] public bool IsError => isError != "0" || ErrorCode != "";
    public string Hash { get; init; } = default!;

    public string From { get; init; } = default!;
    public string To { get; init; } = default!;
    public string ContractAddress { get; init; } = default!;
    public string Input { get; init; } = default!;
    public string Type { get; init; } = default!;
    [JsonPropertyName("errCode")]
    public string ErrorCode { get; init; } = default!;
    public string TraceId { get; init; } = default!;
}
