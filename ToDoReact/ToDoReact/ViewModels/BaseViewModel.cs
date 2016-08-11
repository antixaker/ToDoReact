using System;
using RxLite;
using FreshMvvm;
namespace ToDoReact
{
    public abstract class BaseViewModel : FreshBasePageModel
    {
        public virtual void Init() { }

        public virtual IReactiveCommand LoadingCommand { get; }
    }
}

