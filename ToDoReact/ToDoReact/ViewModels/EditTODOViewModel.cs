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
        private IDisposable _deleteCommandSubscription;
        private TODOModel _model;

        public EditTODOViewModel(ITODOService todoService)
        {
            _commandCanExecute = Title.Select((arg) => !string.IsNullOrEmpty(arg));

            SaveChangesCommand = new ReactiveCommand(_commandCanExecute);

            _saveCommandSubscription = SaveChangesCommand.Subscribe((_) =>
            {
                if (IsCompleted.Value)
                {
                    todoService.DeleteItem(_model);
                }
                else
                {
                    _model.Description = Description.Value;
                    _model.Title = Title.Value;
                }
                CoreMethods.PushPageModel<MainViewModel>();
            });

            _deleteCommandSubscription = DeleteItemCommand.Subscribe(async (_) =>
            {
                var areYouSure = await CoreMethods.DisplayAlert(string.Empty, "Are you sure?", "Yes", "No");
                if (areYouSure)
                {
                    todoService.DeleteItem(_model);
                }
            });

        }

        public override void Init(object initData)
        {
            base.Init(initData);
            var model = initData as TODOModel;
            if (model == null)
            {
                return;
            }
            _model = model;

            CreationDate.Value = model.Date;
            Title.Value = model.Title;
            Description.Value = model.Description;
        }

        public ReactiveProperty<bool> IsCompleted { get; } = new ReactiveProperty<bool>();

        public ReactiveProperty<DateTime> CreationDate { get; } = new ReactiveProperty<DateTime>();

        public ReactiveProperty<string> Title { get; } = new ReactiveProperty<string>();

        public ReactiveProperty<string> Description { get; } = new ReactiveProperty<string>();

        public ReactiveCommand SaveChangesCommand { get; }

        public ReactiveCommand DeleteItemCommand { get; } = new ReactiveCommand();

        ~EditTODOViewModel()
        {
            _saveCommandSubscription.Dispose();
            _deleteCommandSubscription.Dispose();
        }
    }
}

