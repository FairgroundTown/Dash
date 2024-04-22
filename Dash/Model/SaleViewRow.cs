using DataFoundation;

namespace Dash;

public class SaleViewRow : DbRow
{
    [DbField]
    public long Month{ get; set; }
    [DbField]
    public long? ThisYear{ get; set; }
    [DbField]
    public long LastYear{ get; set; }

    public override string Select()
    {
        return 
            "SELECT MONTH(rentalAgreement.CreatedDate) Month, " + 
            $"NULLIF(COUNT(DISTINCT CASE WHEN YEAR(rentalAgreement.CreatedDate) = {DateTime.Now.Year} THEN rentalAgreement.Code END), 0) 'ThisYear', " + 
            $"COUNT(DISTINCT CASE WHEN YEAR(rentalAgreement.CreatedDate) = {DateTime.Now.Year - 1} THEN rentalAgreement.Code END) 'LastYear' " + 
            "FROM rentalAgreement " + 
            "INNER JOIN transactionRequestResult ON " + 
            "rentalAgreement.Code = transactionRequestResult.PlanReference " + 
            "AND transactionRequestResult.TransactionStatus = 'AUTHORISED' " + 
            "AND transactionRequestResult.Amount > 0 ";
    }

    public override string GroupBy()
    {
        return "GROUP BY MONTH(rentalAgreement.CreatedDate)";
    }
}
