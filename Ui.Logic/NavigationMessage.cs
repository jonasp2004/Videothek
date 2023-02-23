namespace Ui.Logic.ViewModel {

    public class NavigationMessage {

        public string Page { get; set; }
        public NavigationMessage(string page) {
            Page = page;
        }
    }
}