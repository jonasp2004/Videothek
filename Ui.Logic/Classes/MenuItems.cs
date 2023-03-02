using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Ui.Logic.ViewModel;

namespace Ui.Logic.Classes {
    public class ListItems : ViewModelBase {
        public string ItemName { get; set; }
        public string ItemCategory { get; set; }
        public string ItemPrice { get; set; }
        public int ItemId { get; set; }

        public ListItems(string itemName, string itemCategory, string itemPrice, int itemId) {
            ItemName = itemName;
            ItemCategory = itemCategory;
            ItemPrice = itemPrice;
            ItemId = itemId;
        }

        private ICommand _ButtonClicked;
        public ICommand ComboBoxCategorySelected {
            get {
                if (_ButtonClicked == null) {
                    _ButtonClicked = new RelayCommand(() => {
                        Messenger.Default.Send<OpenContentDetail>(new OpenContentDetail(ItemId, ItemName, ItemPrice));
                    });
                }
                return _ButtonClicked;
            }
        }

    }
}