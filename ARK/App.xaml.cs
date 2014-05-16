﻿using System;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

using ARK.HelperFunctions;
using ARK.HelperFunctions.SMSGateway;
using ARK.Model;
using ARK.Model.DB;
using ARK.Model.XML;

namespace ARK
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Constructors and Destructors

        public App()
        {
#if RELEASE || DEBUG

            // Start thread which downloads the sunset time every day
            var wtoken = new CancellationTokenSource();
            SunsetClass.StartSunsetTask(wtoken.Token);
#endif

            // Thread that checks if a new season needs to be started
            var checkForNewSeasonThread = new Thread(
                () =>
                    {
                        while (true)
                        {
                            DateTime tomorrowAtTree = new DateTime(
                                DateTime.Now.Year, 
                                DateTime.Now.Month, 
                                DateTime.Now.Day, 
                                3, 
                                00, 
                                00);

                            tomorrowAtTree = tomorrowAtTree.AddDays(1);

                            TimeSpan TimeToTreeOCloc = tomorrowAtTree - DateTime.Now;

                            try
                            {
                                Thread.Sleep((int)TimeToTreeOCloc.TotalMilliseconds); // sleep until 3.00 hours
                            }
                            catch (ThreadInterruptedException)
                            {
                                break;
                            }

                            this.CheckCurrentSeasonEnd();
                        }
                    });
            checkForNewSeasonThread.Start();

            // CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("da-DK");
            // CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("da-DK");

            Current.ShutdownMode = System.Windows.ShutdownMode.OnLastWindowClose;

            WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
            Current.Exit += (sender, e) =>
                {
                    if (windowsIdentity != null
                        && (windowsIdentity.Name == "SAHB-WIN7\\sahb" || windowsIdentity.Name == "Jonas-BB\\Jonas"))
                    {
                        // KILL IT!
                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                    }

                    if (checkForNewSeasonThread.ThreadState == ThreadState.Running
                        || checkForNewSeasonThread.ThreadState == ThreadState.WaitSleepJoin)
                    {
                        checkForNewSeasonThread.Interrupt();
                    }
                };
        }

        #endregion

        #region Public Methods and Operators

        public static T GetChildOfType<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null)
            {
                return null;
            }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = (child as T) ?? GetChildOfType<T>(child);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        #endregion

        #region Methods

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            EventManager.RegisterClassHandler(
                typeof(DatePicker), 
                DatePicker.LoadedEvent, 
                new RoutedEventHandler(this.DatePicker_Loaded));
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
#if AdministrationsSystem
            this.StartupUri = new Uri("/View/Administrationssystem/AdminSystem.xaml", UriKind.Relative);
#else
#if DEBUG
            this.StartupUri = new Uri("/MainWindow.xaml", UriKind.Relative);
#else
            this.StartupUri = new Uri("/View/Protokolsystem/ProtocolSystem.xaml", UriKind.Relative);
    #endif
#endif
        }

        private void CheckCurrentSeasonEnd()
        {
            using (var db = new DbArkContext())
            {
                Season currentSeason;

                // Get current season
                if (!db.Season.Any(x => true))
                {
                    currentSeason = new Season();
                    db.Season.Add(currentSeason);
                }
                else
                {
                    currentSeason = db.Season.AsEnumerable().Last(x => true);
                }

                // if current seasonEnd is before today add new season.
                if (DateTime.Compare(currentSeason.SeasonEnd, DateTime.Now) <= 0)
                {
                    currentSeason = new Season();
                    db.Season.Add(currentSeason);
                    db.SaveChanges();
                }
            }
        }

        // Opdater Watermark
        // http://matthamilton.net/datepicker-watermark
        private void DatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            var dp = sender as DatePicker;
            if (dp == null)
            {
                return;
            }

            var tb = GetChildOfType<DatePickerTextBox>(dp);
            if (tb == null)
            {
                return;
            }

            var wm = tb.Template.FindName("PART_Watermark", tb) as ContentControl;
            if (wm == null)
            {
                return;
            }

            wm.Content = "Vælg en dato";
        }

        #endregion
    }
}