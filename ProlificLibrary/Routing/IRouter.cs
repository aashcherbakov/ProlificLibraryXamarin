namespace ProlificLibrary.Routing
{
    /// <summary>
    /// Router used to navigate between screens.
    /// </summary>
    public interface IRouter
    {
        /// <summary>
        /// Interface to abstract away concrete screen creation from navigation. 
        /// </summary>
        /// <param name="destination">Enum value that represents screen you need to get into</param>
        /// <param name="presenter">Screen that is currently visible and will be presenting new screeen</param>
        /// <param name="parameters">Object that encapsulates parameters that needs to be passed to next screen</param>
        /// <param name="navigationType">Visual way to move from one screen to another</param>
        void NavigateTo(
            Destination destination,
            IPresenter presenter,
            ITransferable parameters,
            NavigationType navigationType
            );
    }

    /// <summary>
    /// List of screens in the app. 
    /// </summary>
    public enum Destination
    {
        BookList,
        BookDetails,
        BookEdit
    }

    /// <summary>
    /// Object responsible for presentation of other screens (controllers or actions) and alerts.
    /// </summary>
    public interface IPresenter
    {
        void Present(IPresenter screen, NavigationType navigationType);
    }

    // Empty interface to unify parameter objects.
    public interface ITransferable { }

    /// <summary>
    /// Enum that defines way to transition from one screen to another. 
    /// Push - screen slides horizontally from the left.
    /// Modal - screen comes vertically from the bottom.
    /// </summary>
    public enum NavigationType
    {
        Push,
        Modal
    }
}