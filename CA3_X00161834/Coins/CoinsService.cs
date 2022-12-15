using System.Text.Json;

namespace CA3_X00161834.Coins
{
   
    public class CoinsService : CoinsInterface
    {
        private readonly HttpClient _httpClient;
        const string _baseUrl = "https://coinranking1.p.rapidapi.com";
        const string _apiKey = "802c718324msh2d6558e7edaf0dcp111234jsn5961e007c5b4";
        const string _newsEndPoint = "/coins?referenceCurrencyUuid=yhjMzLPhuIDl&timePeriod=24h&tiers%5B0%5D=1&orderBy=marketCap&orderDirection=desc&limit=50&offset=0";
        const string _host = "coinranking1.p.rapidapi.com";
        public async Task<List<CoinItem>> GetCoins()
        {

            ConfigureHttpResponse();

            var response = await _httpClient.GetAsync(_newsEndPoint);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            var dataRecieved = await JsonSerializer.DeserializeAsync<CoinsDataObject>(stream);
            return dataRecieved.data.coins.Select(n => new CoinItem
            {
                Name = n.name.ToString(),
                Value = n.price,
                Image = n.iconUrl

            }).ToList();
            throw new Exception();
        }

        public CoinsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        private void ConfigureHttpResponse()
        {
            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", _host);
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", _apiKey);
        }
        
    }
    public class CoinItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Image { get; set; }
    }
    public class CoinsDataObject
    {
        public string status { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public Stats stats { get; set; }
        public Coin[] coins { get; set; }
    }

    public class Stats
    {
        public int total { get; set; }
        public int totalCoins { get; set; }
        public int totalMarkets { get; set; }
        public int totalExchanges { get; set; }
        public string totalMarketCap { get; set; }
        public string total24hVolume { get; set; }
    }

    public class Coin
    {
        public string uuid { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public string iconUrl { get; set; }
        public string marketCap { get; set; }
        public string price { get; set; }
        public string btcPrice { get; set; }
        public int listedAt { get; set; }
        public string change { get; set; }
        public int rank { get; set; }
        public string[] sparkline { get; set; }
        public string coinrankingUrl { get; set; }
        public string _24hVolume { get; set; }
    }
}
