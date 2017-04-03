using System;
using MvvmCross.Core.ViewModels;

namespace ProlificLibrary
{
    public class CustomAppStart : MvxNavigatingObject, IMvxAppStart
    {
        // hint is a platform specific object like options dictionary
        public void Start(object hint = null)
        {
            // Put logic here if we want first screen to be different 
            // depending on user being logged in for example.
            ShowViewModel<BookListViewModel>();
        }
    }
}
