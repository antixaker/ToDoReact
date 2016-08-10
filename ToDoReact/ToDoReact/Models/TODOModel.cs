using System;
namespace ToDoReact.Models
{
    public class TODOModel
    {
        public TODOModel(DateTime date, string title) : this(date, title, "Empty")
        {

        }

        public TODOModel(DateTime date, string title, string description)
        {
            Date = date;
            Title = title;
            Description = description;
        }

        public DateTime Date { get; }

        public string Title { get; }

        public string Description { get; }
    }
}

