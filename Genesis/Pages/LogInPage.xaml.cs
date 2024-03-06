using Genesis.ViewModels;

namespace Genesis.Pages;

public partial class LogInPage : ContentPage
{
	public LogInPage(LoginViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}