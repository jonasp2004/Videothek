namespace Ui.Logic.ViewModel {

    public class NavigationMessage {

        public string Page { get; set; }
        public NavigationMessage(string page)
        {
            Page = page;
        }
    }

    public class UIMessage {
        public string Action { get; set; }
        public UIMessage(string action) {
            Action = action;
        }
    }

    public class AccountAction {
        public string Action { get; set; }
        public AccountAction(string action) {
            Action = action;
        }
    }

    public class OpenContentDetail {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string Price { get; set; }
        public OpenContentDetail(int id, string itemName, string price) {
            Id = id;
            ItemName = itemName;
            Price = price;
        }
    }
}