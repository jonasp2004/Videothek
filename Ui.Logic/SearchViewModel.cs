using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Office2021.DocumentTasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows;

namespace Ui.Logic.ViewModel {

    public class SearchViewModel : ViewModelBase {

        public string WindowTitle { get; set; }
        public SearchViewModel() {
            if (IsInDesignMode) {
                WindowTitle = "Suche (Designer-Ansicht)";
            } else {
                WindowTitle = "Suche";
            }
            LoadingGridVisible = Visibility.Visible;
            ObservableCollection<MediaItem> mediaCollection = new ObservableCollection<MediaItem>();
            media = mediaCollection;
        }


        private Visibility _LoadingGridVisible { get; set; }
        public Visibility LoadingGridVisible { get { return _LoadingGridVisible; } set { _LoadingGridVisible = value; RaisePropertyChanged(); } }

        private string _Anfrage;
        public string Anfrage { get { return _Anfrage; } set { _Anfrage = value; RaisePropertyChanged(); } }
        private ICommand _SearchClicked;
        public ICommand SearchClicked {
            get {
                if (_SearchClicked == null) {
                    _SearchClicked = new RelayCommand(() => {
                        LoadingGridVisible = Visibility.Visible;
                        if(media == null) {
                            ObservableCollection<MediaItem> media = new System.Collections.ObjectModel.ObservableCollection<MediaItem>();
                        }
                        media.Clear();
                        ObservableCollection<MediaItem> mediaCollection = new ObservableCollection<MediaItem>();
                        foreach (var item in SearchFor(Anfrage, mediaCollection)) {
                            media.Add(item);
                        }
                        mediaCollection.Clear();
                        LoadingGridVisible = Visibility.Collapsed;
                    });
                }
                return _SearchClicked;
            }
        }


        public ObservableCollection<MediaItem> SearchFor(string request, ObservableCollection<MediaItem> mediaCol) {
            mediaCol.Clear();
                using(videothek_development db  = new videothek_development()) {
                    var query = db.Article
                    .Select(n => new { n.Name, n.ArticleId, n.CategoryId, n.Description, n.LeasePrice })
                    .Where(q => q.Name.Contains(request) || q.Description.Contains(request));

                foreach (var item in query) {
                    int artnbr = Convert.ToInt32(item.ArticleId);
                    string category = db.Category
                            .Where(c => c.CategoryId.Equals(item.CategoryId))
                            .Select(c => c.Name)
                            .SingleOrDefault();
                    mediaCol.Add(new MediaItem() {
                        Artikelnummer = artnbr,
                        Titel = item.Name,
                        Kategorie = category,
                        Leihpreis = item.LeasePrice.ToString()
                    });
                    }
                }
            return mediaCol;
        }


        private ObservableCollection<MediaItem> _media;
        public ObservableCollection<MediaItem> media { get { return _media; } set { _media = value; RaisePropertyChanged(); } }
        public class MediaItem {
            public int Artikelnummer { get; set; }
            public string Titel { get; set; }
            public string Kategorie { get; set; }
            public string Leihpreis { get; set; }
        }

    }
}