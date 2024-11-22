using ChinookWPFApp;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace ChinookWPFApp
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;

        public RelayCommand(Action execute) : this(execute, null) { }

        public RelayCommand(Action execute, Func<bool>? canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => _canExecute == null || _canExecute();

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object? parameter) => _execute();
    }

    public partial class MainWindow : Window
    {
        private ICommand? _exitCommand;
        public ICommand ExitCommand => _exitCommand ??= new RelayCommand(Exit);

        private ICommand? _homeCommand;
        public ICommand HomeCommand => _homeCommand ??= new RelayCommand(Home);

        private ICommand? _artistsCommand;
        public ICommand ArtistsCommand => _artistsCommand ??= new RelayCommand(Artists);

        private ICommand? _albumsCommand;
        public ICommand AlbumsCommand => _albumsCommand ??= new RelayCommand(Albums);

        private ICommand? _tracksCommand;
        public ICommand TracksCommand => _tracksCommand ??= new RelayCommand(Tracks);

        private ICommand? _musicCatCommand;
        public ICommand MusicCatCommand => _musicCatCommand ??= new RelayCommand(Catalog);

        private ICommand? _custOrderCommand;
        public ICommand CustOrderCommand => _custOrderCommand ??= new RelayCommand(Orders);



        private NavigationService? _navigationService;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            MainContentFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            MainContentFrame.Loaded += MainContentFrame_Loaded;
            MainContentFrame.Source = new Uri("HomePage.xaml", UriKind.Relative);
        }

        private void MainContentFrame_Loaded(object sender, RoutedEventArgs e)
        {
            _navigationService = MainContentFrame.NavigationService;
        }

        private void Exit() => Application.Current.Shutdown();

        private void Home() => _navigationService?.Navigate(new Uri("HomePage.xaml", UriKind.Relative));

        private void Artists() => _navigationService?.Navigate(new Uri("ArtistsPage.xaml", UriKind.Relative));

        private void Albums() => _navigationService?.Navigate(new Uri("AlbumsPage.xaml", UriKind.Relative));

        private void Tracks() => _navigationService?.Navigate(new Uri("TracksPage.xaml", UriKind.Relative));

        private void Catalog() => _navigationService?.Navigate(new Uri("CatalogPage.xaml", UriKind.Relative));

        private void Orders() => _navigationService?.Navigate(new Uri("OrdersPage.xaml", UriKind.Relative));
    }
}

