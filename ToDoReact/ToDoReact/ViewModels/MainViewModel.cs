using System;
using ToDoReact.Services;
using System.Collections.Generic;
using ToDoReact.Models;
using Reactive.Bindings;
using System.Reactive.Linq;
using System.Linq;

namespace ToDoReact
{
    public class MainViewModel : BaseViewModel
    {
        private ITODOService _todoService;

        public MainViewModel(ITODOService todoService)
        {
            _todoService = todoService;
        }

        public override void Init()
        {
            Items.Value = _todoService.GetAll();
            ItemsAreEmpty = Items.Any().ToReactiveProperty();
        }

        public ReactiveProperty<List<TODOModel>> Items { get; } = new ReactiveProperty<List<TODOModel>>();

        public ReactiveProperty<bool> ItemsAreEmpty { get; private set; }
    }
}

