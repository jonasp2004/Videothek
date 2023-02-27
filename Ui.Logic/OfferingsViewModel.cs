using DocumentFormat.OpenXml.Office2021.DocumentTasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Ui.Logic.ViewModel {

    public class OfferingsViewModel : ViewModelBase {

        public string WindowTitle { get; set; }
        public OfferingsViewModel() {
            if (IsInDesignMode) {
                WindowTitle = "Alle Inhalte (Designer-Ansicht)";
            } else {
                WindowTitle = "Alle Inhalte";
            }

            BorderVisibility = Visibility.Collapsed;
            IsWindowBlurred = false;

            media = new ObservableCollection<MediaList>();

            foreach(var medium in GetData(0, mediaListing)) {
                media.Add(new MediaList(medium.Id, medium.Title, medium.Amount, medium.LeasePrice, medium.Category));
            }


            ComboBoxSelection = 0;
        }

        private List<string> _MediaSelection { get; set; }
        public List<string> MediaSelection { get { return _MediaSelection; } set { _MediaSelection = value; RaisePropertyChanged(); } }

        private ObservableCollection<MediaList> _media;
        public ObservableCollection<MediaList> media { get { return _media; } set { _media = value; RaisePropertyChanged(); } }
        public class MediaList {
            public MediaList(int articleId, string name, int amount, decimal leasePrice, string category) {
                Artikelnummer = articleId.ToString();
                Name = name;
                Typ = category;
                Preis = leasePrice.ToString() + " €";
            }

            public string Artikelnummer { get; set; }
            public string Name { get; set; }
            public string Typ { get; set; }
            public string Preis { get; set; }
        }


        private Visibility _BorderVisibility { get; set; }
        public Visibility BorderVisibility { get { return _BorderVisibility; } set { _BorderVisibility = value; RaisePropertyChanged(); } }
        private bool _IsWindowBlurred { get; set; }
        public bool IsWindowBlurred { get { return _IsWindowBlurred; } set { _IsWindowBlurred = value; RaisePropertyChanged(); } }
        private int _ComboBoxSelection { get; set; }
        public int ComboBoxSelection { get { return _ComboBoxSelection; } set { _ComboBoxSelection = value; RaisePropertyChanged(); } }

        private ICommand _ComboBoxCategorySelected;
        public ICommand ComboBoxCategorySelected {
            get {
                if (_ComboBoxCategorySelected == null) {
                    _ComboBoxCategorySelected = new RelayCommand(() => {
                        GetItemsByCategory(ComboBoxSelection);
                    });
                }
                return _ComboBoxCategorySelected;
            }
        }

        internal List<Medium> mediaListing = new List<Medium>();
        private async void GetItemsByCategory(int SelectedIndex) {
            try {
                BorderVisibility = Visibility.Visible;
                IsWindowBlurred = true;
                int selection = ComboBoxSelection;
                media.Clear();
                mediaListing.Clear();
                await System.Threading.Tasks.Task.Run(() => {
                    GetData(selection, mediaListing);
                });
                foreach (var medium in mediaListing) {
                    media.Add(new MediaList(medium.Id, medium.Title, medium.Amount, medium.LeasePrice, medium.Category));
                }
                IsWindowBlurred = false;
                BorderVisibility = Visibility.Collapsed;
            } catch {
                MessageBox.Show("Ein Fehler ist aufgetreten. Bitte überprüfe deine Internetverbindung und versuche es erneut!", "Upsi!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private List<Medium> GetData(int selection, List<Medium> mediaListing) {
            using (videothek_development db = new videothek_development()) {
                    var query = db.Article.Select(a => new { a.ArticleId, a.Name, a.Amount, a.LeasePrice, a.CategoryId });
                    int tempCategoryInt = 0;
                    switch (selection) {
                        case 1:
                            query = db.Article.Select(a => new { a.ArticleId, a.Name, a.Amount, a.LeasePrice, a.CategoryId }).Where(o => o.CategoryId.Equals(2));
                            tempCategoryInt = 2;
                            break;
                        case 2:
                            query = db.Article.Select(a => new { a.ArticleId, a.Name, a.Amount, a.LeasePrice, a.CategoryId }).Where(o => o.CategoryId.Equals(3));
                            tempCategoryInt = 3;
                            break;
                        case 3:
                            query = db.Article.Select(a => new { a.ArticleId, a.Name, a.Amount, a.LeasePrice, a.CategoryId }).Where(o => o.CategoryId.Equals(4));
                            tempCategoryInt = 4;
                            break;
                        case 4:
                            query = db.Article.Select(a => new { a.ArticleId, a.Name, a.Amount, a.LeasePrice, a.CategoryId }).Where(o => o.CategoryId.Equals(5));
                            tempCategoryInt = 5;
                            break;
                        case 5:
                            query = db.Article.Select(a => new { a.ArticleId, a.Name, a.Amount, a.LeasePrice, a.CategoryId }).Where(o => o.CategoryId.Equals(6));
                            tempCategoryInt = 6;
                            break;
                        case 6:
                            query = db.Article.Select(a => new { a.ArticleId, a.Name, a.Amount, a.LeasePrice, a.CategoryId }).Where(o => o.CategoryId.Equals(7));
                            tempCategoryInt = 7;
                            break;
                    }
                if (selection == 0) {
                    foreach (var item in query) {
                        mediaListing.Add(new Medium() {
                            Id = Convert.ToInt32(item.ArticleId),
                            Title = item.Name,
                            Amount = Convert.ToInt32(item.Amount),
                            LeasePrice = Convert.ToInt32(item.LeasePrice),
                            Category = db.Category
                                .Where(c => c.CategoryId.Equals(item.CategoryId))
                                .Select(c => c.Name)
                                .SingleOrDefault()
                        });
                    }
                } else {
                    foreach (var item in query) {
                        mediaListing.Add(new Medium() {
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
                    
            }
            return mediaListing;
        }
    } 
}