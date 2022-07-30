using Enforsement.Core.ViewModel;

namespace Enforsement.Core.View;

public partial class StartPage : ContentPage
{
	public StartPage()
	{
		InitializeComponent();
        BindingContext = new StartPageViewModel();
    }
}