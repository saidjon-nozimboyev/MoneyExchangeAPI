using Microsoft.AspNetCore.Mvc;
using MoneyExchangeAPI.Interfaces;
using System.Threading.Tasks;

namespace MoneyExchangeAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExchangeController(IExchange exchange) : ControllerBase
{
    private readonly IExchange _exchange = exchange;

    [HttpGet]
    public async Task<IActionResult> GetInfo()
    {
        var info = await _exchange.GetInfo();
        return Ok(info);
    }
    [HttpGet("exchange")]
    public async Task<IActionResult> UzsToUsd(string from,string to,double amount)
    {
        var usd = await _exchange.UniversalService(from,to,amount);
        return Ok($"$: {usd}");
    }
}