using System;
using RxLite;
using FreshMvvm;
namespace ToDoReact
{
    public abstract class BaseViewModel : FreshBasePageModel
    {
        public virtual void Init() { }

        protected IObservable<bool> _commandCanExecute;

        public virtual IReactiveCommand LoadingCommand { get; }
    }
}

