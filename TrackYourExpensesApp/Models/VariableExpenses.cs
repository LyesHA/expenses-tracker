using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackYourExpensesApp.Models
{
    public class VariableExpenses : Expenses
    {
        public VariableExpenses()
        {
        }

        public VariableExpenses(int id, string title, decimal amount, DateTime transactionDate) : base(id, title, amount, transactionDate)
        {
        }
    }
}
