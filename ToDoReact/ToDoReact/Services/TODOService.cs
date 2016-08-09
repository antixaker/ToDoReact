using System;
using System.Collections.Generic;
using ToDoReact.Models;

namespace ToDoReact.Services
{
    public class TODOService : ITODOService
    {
        private List<TODOModel> _todoList = new List<TODOModel>();

        public void Add(TODOModel item)
        {
            _todoList.Add(item);
        }

        public void DeleteItem(TODOModel item)
        {
            _todoList.Remove(item);
        }

        public List<TODOModel> GetAll()
        {
            return _todoList;
        }
    }
}

