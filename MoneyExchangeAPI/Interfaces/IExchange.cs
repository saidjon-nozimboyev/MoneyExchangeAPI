using static ExchangeService;

namespace MoneyExchangeAPI.Interfaces;

public interface IExchange
{
    Task<List<Rootobject>> GetInfo();
    Task<string> UniversalService(string from, string to, double amount);
}
