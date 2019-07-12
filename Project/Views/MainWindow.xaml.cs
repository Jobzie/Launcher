using System.Windows;
using System.Windows.Controls;
using Launcher.Code.Settings;
using Launcher.Code.Starter;
using Launcher.Code.Monitor;

namespace Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LauncherSettings laucherSettings = null;
        private ServerSettings serverSettings = null;
        private Watcher gameWatcher = new Watcher("EscapeFromTarkov.exe");
        private Watcher serverWatcher = new Watcher("EmuTarkov-Server.exe");

        public MainWindow()
        {
            InitializeComponent();
            OnAccount(null, null);
        }

        private void HideAllGrids()
        {
            // account
            LoginGrid.Visibility = Visibility.Hidden;
            RegisterGrid.Visibility = Visibility.Hidden;
            AccountGrid.Visibility = Visibility.Hidden;
            // AccountGrid.Visibility = Visibility.Hidden;
            // ChangeEmailGrid.Visibility = Visibility.Hidden;
            // ChangePasswordGrid.Visibility = Visibility.Hidden;
            // ChangeNicknameGrid.Visibility = Visibility.Hidden;
            ChangeAppearanceGrid.Visibility = Visibility.Hidden;

            // server
            ServerGeneralGrid.Visibility = Visibility.Hidden;
            ServerBotsGrid.Visibility = Visibility.Hidden;
            // ServerWeatherGrid.Visibility = Visibility.Hidden;

            // launcher
            SettingsGrid.Visibility = Visibility.Hidden;
        }

        private void LoadAllSettings()
        {
            laucherSettings = new LauncherSettings();
            serverSettings = new ServerSettings(System.IO.Path.Combine(laucherSettings.GetServerLocation(), "data"));
        }

        #region MENU_BAR
        // TODO: Check if user is logged in
        private void OnAccount(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            LoginGrid.Visibility = Visibility.Visible;
            LoadLoginSettings();
        }

        private void OnServerGeneral(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            ServerGeneralGrid.Visibility = Visibility.Visible;
            LoadServerGeneralSettings();
        }

        private void OnServerBots(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            ServerBotsGrid.Visibility = Visibility.Visible;
            LoadServerBotsSettings();
        }

        private void OnServerWeather(object sender, RoutedEventArgs e)
        {
            // code here
        }

        private void OnSettings(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            SettingsGrid.Visibility = Visibility.Visible;
            LoadLauncherSettings();
        }
        #endregion

        #region APPLICATION_START
        private void OnStartGame(object sender, RoutedEventArgs e)
        {
            // allow only one instance to run
            if (gameWatcher.IsProcessAlive())
            {
                // show error message
                return;
            }

            GameStarter starter = new GameStarter(GameLocation.Text, ClientBackendURL.Text, LoginEmail.Text, LoginPassword.Text);
        }

        private void OnStartServer(object sender, RoutedEventArgs e)
        {
            // allow only one instance to run
            if (!serverWatcher.IsProcessAlive())
            {
                // show error message
                return;
            }

            ServerStarter starter = new ServerStarter(ServerLocation.Text);
        }
        #endregion

        #region ACCOUNT_LOGIN
        private void LoadLoginSettings()
        {
            // reload config files
            LoadAllSettings();

            // load the settings
            LoginEmail.Text = laucherSettings.GetEmail();
            LoginPassword.Text = laucherSettings.GetPassword();
            ClientBackendURL.Text = laucherSettings.GetBackendURL();
        }

        private void OnChangeLoginEmail(object sender, TextChangedEventArgs e)
        {
            laucherSettings.SetEmail(LoginEmail.Text);
        }

        private void OnChangeLoginPassword(object sender, TextChangedEventArgs e)
        {
            laucherSettings.SetPassword(LoginPassword.Text);
        }

        private void OnChangeClientBackendURL(object sender, TextChangedEventArgs e)
        {
            laucherSettings.SetBackendURL(ClientBackendURL.Text);
        }

        private void OnLogin(object sender, RoutedEventArgs e)
        {
            // check if login is valid

            // show account panel
            HideAllGrids();
            AccountGrid.Visibility = Visibility.Visible;
            LoadAccountSettings();
        }
        #endregion

        #region ACCOUNT_REGISTER
        private void OnChangeRegisterEmail(object sender, TextChangedEventArgs e)
        {
            // code here
        }

        private void OnChangeRegisterPassword(object sender, TextChangedEventArgs e)
        {
            // code here
        }

        private void OnChangeRegisterNickname(object sender, TextChangedEventArgs e)
        {
            // code here
        }

        private void OnChangeRegisterSide(object sender, SelectionChangedEventArgs e)
        {
            // code here
        }

        private void OnRegister(object sender, RoutedEventArgs e)
        {
            // code here
        }
        #endregion

        #region ACCOUNT_SETTINGS
        private void LoadAccountSettings()
        {
            // load the settings
            PlayerName.Content = "EmuTarkov user";  // replace this with the actual player nickname!
        }

        private void OnChangeEmail(object sender, RoutedEventArgs e)
        {
            // code here
        }

        private void OnChangePassword(object sender, RoutedEventArgs e)
        {
            // code here
        }

        private void OnChangeNickname(object sender, RoutedEventArgs e)
        {
            // code here
        }

        private void OnChangeAppearance(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            ChangeAppearanceGrid.Visibility = Visibility.Visible;
            //LoadLoginSettings();
        }

        private void OnLogout(object sender, RoutedEventArgs e)
        {
            // logout user
            
            // show account panel
            HideAllGrids();
            LoginGrid.Visibility = Visibility.Visible;
            LoadLoginSettings();
        }
        #endregion

        #region ACCOUNT_ACTION_BUTTONS
        private void OnSaveChange(object sender, RoutedEventArgs e)
        {
            // code here
        }
        #endregion

        #region SERVER_GENERAL
        private void LoadServerGeneralSettings()
        {
            // reload config files
            LoadAllSettings();

            // load the settings
            Port.Text = serverSettings.GetServerPort();
        }

        private void OnChangePort(object sender, RoutedEventArgs e)
        {
            serverSettings.SetServerPort(Port.Text);
        }
        #endregion

        #region SERVER_BOTS
        private void LoadServerBotsSettings()
        {
            // reload config files
            LoadAllSettings();

            // load PMC war settings
            PmcWarEnabled.IsChecked = serverSettings.GetBotsPmcWarEnabled();
            PmcWarUsecChance.Text = serverSettings.GetBotsPmcWarUsecChance();
        }

        private void OnChangePmcWarEnabled(object sender, RoutedEventArgs e)
        {
            serverSettings.SetBotsPmcWarEnabled((bool)PmcWarEnabled.IsChecked);
        }

        private void OnChangePmcWarUsecChance(object sender, TextChangedEventArgs e)
        {
            serverSettings.SetBotsPmcWarUsecChance(PmcWarUsecChance.Text);
        }

        #endregion

        #region LAUNCHER_SETTINGS
        private void LoadLauncherSettings()
        {
            // reload config files
            LoadAllSettings();

            // load the settings
            GameLocation.Text = laucherSettings.GetGameLocation();
            ServerLocation.Text = laucherSettings.GetServerLocation();
        }

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
