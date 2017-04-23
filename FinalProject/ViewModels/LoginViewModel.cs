using Caliburn.Micro;
using FinalProject.Models;
using FinalProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FinalProject.ViewModels
{
    public class LoginViewModel : Screen
    {
        private readonly AuthService authService;
        private readonly NavigationService navigationService;

        string userId, pin;
        uint userIdUint, pinUint;
        Visibility errorVisible = Visibility.Hidden;

        public LoginViewModel(NavigationService navigationService, AuthService authService)
        {
            this.navigationService = navigationService;
            this.authService = authService;
        }

        protected override void OnActivate()
        {
            authService.SignOut();
            base.OnActivate();
        }

        public string UserId
        {
            get { return userId; }
            set
            {
                ErrorVisible = Visibility.Hidden;
                userId = value;
                NotifyOfPropertyChange(() => UserId);
                NotifyOfPropertyChange(() => CanSubmit);
            }
        }

        public string Pin
        {
            get { return pin; }
            set
            {
                ErrorVisible = Visibility.Hidden;
                pin = value;
                NotifyOfPropertyChange(() => Pin);
                NotifyOfPropertyChange(() => CanSubmit);
            }
        }

        public Visibility ErrorVisible
        {
            get { return errorVisible; }
            set
            {
                errorVisible = value;
                NotifyOfPropertyChange(() => ErrorVisible);
            }
        }

        public bool CanSubmit
        {
            get
            {
                var isValidUserId = !string.IsNullOrWhiteSpace(UserId)
                    && (UserId.Length == 5)
                    && (uint.TryParse(UserId, out userIdUint));

                var isValidPin = !string.IsNullOrWhiteSpace(Pin)
                    && (Pin.Length == 4)
                    && (uint.TryParse(Pin, out pinUint));

                return isValidUserId && isValidPin;
            }
        }

        public async Task Submit()
        {
            using (var db = new UserContext())
            {
                var result = await UserManager.AuthenticateUser(userIdUint, pinUint);
                if (result.Failure)
                {
                    ErrorVisible = Visibility.Visible;
                    return;
                }
                else
                {
                    authService.SignIn(result.Value);
                    navigationService.NavigateToViewModel(typeof(BalanceViewModel));
                }
            }
        }

        public void ExecuteFilterView(ActionExecutionContext context)
        {
            var keyArgs = context.EventArgs as KeyEventArgs;

            if (keyArgs.Key == Key.Return)
            {
                Submit().Wait();
            }
        }
    }
}
