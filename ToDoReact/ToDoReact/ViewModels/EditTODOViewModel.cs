using System;
using ToDoReact;
using ToDoReact.Models;
using Reactive.Bindings;
using System.Reactive.Linq;
using ToDoReact.Services;

namespace ViewModels
{
    public class EditTODOViewModel : BaseViewModel
    {
        private IDisposable _saveCommandSubscription;

        public EditTODOViewModel(ITODOService todoService, TODOModel model)
        {
            CreationDate.Value = model.Date;
            Title.Value = model.Title;
            Description.Value = model.Description;

            _commandCanExecute = Title.Select((arg) => !string.IsNullOrEmpty(arg));

            SaveChangesCommand = new ReactiveCommand(_commandCanExecute);
            _saveCommandSubscription = SaveChangesCommand.Subscribe((_) =>
            {
                if (Completed.Value)
                {
                    todoService.DeleteItem(model);
                }
                else
                {
                    model.Description = Description.Value;
                    model.Title = Title.Value;
                }
            });
        }

        public ReactiveProperty<bool> Completed { get; } = new ReactiveProperty<bool>();

        public ReactiveProperty<DateTime> CreationDate { get; } = new ReactiveProperty<DateTime>();

        public ReactiveProperty<string> Title { get; } = new ReactiveProperty<string>();

        public ReactiveProperty<string> Description { get; } = new ReactiveProperty<string>();

        public ReactiveCommand SaveChangesCommand { get; }

        ~EditTODOViewModel()
        {
            _saveCommandSubscription.Dispose();
        }
    }
}

