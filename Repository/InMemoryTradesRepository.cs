using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class InMemoryTradesRepository : ITradesRepository
    {
        private List<Trades> _trades;

        public InMemoryTradesRepository()
        {
            _trades = new List<Trades>();
        }
        public IEnumerable<Trades> GetTrades()
        {
            return _trades;
        }
    }
}
