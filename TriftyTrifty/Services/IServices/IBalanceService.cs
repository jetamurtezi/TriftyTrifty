using TriftyTrifty.DataAccess.Models;

namespace TriftyTrifty.Services.IServices
{
    public interface IBalanceService
    {
        List<string> CalculateBalances(ExpenseGroup group);
    }
}
