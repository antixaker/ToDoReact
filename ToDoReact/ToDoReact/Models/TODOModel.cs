using System;
namespace ToDoReact.Models
{
    public class TODOModel
    {
        public TODOModel(DateTime date, string title, string description)
        {
            Date = date;
            Title = title;
            Description = description;
        }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}

