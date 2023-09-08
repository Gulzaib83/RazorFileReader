using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataModel;
using Repository;
using FileReaderWeb.Misc;
using Microsoft.Data.SqlClient;

namespace FileReaderWeb.Pages.V2
{
    public class IndexModel : PageModel
    {
        private readonly Repository.ApplicationDBContext _context;

        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        [BindProperty(SupportsGet = true)]
        public string SortField { get; set; } 

        [BindProperty(SupportsGet = true)]
        public string SortDirection { get; set; }

        public string CurrentFilter { get; set; }


        public IndexModel(Repository.ApplicationDBContext context)
        {
            _context = context;
        }

        public PaginatedList<DataModel.Trades> Trades { get; set; } = default!;

        public async Task OnGetAsync(int? pageIndex , string searchString, string currentFilter)
        {
            PageIndex = pageIndex ?? 1;
            
            if (searchString != null)
                pageIndex = 1;
            else
                searchString = currentFilter;
            CurrentFilter = searchString;

            var query = _context.Trades.AsQueryable();

            // Apply sorting
            if (!string.IsNullOrWhiteSpace(SortField))
            {
                switch (SortField)
                {
                    case "StockSymbol":
                        query = SortDirection == "asc"
                            ? query.OrderBy(item => item.StockSymbol)
                            : query.OrderByDescending(item => item.StockSymbol);
                        break;
                    case "TradeTime":
                        query = SortDirection == "asc"
                            ? query.OrderBy(item => item.TradeTime)
                            : query.OrderByDescending(item => item.TradeTime);
                        break;
                    case "TradePrice":
                        query = SortDirection == "asc"
                            ? query.OrderBy(item => item.TradePrice)
                            : query.OrderByDescending(item => item.TradePrice);
                        break;
                    case "Qunatity":
                        query = SortDirection == "asc"
                            ? query.OrderBy(item => item.Qunatity)
                            : query.OrderByDescending(item => item.Qunatity);
                        break;
                    case "BuyerId":
                        query = SortDirection == "asc"
                            ? query.OrderBy(item => item.BuyerId)
                            : query.OrderByDescending(item => item.BuyerId);
                        break;
                    case "SellerId":
                        query = SortDirection == "asc"
                            ? query.OrderBy(item => item.SellerId)
                            : query.OrderByDescending(item => item.SellerId);
                        break;
                    case "TradeType":
                        query = SortDirection == "asc"
                            ? query.OrderBy(item => item.TradeType)
                            : query.OrderByDescending(item => item.TradeType);
                        break;
                    case "CommissionFee":
                        query = SortDirection == "asc"
                            ? query.OrderBy(item => item.CommissionFee)
                            : query.OrderByDescending(item => item.CommissionFee);
                        break;
                    default:
                        break;
                }
            }

            if (!String.IsNullOrEmpty(searchString))
                query = query.Where(s => s.StockSymbol.Contains(searchString) || s.TradeTime.ToString().Contains(searchString)
                        || s.TradePrice.ToString().Contains(searchString) || s.Qunatity.ToString().Contains(searchString) 
                        || s.BuyerId.ToString().Contains(searchString) || s.SellerId.ToString().Contains(searchString)
                        || s.TradeType.Contains(searchString) || s.CommissionFee.ToString().Contains(searchString));


            Trades = await PaginatedList<DataModel.Trades>.CreateAsync(query, PageIndex, PageSize);
        }

        public string GetNewSortDirection(string currentSortField)
        {
            if (SortField == currentSortField)
            {
                if (SortDirection == "asc")
                {
                    SortDirection = "desc";
                }
                else
                    SortDirection = "asc";
            }
            else
            {
                // Default to ascending when sorting a new column
                SortField = currentSortField;
                SortDirection = "asc";
            }

            return SortDirection;
        }
    }
}
