using System;
using System.Windows.Input;
using Xamarin.Forms;
namespace ToDoReact.Behaviors
{
    public class ListViewItemTappedBehavior : BehaviorBase<ListView>
    {
        public static readonly BindableProperty BehaviorCommandProperty =
            BindableProperty.Create(nameof(BehaviorCommand), typeof(ICommand), typeof(ListViewItemTappedBehavior), null, BindingMode.TwoWay);

        public ICommand BehaviorCommand
        {
            get { return (ICommand)GetValue(BehaviorCommandProperty); }
            set { SetValue(BehaviorCommandProperty, value); }
        }

        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.ItemTapped += OnListViewItemTapped;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.ItemTapped -= OnListViewItemTapped;
        }

        private void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (BehaviorCommand == null)
                return;

            if (BehaviorCommand.CanExecute(e.Item))
            {
                BehaviorCommand.Execute(e.Item);
                AssociatedObject.SelectedItem = null;
            }
        }
    }
}

