using Windows.UI.Xaml.Controls;

namespace YTApp
{
    /// <summary>
    /// Main page.
    /// </summary>
    public sealed partial class MainPage2 : Page
    {
        /// <summary>
        /// Initializes a new instance of MainPage2 class.
        /// </summary>
        public MainPage2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets the main viewmodel.
        /// </summary>
        public MainViewModel Vm => DataContext as MainViewModel;
    }
}
