﻿using System;
using ToDoReact.Services;
using System.Collections.Generic;
using ToDoReact.Models;
using Reactive.Bindings;
using System.Reactive.Linq;
using System.Linq;
using System.Diagnostics;

namespace ToDoReact
{
    public class MainViewModel : BaseViewModel
    {
        private ITODOService _todoService;

        public MainViewModel(ITODOService todoService)
        {
            _todoService = todoService;
            ItemsAreEmpty = Items
                .Select(list => !list.Any())
                .ToReactiveProperty();
        }

        public override void Init()
        {
            Items.Value = _todoService.GetAll();
        }

        public ReactiveProperty<List<TODOModel>> Items { get; } = new ReactiveProperty<List<TODOModel>>(new List<TODOModel>());

        public ReactiveProperty<bool> ItemsAreEmpty { get; }
    }
}

