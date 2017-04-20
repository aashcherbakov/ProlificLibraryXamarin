using System;
using System.Linq;
using UIKit;

namespace ProlificLibrary.iOS
{
    public class ControllerFactory
    {
        public T Create<T>(Destination destination) where T : UIViewController
        {
            var storyboardName = StoryboardNameFor(destination);
            var storyboard = UIStoryboard.FromName(storyboardName, null);
            var identifier = typeof(T).ToString().Split('.').Last();
            var controller = storyboard.InstantiateViewController(identifier) as T;
            return controller;
        }

        string StoryboardNameFor(Destination destination)
        {
            switch (destination)
            {
                case Destination.BookEdit:
                    return "Main";
                case Destination.BookList:
                    return "Main";
                case Destination.BookDetails:
                    return "Main";
                default:
                    throw new Exception("Destination unrecognized");
            };
        }
    }
}
