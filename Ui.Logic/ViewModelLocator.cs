/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Ui.Desktop"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace Ui.Logic.ViewModel {

    public class ViewModelLocator {
        
        public ViewModelLocator() {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<SearchViewModel>();
            SimpleIoc.Default.Register<UsermanagerViewModel>();
            SimpleIoc.Default.Register<MediamanagementViewModel>();
        }
        
        public MainViewModel Main {
            get {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        
        public LoginViewModel Login {
            get {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }
        public UsermanagerViewModel Usermanager {
            get {
                return ServiceLocator.Current.GetInstance<UsermanagerViewModel>();
            }
        }

        public SearchViewModel search { 
            get {
                return ServiceLocator.Current.GetInstance<SearchViewModel>();
            }
        }

        public MediamanagementViewModel MediaManagement {
            get {
                return ServiceLocator.Current.GetInstance<MediamanagementViewModel>();
            }
        }


        //public LoginViewModel Login {
        //    get {
        //        return ServiceLocator.Current.GetInstance<LoginViewModel>();
        //    }
        //}

        public static void Cleanup() {
            // TODO Clear the ViewModels
        }

        public string WindowTitle { get; set; }
    }
}