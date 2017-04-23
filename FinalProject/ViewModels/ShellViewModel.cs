using Caliburn.Micro;
using FinalProject.Helpers;
using FinalProject.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FinalProject.ViewModels
{
    public class ShellViewModel : Screen
    {
        string title = "ATM";

        private readonly SimpleContainer container;
        private NavigationService navigationService;

        public ShellViewModel(SimpleContainer container)
        {
            this.container = container;
        }

        public void RegisterFrame(Frame frame)
        {
            navigationService = new NavigationService(frame);
            container.Instance(navigationService);

            navigationService.NavigateToViewModel(typeof(LoginViewModel));
        }

        public string Title
        {
            get { return title; }
        }
    }
}
