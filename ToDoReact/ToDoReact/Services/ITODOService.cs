using System;
using ToDoReact.Models;
using System.Collections.ObjectModel;

namespace ToDoReact.Services
{
    public interface ITODOService
    {
        ObservableCollection<TODOModel> GetAll();

        void Add(TODOModel item);

        void DeleteItem(TODOModel item);
    }
}

