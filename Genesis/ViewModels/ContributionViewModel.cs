using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Genesis.data;
using Genesis.Service;
using System.Collections.ObjectModel;

namespace Genesis.ViewModels
{
    public partial class ContributionViewModel: ObservableObject
    {

        [ObservableProperty]
        private ObservableCollection<ContributionsDto> contributions = new ObservableCollection<ContributionsDto>();

        [ObservableProperty]
        private bool isRefreshing = false;

        private ContributionService _contributionService;

        private IConnectivity _connectivity;

        public ContributionViewModel(ContributionService contributionService, IConnectivity connectivity)
        {
            _contributionService = contributionService;
            _connectivity = connectivity;
        }

        [RelayCommand]
        public async Task GetContributions()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Oops", "No Internet", "OK");
                return;
            }


            var data = await _contributionService.getContributions();

            IsRefreshing = true;

            data.ForEach( value =>
            {
                var date = DateTimeOffset.FromUnixTimeSeconds(value.Date).DateTime;
                value.DateString = date.ToString("ddd, dd MMM yyyy");
                Contributions.Add(value);
            });

            IsRefreshing = false;
        }

        [RelayCommand]
        public async Task OnRefresh()
        {
            Contributions.Clear();
            await GetContributions();
        }

    }
}
