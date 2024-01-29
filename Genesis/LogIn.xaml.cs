using Genesis.ViewModels;

namespace Genesis;

public partial class LogIn : ContentPage
{
	public LogIn(LoginViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}