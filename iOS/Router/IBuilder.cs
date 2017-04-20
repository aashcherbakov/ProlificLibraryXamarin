using System;
namespace ProlificLibrary.iOS
{
    public interface IBuilder
    {
        void Build<T>(T controller, IDestinationParameters parameters) where T : BaseViewController;
    }
}
