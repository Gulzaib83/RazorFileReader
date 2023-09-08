using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class SQLTradesRepository : ITradesRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public SQLTradesRepository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public IEnumerable<Trades> GetTrades()
        {
            return _dbContext.Trades;
        }
    }
}
