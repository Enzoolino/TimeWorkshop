using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeWorkshopDesktopApplication.Core;

namespace TimeWorkshopDesktopApplication.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand TimeViewCommand { get; set; }
        public RelayCommand TimePeriodViewCommand { get; set; }
        public RelayCommand ClockViewCommand { get; set; }
        public RelayCommand StopWatchViewCommand { get; set; }


        public HomeViewModel HomeVM { get; set; }
        public TimeViewModel TimeVM { get; set; }
        public TimePeriodViewModel TimePeriodVM { get; set; }
        public ClockViewModel ClockVM { get; set; }
        public StopWatchViewModel StopWatchVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            TimeVM = new TimeViewModel();
            TimePeriodVM = new TimePeriodViewModel();
            ClockVM = new ClockViewModel();
            StopWatchVM = new StopWatchViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            }
            );

            TimeViewCommand = new RelayCommand(o =>
            {
                CurrentView = TimeVM;
            }
            );

            TimePeriodViewCommand = new RelayCommand(o =>
            { 
                CurrentView = TimePeriodVM;
            }
            );

            ClockViewCommand = new RelayCommand(o =>
            {
                CurrentView= ClockVM;
            }
            );

            StopWatchViewCommand = new RelayCommand(o =>
            {
                CurrentView = StopWatchVM;
            }
            );
        }
    }
}
