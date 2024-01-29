using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Genesis.data;
using Genesis.Service;

namespace Genesis.ViewModels
{
    public partial class LoginViewModel: ObservableObject
    {
        [ObservableProperty]
        private string? identifier;

        [ObservableProperty]
        private string? password;

        [ObservableProperty]
        private bool isLoginEnabled = true;

        private LogInService _loginService;

        private IConnectivity _connectivity;


        public LoginViewModel(LogInService logInService, IConnectivity connectivity)
        {
            _loginService = logInService;
            _connectivity = connectivity;
        }

        [RelayCommand(CanExecute = nameof(CanLogin))]
        public async Task LogIn()
        {
            if (string.IsNullOrWhiteSpace(Identifier) || string.IsNullOrWhiteSpace(Password))
                return;

            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Oops", "No Internet", "OK");
                return;
            }

            var loginDto = new LoginDto
            {
                identifier = Identifier,
                password = Password
            };

            IsLoginEnabled = false;
            string token = await _loginService.LogIn(loginDto);
            if(string.IsNullOrEmpty(token))
                await Shell.Current.DisplayAlert("Genesis","Invalid Credentials", "OK");

            IsLoginEnabled = true;
        }

        public bool CanLogin()
        {
            return IsLoginEnabled;
        }

    }
}
