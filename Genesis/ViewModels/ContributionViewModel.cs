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

        [ObservableProperty]
        private string name = "";

        [ObservableProperty]
        private float amount = 0.00f;

        [ObservableProperty]
        private string phone = "";

        [ObservableProperty]
        private string paymentMode = "";

        [ObservableProperty]
        private long date = DateTime.Now.Millisecond;

        private ContributionService _contributionService;

        private IConnectivity _connectivity;

        public ContributionViewModel(ContributionService contributionService, IConnectivity connectivity)
        {
            _contributionService = contributionService;
            _connectivity = connectivity;
        }

        [RelayCommand]
        public async Task AddContribution()
        {
            if(String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Phone) || String.IsNullOrEmpty(PaymentMode) ||
                Amount <= 0.00f)
            {
                await Shell.Current.DisplayAlert("Check Fields", "Name, Phone and Payment Mode are required. Amount should be above Zero.", "OK");
                return;
            }
        }


        [RelayCommand]
        public async Task GetContributions()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Oops", "No Internet", "OK");
                return;
            }

            IsRefreshing = true;

            var data = await _contributionService.getContributions();

            data.ForEach( value =>
            {
                var date = DateTimeOffset.FromUnixTimeSeconds(value.Date).DateTime;
                value.DateString = date.ToString("ddd, dd MMM yyyy");
                Contributions.Add(value);
            });

            IsRefreshing = false;
        }

        [RelayCommand]
        public async Task Reload()
        {
            Contributions.Clear();
            await GetContributions();
        }

    }
}
