namespace Epoche.Etherscan;

public class NativeBalanceResult
{
    public string Account { get; init; } = default!;
    [JsonInclude] public string balance { private get; init; } = default!;
    BigFraction? _balance;
    [JsonIgnore] public BigFraction Balance => _balance ??= balance.FromWei();
}
