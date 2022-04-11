using System.Text;
using Epoche.Etherscan;
using Epoche.Shared;

var c = new EtherscanClient();

var addr = "0xbb2b8038a1640196fbe3e38816f3e67cba72d940";
var supply = await c.ERC20(addr).GetTotalSupplyAsync(null);
var name = await c.ERC20(addr).GetNameAsync();
var symbol = await c.ERC20(addr).GetSymbolAsync();
var dec = await c.ERC20(addr).GetDecimalsAsync();
var pair = c.LiquidityPair(addr);
var bal = await pair.ERC20.GetBalanceOfAsync(addr, null);
var tok0 = await pair.GetToken0Async();
var tok1 = await pair.GetToken1Async();

//var name = await c.CallAsync(to: "0xA0b86991c6218b36c1d19D4a2e9Eb0cE3606eB48", inputData: "0x06fdde03");
//var symbol = await c.CallAsync(to: "0xA0b86991c6218b36c1d19D4a2e9Eb0cE3606eB48", inputData: "0x95d89b41");
//var decimals = await c.CallAsync(to: "0xA0b86991c6218b36c1d19D4a2e9Eb0cE3606eB48", inputData: "0x313ce567");
//var token0 = await c.CallAsync(to: "0xbb2b8038a1640196fbe3e38816f3e67cba72d940", inputData: "0x0dfe1681");
//new _ethers.utils.Interface(["function name()"]).encodeFunctionData("name", [])

Console.ReadKey();