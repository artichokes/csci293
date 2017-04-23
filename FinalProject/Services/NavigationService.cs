using Caliburn.Micro;
using FinalProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FinalProject.Services
{
    public class NavigationService
    {
        private readonly Frame frame;

        public NavigationService(Frame frame)
        {
            this.frame = frame;
        }

        public void NavigateToViewModel(Type viewModelType)
        {
            var view = ViewLocator.LocateForModelType(viewModelType, null, null);
            var viewModel = ViewModelLocator.LocateForView(view);

            ViewModelBinder.Bind(viewModel, view, null);

            var activator = viewModel as IActivate;

            activator?.Activate();

            frame.Navigate(view);
        }
    }
}
