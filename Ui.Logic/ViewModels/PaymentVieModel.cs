using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using System.Threading;
using System.Windows;

namespace Ui.Logic.ViewModel {

    public class PaymentViewModel : ViewModelBase {

        public PaymentViewModel() {
            if (IsInDesignMode) {
                Title = "Ausleihe bestätigen (Designansicht)";
                ContentTitle = "Findet Nemo";
                ContentPrice = "8 EUR inkl. 19% MwSt. + 4 EUR Versand über Deutsche Post/DHL\nAusleihe gültig für zwei Wochen";
            } else {
                Title = "Ausleihe bestätigen";
            }
            //FetchData();
        }

        private int _ContentId { get; set; }
        public int ContentId { get { return _ContentId; } set { _ContentId = value; RaisePropertyChanged(); } }
        private int _UserId { get; set; }
        public int UserId { get { return _UserId; } set { _UserId = value; RaisePropertyChanged(); } }

        private string _Title { get; set; }
        public string Title { get { return _Title; } set { _Title = value; RaisePropertyChanged(); } }
        private string _ContentTitle { get; set; }
        public string ContentTitle { get { return _ContentTitle; } set { _ContentTitle = value; RaisePropertyChanged(); } }
        private string _ContentPrice { get; set; }
        public string ContentPrice { get { return _ContentPrice; } set { _ContentPrice = value; RaisePropertyChanged(); } }
        private bool _IsFrameBlurred { get; set; }
        public bool IsFrameBlurred { get { return _IsFrameBlurred; } set { _IsFrameBlurred = value; RaisePropertyChanged(); } }


        private string _UserFullName { get; set; }
        public string UserFullName { get { return _UserFullName; } set { _UserFullName = value; RaisePropertyChanged(); } }
        private string _UserFullStreet { get; set; }
        public string UserFullStreet { get { return _UserFullStreet; } set { _UserFullStreet = value; RaisePropertyChanged(); } }
        private string _UserFullLocation { get; set; }
        public string UserFullLocation { get { return _UserFullLocation; } set { _UserFullLocation = value; RaisePropertyChanged(); } }

        public async void FetchData() {
            try {
                IsFrameBlurred = true;
                ContentTitle = "Inahlt wird geladen...";
                ContentPrice = "Bitte warte einen Moment. Informationen werden abgerufen...";

                UserFullName = "Lade Benutzerinfos: Voller Name...";
                UserFullStreet = "Lade Benutzerinfos: Straße...";
                UserFullStreet = "Lade Benutzerinfos: Ort...";
                await Task.Run(() => {
                    using(videothek_development db = new videothek_development()) {
                        Thread.Sleep(500);
                        var article = db.Article.Select(a => new { a.ArticleId, a.LeasePrice, a.Amount, a.Name }).Where(i => i.ArticleId.Equals(ContentId)).FirstOrDefault();
                        ContentTitle = article.Name.ToString();
                        ContentPrice = article.LeasePrice.ToString() + " EUR inkl. 19% MwSt. + 4 EUR Versand über Deutsche Post/DHL\nAusleihe gültig für zwei Wochen";

                        var customer = db.Customer.Select(a => new { a.Id, a.FirstName, a.LastName, a.Street, a.HouseNumber, a.Zip, a.Location}).Where(c => c.Id.Equals(UserId)).FirstOrDefault();
                        UserFullName = customer.FirstName + " " + customer.LastName;
                        UserFullStreet = customer.Street + " " + customer.HouseNumber;
                        UserFullLocation = customer.Zip + " " + customer.Location;
                    }
                });
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            } finally {
                IsFrameBlurred = false;
            }
            
        }

        private ICommand _FrameLoaded;
        public ICommand FrameLoaded {
            get {
                if (_FrameLoaded == null) {
                    _FrameLoaded = new RelayCommand(() => {
                        FetchData();
                    });
                }
                return _FrameLoaded;
            }
        }

        private async void Send() {
            Messenger.Default.Send<UIMessage>(new UIMessage("paymentInProgress"));
            IsFrameBlurred = true;
            await System.Threading.Tasks.Task.Delay(3000);
            IsFrameBlurred = false;
            Messenger.Default.Send<UIMessage>(new UIMessage("paymentProgressDone"));
        }
    }
}