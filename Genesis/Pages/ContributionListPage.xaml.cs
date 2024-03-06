using Genesis.ViewModels;

namespace Genesis.Pages;

public partial class ContributionListPage : ContentPage
{
	public ContributionListPage(ContributionViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
		if(BindingContext is  ContributionViewModel viewModel) {
			viewModel.GetContributionsCommand.Execute(null);
		}
    }
}