using System.Windows;
using System.Windows.Controls;
using Launcher.Code.Settings;
using Launcher.Code.Starter;

namespace Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LauncherSettings laucherSettings = null;
        private ServerSettings serverSettings = null;

        public MainWindow()
        {
            InitializeComponent();
            OnAccount(null, null);
        }

        private void HideAllMenuBarGrids()
        {
            // account
            LoginGrid.Visibility = Visibility.Hidden;
            // RegisterGrid.Visibility = Visibility.Hidden;
            // AccountGrid.Visibility = Visibility.Hidden;
            // ChangeEmailGrid.Visibility = Visibility.Hidden;
            // ChangePasswordGrid.Visibility = Visibility.Hidden;

            // server
            ServerGeneralGrid.Visibility = Visibility.Hidden;
            // ServerBotsPmcWarGrid.Visibility = Visibility.Hidden;
            // ServerBotsLimitGrid.Visibility = Visibility.Hidden;
            // ServerBotsSpawnGrid.Visibility = Visibility.Hidden;
            // ServerWeatherGrid.Visibility = Visibility.Hidden;

            // launcher
            SettingsGrid.Visibility = Visibility.Hidden;
        }

        #region LOAD_SETTINGS
        private void LoadAllSettings()
        {
            laucherSettings = new LauncherSettings();
            serverSettings = new ServerSettings(System.IO.Path.Combine(laucherSettings.GetServerLocation(), "data"));
        }

        private void LoadAccountSettings()
        {
            // reload config files
            LoadAllSettings();

            // load the settings
            Email.Text = laucherSettings.GetEmail();
            Password.Text = laucherSettings.GetPassword();
            ClientBackendURL.Text = laucherSettings.GetBackendURL();
        }

        private void LoadServerGeneralSettings()
        {
            // reload config files
            LoadAllSettings();

            // load the settings
            Port.Text = serverSettings.GetServerPort();
        }

        private void LoadLauncherSettings()
        {
            // reload config files
            LoadAllSettings();

            // load the settings
            GameLocation.Text = laucherSettings.GetGameLocation();
            ServerLocation.Text = laucherSettings.GetServerLocation();
        }
        #endregion

        #region MENU_BAR
        // TODO: Check if user is logged in
        private void OnAccount(object sender, RoutedEventArgs e)
        {
            HideAllMenuBarGrids();
            LoginGrid.Visibility = Visibility.Visible;
            LoadAccountSettings();
        }

        private void OnServerGeneral(object sender, RoutedEventArgs e)
        {
            HideAllMenuBarGrids();
            ServerGeneralGrid.Visibility = Visibility.Visible;
            LoadServerGeneralSettings();
        }

        private void OnServerBotsPmcWar(object sender, RoutedEventArgs e)
        {
            // code here
        }

        private void OnServerBotsLimit(object sender, RoutedEventArgs e)
        {
            // code here
        }

        private void OnServerBotsSpawn(object sender, RoutedEventArgs e)
        {
            // code here
        }

        private void OnServerWeather(object sender, RoutedEventArgs e)
        {
            // code here
        }

        private void OnSettings(object sender, RoutedEventArgs e)
        {
            HideAllMenuBarGrids();
            SettingsGrid.Visibility = Visibility.Visible;
            LoadLauncherSettings();
        }
        #endregion

        #region APPLICATION_LAUNCHER
        private void OnStartGame(object sender, RoutedEventArgs e)
        {
            GameStarter starter = new GameStarter(GameLocation.Text, ClientBackendURL.Text, Email.Text, Password.Text);
        }

        private void OnStartServer(object sender, RoutedEventArgs e)
        {
            ServerStarter starter = new ServerStarter(ServerLocation.Text);
        }
        #endregion

        #region ACCOUNT_LOGIN
        private void OnChangeEmail(object sender, TextChangedEventArgs e)
        {
            laucherSettings.SetEmail(Email.Text);
        }

        private void OnChangePassword(object sender, TextChangedEventArgs e)
        {
            laucherSettings.SetPassword(Password.Text);
        }

        private void OnChangeClientBackendURL(object sender, TextChangedEventArgs e)
        {
            laucherSettings.SetBackendURL(ClientBackendURL.Text);
        }

        private void OnLogin(object sender, RoutedEventArgs e)
        {
            // code here
        }

        private void OnRegisterMenu(object sender, RoutedEventArgs e)
        {
            // code here
        }
        #endregion

        #region SERVER_GENERAL

        private void OnChangePort(object sender, RoutedEventArgs e)
        {
            // change settings here
        }

        #endregion

        #region LAUNCHER_SETTINGS
        private void OnChangeGameLocation(object sender, TextChangedEventArgs e)
        {
            laucherSettings.SetGameLocation(GameLocation.Text);
        }

        private void OnChangeServerLocation(object sender, TextChangedEventArgs e)
        {
            laucherSettings.SetServerLocation(ServerLocation.Text);
        }
        #endregion
    }
}
