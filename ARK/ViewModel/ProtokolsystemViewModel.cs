﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ARK.ViewModel
{
    class ProtokolsystemViewModel : INotifyPropertyChanged
    {
        public string HeadlineText
        {
            get { return _headlineText; }
            set { _headlineText = value; Notify("HeadlineText"); }
        }

        public UserControl CurrentPage
        {
            get
            {
                return _currentPage;
            }
            private set
            {
                _currentPage = value;
                Notify("CurrentPage");
            }
        }

        #region Commands
        public ICommand StartRotur { 
            get {
                return GenerateCommand("Start rotur", PageBeginTripBoats);
            }
        }
		
		public ICommand AfslutRotur { 
            get {
                return GenerateCommand("Afslut rotur", PageEndTrip);
            }
        }
		
		public ICommand BaadePaaVandet { 
            get {
                return GenerateCommand("Både på vandet", PageBoatsOut);
            }
        }
		
		public ICommand Statistik { 
            get {
                return GenerateCommand("Kilometerstatistik", PageDistanceStatistics);
            }
        }
		
		public ICommand MedlemsInformation { 
            get {
                return GenerateCommand("Medlemsinformation", PageMembersInformation);
            }
        }
		
		public ICommand OpretSkade { 
            get {
                return GenerateCommand("Opret blanket ► Skade", PageCreateInjury);
            }
        }
		
		public ICommand OpretLangtur { 
            get {
                return GenerateCommand("Opret blanket ► Langtur", PageCreateLongDistance);
            }
        }
        #endregion

        #region Pages
        // TODO: Implementer noget cache på objekterne
        public Protokolsystem.BeginTripBoats PageBeginTripBoats { get { return new Protokolsystem.BeginTripBoats(); } }
		public Protokolsystem.EndTrip PageEndTrip { get { return new Protokolsystem.EndTrip(); } }
        public Protokolsystem.BoatsOut PageBoatsOut { get { return new Protokolsystem.BoatsOut(); } }
		public Protokolsystem.DistanceStatistics PageDistanceStatistics { get { return new Protokolsystem.DistanceStatistics(); } }
		public Protokolsystem.MembersInformation PageMembersInformation { get { return new Protokolsystem.MembersInformation(); } }
		public Protokolsystem.CreateInjury PageCreateInjury { get { return new Protokolsystem.CreateInjury(); } }
		public Protokolsystem.CreateLongDistance PageCreateLongDistance { get { return new Protokolsystem.CreateLongDistance(); } }
        
        #endregion

        #region Private
        private string _headlineText;
        private UserControl _currentPage;
        #endregion

        public ProtokolsystemViewModel()
        {
            HeadlineText = "";
        }

        #region Property
        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
        #region Command
        private ICommand GenerateCommand(string HeadLineText, UserControl page)
        {
            return new DelegateCommand<object>((e) =>
            {
                this.HeadlineText = HeadLineText;
                CurrentPage = page;
            }, (e) => { return true; });
        }
        #endregion
    }
}
