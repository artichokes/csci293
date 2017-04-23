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
    public class WithdrawViewModel : Screen
    {
        private readonly AuthService authService;
        private readonly NavigationService navigationService;

        private decimal amount;
        private Visibility errorVisible = Visibility.Hidden;
        private string amountStr, errorMessage;

        public WithdrawViewModel(NavigationService navigationService, AuthService authService)
        {
            this.navigationService = navigationService;
            this.authService = authService;
        }

        public string Amount
        {
            get { return amountStr; }
            set
            {
                ErrorVisible = Visibility.Hidden;
                amountStr = value;
                NotifyOfPropertyChange(() => Amount);
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

        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage);
            }
        }

        public bool CanSubmit
        {
            get
            {
                return Decimal.TryParse(Amount, out amount);
            }
        }

        public async Task Submit()
        {
            using (var db = new UserContext())
            {
                await authService.User.UpdateBalance();
                var result = await authService.User.Withdraw(amount);

                if (result.Failure)
                {
                    ErrorMessage = result.Error;
                    ErrorVisible = Visibility.Visible;
                    return;
                }
                else
                {
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

        public void GoToBalance()
        {
            navigationService.NavigateToViewModel(typeof(BalanceViewModel));
        }

        public void GoToWithdraw()
        {
            navigationService.NavigateToViewModel(typeof(WithdrawViewModel));
        }

        public void GoToTransactions()
        {
            navigationService.NavigateToViewModel(typeof(TransactionsViewModel));
        }

        public void GoToLogout()
        {
            navigationService.NavigateToViewModel(typeof(LogoutViewModel));
        }
    }
}
