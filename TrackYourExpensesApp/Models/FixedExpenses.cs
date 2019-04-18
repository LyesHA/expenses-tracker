using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrackYourExpensesApp.Models
{
    public class FixedExpenses : Expenses
    {
        [Required]
        public int Frequence { get; set; }

        [Required]
        [Display(Name ="By")]
        public string ByDate { get; set; }

        public FixedExpenses() { }

        public FixedExpenses(int id, string title, decimal amount, DateTime transactionDate, int frequence) : base(id, title, amount, transactionDate)
        {
            this.Frequence = frequence;
        }

    }

   
}
