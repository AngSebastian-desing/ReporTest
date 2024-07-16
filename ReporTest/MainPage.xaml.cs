using ReporTest.Model;
using Xamarin.Forms;

namespace ReporTest
{
    public partial class MainPage : TabbedPage
    {
        public MainPage(Usuario usuario)
        {
            InitializeComponent();

            Children.Add(new ReportsList(usuario));
            Children.Add(new CreateReport(usuario));
            Children.Add(new DataUser(usuario));
        }

    }

}

