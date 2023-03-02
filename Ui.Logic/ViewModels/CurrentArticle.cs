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
using System.Windows.Media.Imaging;

namespace Ui.Logic.ViewModel {

    public class CurrentArticleViewModel : ViewModelBase {

        public string WindowTitle { get; set; }
        public CurrentArticleViewModel() {
            if (IsInDesignMode) {
                WindowTitle = "Details über gewählten Inhalt (Designer-Ansicht)";
            } else {
                WindowTitle = "Details über gewählten Inhalt";
            }
            try {
                FetchData();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Ein Fehler ist aufgetreten", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private ICommand _PrepareInstallation;
        public ICommand PrepareInstallation {
            get {
                if (_PrepareInstallation == null) {
                    _PrepareInstallation = new RelayCommand(() => {
                    });
                }
                return _PrepareInstallation;
            }
        }

        private string _Artikelnummer { get; set; }
        public string Artikelnummer { get { return _Artikelnummer; } set { _Artikelnummer = value; RaisePropertyChanged(); } }
        private BitmapImage _ImageSource { get; set; }
        public BitmapImage ImageSource { get { return _ImageSource; } set { _ImageSource = value; RaisePropertyChanged(); } }
        private string _Artikelbeschreibung { get; set; }
        public string Artikelbeschreibung { get { return _Artikelbeschreibung; } set { _Artikelbeschreibung = value; RaisePropertyChanged(); } }
        private string _Artikeltitel { get; set; }
        public string Artikeltitel { get { return _Artikeltitel; } set { _Artikeltitel = value; RaisePropertyChanged(); } }
        private string _Artikelpreis { get; set; }
        public string Artikelpreis { get { return _Artikelpreis; } set { _Artikelpreis = value; RaisePropertyChanged(); } }
        private string _ArtikelVerfügbar { get; set; }
        public string ArtikelVerfügbar { get { return _ArtikelVerfügbar; } set { _ArtikelVerfügbar = value; RaisePropertyChanged(); } }

        public async void FetchData() {
            try {
                Artikelbeschreibung = "Bitte warte, es lädt...";
                Artikeltitel = "Daten werden abgerufen...";
                int artnr = 1;
                CurrentArticle currArt = new CurrentArticle();
                await Task.Delay(1000);
                artnr = Convert.ToInt32(Artikelnummer.Replace("Art.-Nummer: ", "").Replace(" ", ""));
                await Task.Run(() => { 
                    using(videothek_development db =  new videothek_development()) {
                        var query = db.Article.Select(a => new { a.ArticleId, a.Name, a.Amount, a.LeasePrice, a.CategoryId, a.Description }).Where(o => o.ArticleId.Equals(artnr)).Take(1);
                        foreach (var item in query) {
                            currArt.ArticleId = Convert.ToInt32(item.ArticleId);
                            currArt.ArticleTitle = item.Name;
                            if(item.Description != null) {
                                currArt.ArticleDescription = item.Description;
                            } else {
                                currArt.ArticleDescription = "(Entschuldigung, dieser Artikel hat leider noch keine Beschreibung)";
                            }
                        
                            currArt.ArticleAmount = Convert.ToInt32(item.Amount);
                            currArt.ArticlePrice = Convert.ToString(item.LeasePrice);
                            currArt.ArticleCategory = db.Category
                                .Where(c => c.CategoryId.Equals(item.CategoryId))
                                .Select(c => c.Name)
                                .SingleOrDefault();
                            break;
                        }
                    }
                });
                if(currArt.ArticleAmount == 0) {
                    ArtikelVerfügbar = "Dieser Artikel ist leider\nmomentan vergriffen!";
                } else if (currArt.ArticleAmount <= 5) {
                    ArtikelVerfügbar = "Nur noch " + currArt.ArticleAmount.ToString() + " verfügbar!";
                } else {
                    ArtikelVerfügbar = "Dieser Artikel ist verfügbar!";
                }
                Artikelnummer = "Art.-Nummer: " + currArt.ArticleId.ToString();
                Artikeltitel = currArt.ArticleTitle;
                Artikelbeschreibung = currArt.ArticleDescription;
                Artikelpreis = currArt.ArticlePrice;
            } catch {
                Artikelbeschreibung = "Entschuldigung, aber wir konnten leider nicht alle Daten abfangen!";
            }
            
        }

        public class CurrentArticle {
            public int ArticleId { get; set; }
            public string ArticleTitle { get; set; }
            public string ArticleDescription { get; set; }
            public int ArticleAmount { get; set; }
            public string ArticlePrice { get; set; }
            public string ArticleCategory { get; set; }
        }

    }
}