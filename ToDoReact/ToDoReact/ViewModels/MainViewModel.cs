using System;
using ToDoReact.Services;
using System.Collections.Generic;
using ToDoReact.Models;
using Reactive.Bindings;
using System.Reactive.Linq;
using System.Linq;
using FreshMvvm;
using ViewModels;

namespace ToDoReact
{
    public class MainViewModel : BaseViewModel
    {
        private ITODOService _todoService;
        private IDisposable _addCommandSubscription;
        private IDisposable _editCommandSubscription;

        public MainViewModel(ITODOService todoService)
        {
            _todoService = todoService;
            ItemsAreEmpty = Items
                .Select(list => !list.Any())
                .ToReadOnlyReactiveProperty();
            _addCommandSubscription = AddTODOCommand.Subscribe((_) => CoreMethods.PushPageModel<AddTODOViewModel>(true));
            _editCommandSubscription = EditTODOCommand.Subscribe((_) => CoreMethods.PushPageModel<EditTODOViewModel>(true));
        }

        public override void Init()
        {
            Items.Value = _todoService.GetAll();
        }

        public ReactiveProperty<List<TODOModel>> Items { get; } = new ReactiveProperty<List<TODOModel>>(new List<TODOModel>());

        public ReadOnlyReactiveProperty<bool> ItemsAreEmpty { get; }

        public ReactiveCommand AddTODOCommand { get; } = new ReactiveCommand();

        public ReactiveCommand EditTODOCommand { get; } = new ReactiveCommand();

        ~MainViewModel()
        {
            _addCommandSubscription.Dispose();
            _editCommandSubscription.Dispose();
        }
    }
}

