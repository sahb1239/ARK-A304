﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ARK.Model;
using ARK.Model.DB;
using ARK.View.Protokolsystem.Additional;
using ARK.View.Protokolsystem.Pages;
using ARK.ViewModel.Protokolsystem.Additional;
using ARK.ViewModel.Protokolsystem.Data;

namespace ARK.ViewModel.Protokolsystem.Pages
{
    public class CreateLongTripViewModel : ProtokolsystemContentViewModelBase
    {
        // Fields

        private readonly ObservableCollection<MemberViewModel> _selectedMembers =
            new ObservableCollection<MemberViewModel>(); // Members in boat

        private List<LongTripForm> _longTripForms;
        private List<Member> _members = new List<Member>();
        private List<MemberViewModel> _membersFiltered;
        private List<Boat> _boats;
        private Boat _selectedBoat;
        private FrameworkElement _infoPage;

        // Constructor
        public CreateLongTripViewModel()
        {
            DbArkContext db = DbArkContext.GetDbContext();

            // Load of data
            ParentAttached += (sender, e) =>
            {
                _members = db.Member.OrderBy(x => x.FirstName).ToList();
                MembersFiltered = _members.Select(member => new MemberViewModel(member)).ToList();

                // get long trip forms
                LongTripForms = db.LongTripForm.OrderBy(x => x.FormCreated).Where(x => true).ToList();
                Boats = db.Boat.Where(x => x.Active).ToList();

                // Set info
                UpdateInfo();
            };
        }

        // Properties
        public DateTime? PlannedStartDate { get; set; }
        public DateTime? PlannedEndDate { get; set; }
        public List<Boat> Boats
        {
            get { return _boats; }
            set { _boats = value; Notify(); }
        }
        
        public Boat SelectedBoat
        {
            get { return _selectedBoat; }
            set
            {
                _selectedBoat = value;
                Notify();
                UpdateInfo();
            }
        }

        public string TourDescription { get; set; }
        public string DistancesPerDay { get; set; }
        public string CampSites { get; set; }

        public List<MemberViewModel> MembersFiltered
        {
            get { return _membersFiltered; }
            set
            {
                _membersFiltered = value;
                Notify();
            }
        }

        public ObservableCollection<MemberViewModel> SelectedMembers
        {
            get { return _selectedMembers; }
        }

        public List<LongTripForm> LongTripForms
        {
            get { return _longTripForms; }
            set
            {
                _longTripForms = value;
                Notify();
            }
        }

        public ICommand AddLongTrip
        {
            get
            {
                return GetCommand<object>(d =>
                {
                    var db = DbArkContext.GetDbContext();
                    var longTripForm = new LongTripForm
                    {
                        FormCreated = DateTime.Now,
                        PlannedStartDate = PlannedStartDate ?? DateTime.MinValue,
                        PlannedEndDate = PlannedEndDate ?? DateTime.MinValue,
                        Boat = SelectedBoat,
                        TourDescription = TourDescription,
                        DistancesPerDay = DistancesPerDay,
                        CampSites = CampSites,
                        Members = SelectedMembers.Select(mvm => mvm.Member).ToList(),
                        Status = LongTripForm.BoatStatus.Awaiting,
                        ResponsibleMember = Info.ResponsibleMember.Member
                    };

                    db.LongTripForm.Add(longTripForm);
                    db.SaveChanges();

                    // Returner til Kilometerstatistik.
                    ProtocolSystem.StatisticsDistance.Execute(null);
                });
            }
        }

        public ICommand ViewLongTripForm
        {
            get
            {
                return
                    GetCommand<object>(
                        a => ProtocolSystem.NavigateToPage(() => new ViewLongTripForm(), "AKTIVE LANGTURS BLANKETTER"));
            }
        }

        public ICommand AddBlanc
        {
            get
            {
                return GetCommand<object>(d =>
                {
                    if (SelectedMembers.Count < SelectedBoat.NumberofSeats)
                    {
                        SelectedMembers.Add(new MemberViewModel(new Member() { Id = -1, FirstName = "Blank" }));
                    }
                });
            }
        }

        public ICommand AddGuest
        {
            get
            {
                return GetCommand<object>(d =>
                {
                    if (SelectedMembers.Count < SelectedBoat.NumberofSeats)
                    {
                        SelectedMembers.Add(new MemberViewModel(new Member() { Id = -2, FirstName = "Gæst" }));
                    }
                });
            }
        }

        private FrameworkElement InfoPage
        {
            get { return _infoPage ?? (_infoPage = new CreateLongTripFormAdditionalInfo()); }
        }

        private CreateLongTripFormAdditionalInfoViewModel Info
        {
            get { return InfoPage.DataContext as CreateLongTripFormAdditionalInfoViewModel; }
        }

        private void UpdateInfo()
        {
            Info.SelectedBoat = SelectedBoat;
            Info.SelectedMembers = SelectedMembers;

            ProtocolSystem.ChangeInfo(InfoPage, Info);
        }
    }
}