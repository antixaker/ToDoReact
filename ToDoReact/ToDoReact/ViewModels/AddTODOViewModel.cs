﻿using System;
using ToDoReact;
using ToDoReact.Services;
using Reactive.Bindings;
using ToDoReact.Models;
using Services;
using System.Reactive.Subjects;
using System.Reflection;
using System.Reactive.Linq;
namespace ViewModels
{
    public class AddTODOViewModel : BaseViewModel
    {
        private ITODOService _todoService;
        private ITimeProvider _timeProvider;
        private IObservable<bool> _commandCanExecute;

        public AddTODOViewModel(ITODOService todoService, ITimeProvider timeService)
        {
            _todoService = todoService;
            _timeProvider = timeService;
            _commandCanExecute = Title.Select((arg) => !string.IsNullOrEmpty(arg));
            AddItemCommand = new ReactiveCommand(_commandCanExecute);
            AddItemCommand.Subscribe((_) =>
                                     _todoService.Add(new TODOModel(CreationDate, Title.Value, Description.Value)));
        }

        public DateTime CreationDate
        {
            get { return _timeProvider.CurrentTime; }
        }

        public ReactiveProperty<string> Title { get; } = new ReactiveProperty<string>();

        public ReactiveProperty<string> Description { get; } = new ReactiveProperty<string>();

        public ReactiveCommand AddItemCommand { get; }
    }
}

