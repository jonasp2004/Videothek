using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ui.Logic.ViewModel {

    public class UpdateViewModel : ViewModelBase {

        public string WindowTitle { get; set; }
        public UpdateViewModel() {
            if (IsInDesignMode) {
                WindowTitle = "Updates (Designer-Ansicht)";
            } else {
                WindowTitle = "Updates";
            }
            waitingVisible = Visibility.Collapsed;
            ChannelSelect = 0;
            UpdateReleaseDate = "Veröffentlicht am: k.A.";
            Patchnotes = "Patchnotes: k.A.";
            latestVersion = "Aktuellste Version: k.A.";
            versionDetails[2] = "";
        }

        private ICommand _PrepareInstallation;
        public ICommand PrepareInstallation {
            get {
                if (_PrepareInstallation == null) {
                    _PrepareInstallation = new RelayCommand(() => {
                        SearchingDesc = "Downloade neuste Version...\nBitte warte einen Moment!";
                        IsProgressBarIndetemiate = true;
                        ProgressVarValue = 0;
                        InstallUpdate();
                    });
                }
                return _PrepareInstallation;
            }
        }

        private async void InstallUpdate() {
            blurActivate = true;
            waitingVisible = Visibility.Visible;
            try {
                if (versionDetails[2] == "" || versionDetails[2] == null) {
                    MessageBox.Show("Hast du überhaupt auf eine neue Version geprüft?", "So nicht!", MessageBoxButton.OK, MessageBoxImage.Stop);
                } else {
                    await Install();
                    MessageBox.Show("Das Update wurde erfolgreich heruntergeladen. Bitte installieren Sie das Paket. Es wurde im Ordner \"" + Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\" abgelegt.", "Update-Download erfolgreich!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            } catch (Exception ex) {
                MessageBox.Show("Es gab einen Fehler bei der Installation.\n" + ex.Message, "Ein Fehler ist aufgetreten", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            blurActivate = false;
            await Timer(200);
            waitingVisible = Visibility.Collapsed;
        }

        internal async Task Install() {
            await Task.Run(() => {
                using (WebClient wc = new WebClient()) {
                    wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                    wc.DownloadFileAsync(new Uri("http://10.33.11.55/" + versionDetails[2]), Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/" + "Videothek " + versionDetails[0] + ".exe");
                }
            });
        }

        private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) {
            ProgressVarValue = e.ProgressPercentage;
        }

        private bool _IsProgressBarIndetemiate { get; set; }
        public bool IsProgressBarIndetemiate { get { return _IsProgressBarIndetemiate; } set { _IsProgressBarIndetemiate = value; RaisePropertyChanged(); } }
        private int _ProgressVarValue { get; set; }
        public int ProgressVarValue { get { return _ProgressVarValue; } set { _ProgressVarValue = value; RaisePropertyChanged(); } }



        private ICommand _PrepareSearch;
        public ICommand PrepareSearch {
            get {
                if (_PrepareSearch == null) {
                    _PrepareSearch = new RelayCommand(() => {
                        SearchingDesc = "Sucht nach Updates...\nBitte warte einen Moment!";
                        ProgressVarValue = 0;
                        IsProgressBarIndetemiate = true;
                        SearchUpdate();
                    });
                }
                return _PrepareSearch;
            }
        }

        private string _Patchnotes { get; set; }
        public string Patchnotes { get { return _Patchnotes; } set { _Patchnotes = value; RaisePropertyChanged(); } }
        private string _UpdateReleaseDate { get; set; }
        public string UpdateReleaseDate { get { return _UpdateReleaseDate; } set { _UpdateReleaseDate = value; RaisePropertyChanged(); } }

        private string _UpdaterHelpText { get; set; }
        public string UpdaterHelpText { get { return _UpdaterHelpText; } set { _UpdaterHelpText = value; RaisePropertyChanged(); } }

        public string[] versionDetails = new string[4];
        private async void SearchUpdate() {
            blurActivate = true;
            waitingVisible = Visibility.Visible;

            // Sucht hier wirklich nach updates
            Version ver = new Version();
            await ver.SearchForUpdate(versionDetails, ChannelSelect);
            latestVersion = "Aktuellste Version: " + versionDetails[0];
            Patchnotes = "Patchnotes: " + versionDetails[1];
            UpdateReleaseDate = "Veröffentlicht am: " + versionDetails[3];
            Messenger.Default.Send<NavigationMessage>(new NavigationMessage("updateTheHelperText!"));
            blurActivate = false;
            await Timer(200);
            waitingVisible = Visibility.Collapsed;
        }

        internal async Task Timer(int duration) { await Task.Delay(duration); }


        private int _ChannelSelect { get; set; }
        public int ChannelSelect { get { return _ChannelSelect; } set { _ChannelSelect = value; RaisePropertyChanged(); } }
        private string _SearchingDesc { get; set; }
        public string SearchingDesc { get { return _SearchingDesc; } set { _SearchingDesc = value; RaisePropertyChanged(); } }

        private string _latestVersion { get; set; }
        public string latestVersion { get { return _latestVersion; } set { _latestVersion = value; RaisePropertyChanged(); } }


        private bool _blurActivate { get; set; }
        public bool blurActivate { get { return _blurActivate; } set { _blurActivate = value; RaisePropertyChanged(); } }
        private Visibility _waitingVisible { get; set; }
        public Visibility waitingVisible { get { return _waitingVisible; } set { _waitingVisible = value; RaisePropertyChanged(); } }
    }

    class Version {

        public async Task SearchForUpdate(string[] versionDetails, int channel) {
            await Task.Run(() => {
                using (videothek_development db = new videothek_development()) {
                    try {
                        var query = db.AppUpdates.Where(f => f.Channel == channel).OrderByDescending(u => u.VersionId).FirstOrDefault();
                        if(query != null) {
                            versionDetails[0] = query.Version.Replace(" ", "");
                            versionDetails[1] = query.PatchNotes;
                            versionDetails[2] = query.EXE_Name.Replace(" ", "");
                            versionDetails[3] = query.ReleaseDate.ToString();
                        } else {
                            MessageBox.Show("Es existiert derzeit kein Beta-Update", "Upsi!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    } catch (Exception ex) { 
                        MessageBox.Show("Während des Updates ist uns ein kleiner Fehler unterlaufen.\n\n" + ex.Message, "Upsi!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    
                }
            });
        }
    }
}