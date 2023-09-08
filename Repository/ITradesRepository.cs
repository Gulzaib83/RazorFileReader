using DataModel;

namespace Repository
{
    public interface ITradesRepository 
    {
        IEnumerable<Trades> GetTrades();    
    }
}