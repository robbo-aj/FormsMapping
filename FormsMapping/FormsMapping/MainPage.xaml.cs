using FormsMapping.ViewModels;
using Xamarin.Forms;

namespace FormsMapping
{
	public partial class MainPage : ContentPage
	{
        public MainPage()
		{
            BindingContext = new MainViewModel();
			InitializeComponent();
		}
    }
}