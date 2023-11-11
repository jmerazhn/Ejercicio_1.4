using Ejercicio_1._4.Views;

namespace Ejercicio_1._4
{
    public partial class App : Application
    {
        public static DbContext _DbContext { get; set; }
        public App(DbContext context)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ListPage());
            _DbContext = context;
        }
    }
}