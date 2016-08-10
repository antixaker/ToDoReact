using System;
using ToDoReact.Services;
using System.Collections.Generic;
using ToDoReact.Models;
using Reactive.Bindings;
using System.Reactive.Linq;

namespace ToDoReact
{
    public class MainViewModel
    {
        private ITODOService _todoService;

        public MainViewModel(ITODOService todoService)
        {
            _todoService = todoService;
            Items.Value = _todoService.GetAll();
        }

        public ReactiveProperty<List<TODOModel>> Items = new ReactiveProperty<List<TODOModel>>();
    }
}

