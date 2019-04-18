using System.Collections.Generic;

namespace TrackYourExpensesApp.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Maximum { get; set; }
        public ICollection<Expenses> Expenses { get; set; }

        public Categories() { }

        public Categories(int id, string name,decimal maximum, ICollection<Expenses> expenses)
        {
            Id = id;
            Name = name;
            Maximum = maximum;
            Expenses = expenses;
        }
    }
}