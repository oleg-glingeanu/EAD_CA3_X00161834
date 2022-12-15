namespace CA3_X00161834.Coins
{
    public interface CoinsInterface
    {
        Task<List<CoinItem>> GetCoins();
    }
}
