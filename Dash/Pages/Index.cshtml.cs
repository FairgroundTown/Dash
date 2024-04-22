using DataFoundation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dash.Pages;

public class IndexModel : DashPageModel
{
    public DbTable<SaleViewRow>? _Table;
    public Dictionary<string, decimal> _Chart;
    public Dictionary<string, decimal> _Chart2;
    public override void Init()
    {
        _Table = DbTable<SaleViewRow>.GetTable("rentalAgreement.CreatedDate >= @DT", "@DT", new DateTime(DateTime.Now.Year - 1, 1, 1));        
        _Chart = _Table.Where(d => d.Month <= DateTime.Now.Month).ToDictionary(k => k.Month.ToString(), k => Convert.ToDecimal(k.ThisYear));
        _Chart2 = _Table.ToDictionary(k => k.Month.ToString(), k => Convert.ToDecimal(k.LastYear));
    }
}
