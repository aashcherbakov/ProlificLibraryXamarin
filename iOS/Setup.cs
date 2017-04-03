using System;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Core.ViewModels;

namespace ProlificLibrary.iOS
{
	/// <summary>
	/// Every MvvmCross UI project needs a setup class.
	/// For iOS, inherit from MvxIosSetup
	/// 
	/// Initializes:
	/// - IoC system
	/// - MvvmCross data binding
	/// - App class and collection of ViewModels
	/// - UI project and collection of Views
	/// </summary>
    public class Setup : MvxIosSetup
    {
        public Setup(MvxApplicationDelegate appDelegate,
                     IMvxIosViewPresenter presenter) : base(appDelegate, presenter)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }
    }
}
