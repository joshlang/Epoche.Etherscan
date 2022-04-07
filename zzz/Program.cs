using Epoche.Etherscan;

var c = new EtherscanClient();

var addr = "0xA0b86991c6218b36c1d19D4a2e9Eb0cE3606eB48";
var supply = await c.GetTotalSupplyAsync(addr, null);
var name = await c.GetTokenNameAsync(addr);
var symbol = await c.GetTokenSymbolAsync(addr);
var dec = await c.GetTokenDecimalsAsync(addr);
//var name = await c.CallAsync(to: "0xA0b86991c6218b36c1d19D4a2e9Eb0cE3606eB48", inputData: "0x06fdde03");
//var symbol = await c.CallAsync(to: "0xA0b86991c6218b36c1d19D4a2e9Eb0cE3606eB48", inputData: "0x95d89b41");
//var decimals = await c.CallAsync(to: "0xA0b86991c6218b36c1d19D4a2e9Eb0cE3606eB48", inputData: "0x313ce567");
//new _ethers.utils.Interface(["function name()"]).encodeFunctionData("name", [])

Console.ReadKey();