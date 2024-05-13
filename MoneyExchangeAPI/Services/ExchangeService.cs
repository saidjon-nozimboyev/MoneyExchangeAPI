using MoneyExchangeAPI.Interfaces;
using System.Text.Json; // Import for JsonSerializer

// ... other namespaces

public class ExchangeService : IExchange
{
    public async Task<List<Rootobject>> GetInfo()
    {
        HttpClient client = new();
        string url = "https://cbu.uz/uz/arkhiv-kursov-valyut/json/";
        var json = await client.GetStringAsync(url);
        var valutas = JsonSerializer.Deserialize<List<Rootobject>>(json);
        return valutas;
    }

    public async Task<string> UniversalService(string from, string to, double amount)
    {
        var exchangeRates = await GetInfo();

        var fromRate = exchangeRates.FirstOrDefault(rate => rate.Ccy == from)?.Rate;
        var toRate = exchangeRates.FirstOrDefault(rate => rate.Ccy == to)?.Rate;

        if (fromRate == null || toRate == null)
        {
            return "Invalid currency code(s).";
        }

        double fromRateValue = double.Parse(fromRate);
        double toRateValue = double.Parse(toRate);

        double result = (amount / fromRateValue) * toRateValue;
        return $"{amount} {from} is equal to {result:F2} {to}";

    }

    public class Rootobject
    {
        public int id { get; set; }
        public string Code { get; set; }
        public string Ccy { get; set; }
        public string CcyNm_RU { get; set; }
        public string CcyNm_UZ { get; set; }
        public string CcyNm_UZC { get; set; }
        public string CcyNm_EN { get; set; }
        public string Nominal { get; set; }
        public string Rate { get; set; }
        public string Diff { get; set; }
        public string Date { get; set; }
    }
}