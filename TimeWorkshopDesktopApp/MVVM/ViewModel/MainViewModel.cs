﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeWorkshopDesktopApp.Core;

namespace TimeWorkshopDesktopApp.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand TimeViewCommand { get; set; }
        public RelayCommand TimePeriodCommand { get; set; }


        public HomeViewModel HomeVM { get; set; }
        public TimeViewModel TimeVM { get; set; }
        public TimePeriodViewModel TimePeriodVM { get; set; }

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

            TimePeriodCommand = new RelayCommand(o =>
            { 
                CurrentView = TimePeriodVM;
            }
            );

        }
    }
}