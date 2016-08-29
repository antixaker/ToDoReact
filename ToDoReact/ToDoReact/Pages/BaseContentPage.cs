using System;

using Xamarin.Forms;
using ToDoReact;

namespace Pages
{
    public class BaseContentPage : ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var vm = BindingContext as BaseViewModel;
            if (vm != null)
            {
                vm.Init();
            }
        }
    }
}


