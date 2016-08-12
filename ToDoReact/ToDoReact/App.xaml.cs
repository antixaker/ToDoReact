using FreshMvvm;
using Xamarin.Forms;
using ToDoReact.Services;

namespace ToDoReact
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            RegisterServices();

            var page = FreshPageModelResolver.ResolvePageModel<MainViewModel>();
            var basicNavContainer = new FreshNavigationContainer(page);
            MainPage = basicNavContainer;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void RegisterServices()
        {
            FreshIOC.Container.Register<ITODOService, TODOService>();
            FreshIOC.Container.Register<ITimeProvider, TimeProvider>();
        }
    }
}

