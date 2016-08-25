using System;
using System.Collections.Generic;
using ToDoReact.Models;
using System.Collections.ObjectModel;

namespace ToDoReact.Services
{
    public class TODOService : ITODOService
    {
        private ObservableCollection<TODOModel> _todoList = new ObservableCollection<TODOModel>();

        public void Add(TODOModel item)
        {
            _todoList.Add(item);
        }

        public void DeleteItem(TODOModel item)
        {
            _todoList.Remove(item);
        }

        public ObservableCollection<TODOModel> GetAll()
        {
            return _todoList;
        }
    }
}

