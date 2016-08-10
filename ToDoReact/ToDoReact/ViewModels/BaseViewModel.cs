using System;
using RxLite;
namespace ToDoReact
{
    public abstract class BaseViewModel
    {
        public virtual void Init() { }

        public virtual IReactiveCommand LoadingCommand { get; }
    }
}

