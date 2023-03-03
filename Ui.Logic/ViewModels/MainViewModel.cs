using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Ui.Logic.ViewModel {

    public class MainViewModel : ViewModelBase {

        public string WindowTitle { get; set; }
        public MainViewModel() {
            if (IsInDesignMode) {
                WindowTitle = "Videothek (Designer-Ansicht)";
            } else {
                WindowTitle = "Videothek";
            }

            YouAreOffline = Visibility.Collapsed;
            IsUpdatePopupHidden = true;
            CurrentAppVersion = "1.0";
            try {
                Service service = new Service();
                media = new ObservableCollection<MediaList>();
                foreach (var item in service.getAllMedia()) {
                    media.Add(new MediaList(item.Id, item.Title, item.LeasePrice, item.Amount, item.Category.ToString()));
                }
            } catch (Exception ex) {
                MessageBox.Show("Es ist ein Fehler beim Abrufen der Daten aufgetreten.\n\n" + ex.Message, "Upsi!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            

            SearchForUpdates();
            

            UserName = "@benutzerName2023";
            FullName = "Benutzer Name";
        }


        private Visibility _YouAreOffline { get; set; }
        public Visibility YouAreOffline { get { return _YouAreOffline; } set { _YouAreOffline = value; RaisePropertyChanged(); } }


        private ICommand _HideOfflineField;
        public ICommand HideOfflineField {
            get {
                if (_HideOfflineField == null) {
                    _HideOfflineField = new RelayCommand(() => {
                        YouAreOffline = Visibility.Collapsed;
                    });
                }
                return _HideOfflineField;
            }
        }

        private async void SearchForUpdates() {
            await System.Threading.Tasks.Task.Delay(3000);
            await System.Threading.Tasks.Task.Run(() => {
                using (videothek_development db = new videothek_development()) {
                    try { 
                        var query = db.AppUpdates.Where(f => f.Channel == 0).OrderByDescending(u => u.VersionId).FirstOrDefault();
                        if (query != null) {
                            if(query.Version.Replace(" ", "") != CurrentAppVersion) {
                                IsUpdatePopupHidden = false;
                            }
                        }
                    } catch {
                        YouAreOffline = Visibility.Visible;
                    }

                }
            });
        }

        private string _CurrentAppVersion { get; set; }
        public string CurrentAppVersion { get { return _CurrentAppVersion; } set { _CurrentAppVersion = value; RaisePropertyChanged(); } }
        private bool _IsUpdatePopupHidden { get; set; }
        public bool IsUpdatePopupHidden { get { return _IsUpdatePopupHidden; } set { _IsUpdatePopupHidden = value; RaisePropertyChanged(); } }


        private List<string> _MediaSelection { get; set; }
        public List<string> MediaSelection { get { return _MediaSelection; } set { _MediaSelection = value; RaisePropertyChanged(); } }

        private ICommand _ShowLogin;
        public ICommand ShowLogin {
            get {
                if (_ShowLogin == null) {
                    _ShowLogin = new RelayCommand(() => {
                        //Logic here
                        MessageBox.Show("TODO: Kunden-Login-Fenster soll erscheinen");
                    });
                }
                return _ShowLogin;
            }
        }

        private ICommand _ShowLoginEmp;
        public ICommand ShowLoginEmp {
            get {
                if (_ShowLoginEmp == null) {
                    _ShowLoginEmp = new RelayCommand(() => {
                        //Logic here
                        MessageBox.Show("TODO: Mitarbeiter-Login-Fenster soll erscheinen");
                    });
                }
                return _ShowLoginEmp;
            }
        }

        private ObservableCollection<MediaList> _media;
        public ObservableCollection<MediaList> media { get { return _media; } set { _media = value; RaisePropertyChanged(); } }
        public class MediaList {
            public MediaList(int articleId, string name, int amount, decimal leasePrice, string category) {
                Name = name;
                Kategorie = category;
                Mietpreis = leasePrice.ToString();
            }

            public string Name { get; set; }
            public string Kategorie { get; set; }
            public string Mietpreis { get; set; }
        }

        private string _UserName { get; set; }
        public string UserName { get { return _UserName; } set { _UserName = value; RaisePropertyChanged(); } }
        private string _FullName { get; set; }
        public string FullName  { get { return _FullName; } set { _FullName = value; RaisePropertyChanged(); } }


        private ICommand _ShowAbout;
        public ICommand ShowAbout {
            get {
                if (_ShowAbout == null) {
                    _ShowAbout = new RelayCommand<object>(x => {
                        Messenger.Default.Send<NavigationMessage>(new NavigationMessage("about"));
                    });
                }
                return _ShowAbout;
            }
        }

        private ICommand _ShowUpdater;
        public ICommand ShowUpdater {
            get {
                if (_ShowUpdater == null) {
                    _ShowUpdater = new RelayCommand<object>(x => {
                        Messenger.Default.Send<NavigationMessage>(new NavigationMessage("updater"));
                    });
                }
                return _ShowUpdater;
            }
        }

        private ICommand _ShowUserManager;
        public ICommand ShowUserManager {
            get {
                if (_ShowUpdater == null) {
                    _ShowUserManager = new RelayCommand<object>(x => {
                        Messenger.Default.Send<NavigationMessage>(new NavigationMessage("usermanager"));
                    });
                }
                return _ShowUserManager;
            }
        }


        private ICommand _ShowLoginWindow;
        public ICommand ShowLoginWindow {
            get {
                if (_ShowLoginWindow == null) {
                    _ShowLoginWindow = new RelayCommand<object>(x => {
                        Messenger.Default.Send<NavigationMessage>(new NavigationMessage("login"));
                    });
                }
                return _ShowLoginWindow;
            }
        }


        private ICommand _ShowLogoutWindow;
        public ICommand ShowLogoutWindow {
            get {
                if (_ShowLogoutWindow == null) {
                    _ShowLogoutWindow = new RelayCommand<object>(x => {
                        Messenger.Default.Send<NavigationMessage>(new NavigationMessage("logout"));
                    });
                }
                return _ShowLogoutWindow;
            }
        }


        private ICommand _ShowRegistrationWindow;
        public ICommand ShowRegistrationWindow {
            get {
                if (_ShowRegistrationWindow == null) {
                    _ShowRegistrationWindow = new RelayCommand<object>(x => {
                        Messenger.Default.Send<NavigationMessage>(new NavigationMessage("register"));
                    });
                }
                return _ShowRegistrationWindow;
            }
        }

        private ICommand _ShowMediaManager;
        public ICommand ShowMediaManager {
            get {
                if (_ShowMediaManager == null) {
                    _ShowMediaManager = new RelayCommand<object>(x => {
                        Messenger.Default.Send<NavigationMessage>(new NavigationMessage("mediamanager"));
                    });
                }
                return _ShowMediaManager;
            }
        }

        private ICommand _ShowAllArticles;
        public ICommand ShowAllArticles {
            get {
                if (_ShowAllArticles == null) {
                    _ShowAllArticles = new RelayCommand<object>(x => {
                        Messenger.Default.Send<NavigationMessage>(new NavigationMessage("alleArtikel"));
                    });
                }
                return _ShowAllArticles;
            }
        }
    }

    class Service {

        public List<Medium> getAllMedia() {

            var MedienListe = new List<Medium>();
                
                using (videothek_development db = new videothek_development()) {

                // Original query
                //var query = from a in db.Article
                //            select new { a.ArticleId,
                //                        a.Name,
                //                        a.Amount,
                //                        a.LeasePrice,
                //                        a.CategoryId };

                // Quelle: ChatGPT (hat die query umgeschrieben und so verändert, dass die Elemente zufällig und maximal 10 davon selected.)
                // Select random articles
                var query = db.Article
                              .OrderBy(x => Guid.NewGuid()) // Order by a random value
                              .Select(a => new {
                                  a.ArticleId,
                                  a.Name,
                                  a.Amount,
                                  a.LeasePrice,
                                  a.CategoryId
                              })
                              .Take(10); // Select a maximum of 10 random articles


                int tempCategoryInt;
                foreach (var item in query) {
                    tempCategoryInt = item.CategoryId;
                    MedienListe.Add(new Medium() {
                        Id = Convert.ToInt32(item.ArticleId),
                        Title = item.Name,
                        Amount = Convert.ToInt32(item.Amount),
                        LeasePrice = Convert.ToInt32(item.LeasePrice),
                        Category = db.Category
                            .Where(c => c.CategoryId.Equals(tempCategoryInt))
                            .Select(c => c.Name)
                            .SingleOrDefault()
                    });
                }
            }
            return MedienListe;
        }
    }

    class Medium {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Amount { get; set; }
        public int LeasePrice { get; set;}
        public string Category { get; set;}
    }
}