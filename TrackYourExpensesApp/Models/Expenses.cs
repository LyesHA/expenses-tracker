using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackYourExpensesApp.Models
{
    public abstract class Expenses
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Column(TypeName ="money")]
        public decimal Amount { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Transaction date")]
        public DateTime TransactionDate { get; set; }

        [Display(Name = "Category")]
        public int CategoriesId { get; set; }

        public Categories Category { get; set; }

        protected Expenses() { }

        protected Expenses(int id, string title, decimal amount, DateTime transactionDate)
        {
            Id = id;
            Title = title;
            Amount = amount;
            TransactionDate = transactionDate;
        }
    }
}