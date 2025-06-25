
using TriftyTrifty.DataAccess.Models;
using TriftyTrifty.Services.IServices;

namespace TriftyTrifty.Services
{
    public class BalanceService : IBalanceService
    {

        public List<string> CalculateBalances(ExpenseGroup group)
        {
            var expenses = group.Expenses;
            var users = expenses.Select(e => e.PaidByUser).Distinct().ToList();
            var total = expenses.Sum(e => e.Amount);
            var perPerson = users.Count == 0 ? 0 : total / users.Count;

            var balances = users.Select(u => new
            {
                User = u,
                Balance = expenses.Where(e => e.PaidByUserId == u.Id).Sum(e => e.Amount) - perPerson
            }).ToList();

            var creditors = balances.Where(b => b.Balance > 0).OrderByDescending(b => b.Balance).ToList();
            var debtors = balances.Where(b => b.Balance < 0).OrderBy(b => b.Balance).ToList();

            var transactions = new List<string>();
            int i = 0, j = 0;

            while (i < debtors.Count && j < creditors.Count)
            {
                var debtor = debtors[i];
                var creditor = creditors[j];
                var amount = Math.Min(-debtor.Balance, creditor.Balance);

                transactions.Add($"{debtor.User.Name} duhet t’i japë {creditor.User.Name} {amount:0.00}€");

                debtor = new { debtor.User, Balance = debtor.Balance + amount };
                creditor = new { creditor.User, Balance = creditor.Balance - amount };

                if (Math.Abs(debtor.Balance) < 0.01m) i++;
                else debtors[i] = debtor;

                if (creditor.Balance < 0.01m) j++;
                else creditors[j] = creditor;
            }

            return transactions;
        }
    }
}

