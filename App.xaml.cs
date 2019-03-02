using System;
using System.Diagnostics;
using System.Windows;
using DatabaseExample.ViewModel;
using NLog;

namespace DatabaseExample
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public void Initialize()
        {
            ShutdownMode = ShutdownMode.OnExplicitShutdown;
            // Чтобы это сработало, нужно у App.xaml поставить дейтсиве при построении в Page.
            LoadComponent(this, new Uri("/DatabaseExample;component/App.xaml", UriKind.Relative));
        }

        public static void Start()
        {
            App app = new App();
            app.Initialize();

            var mainWindow = new MainWindow();
            Current.MainWindow = mainWindow;
            Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            mainWindow.Loaded += OnMainWindowLoaded;
            app.Run(mainWindow);
        }

        private static async void OnMainWindowLoaded(object sender, RoutedEventArgs e)
        {
            _logger.Trace($"{nameof(OnMainWindowLoaded)}.");

            var locator = (ViewModelLocator)Current.FindResource("Locator");
            Debug.Assert(locator != null, "locator != null");
            try
            {
                await locator.Main.InitializeAsync();
            }
            catch (Exception ex)
            {
                _logger.Error($"{nameof(OnMainWindowLoaded)}", ex.ToString());
            }
        }
    }
}
