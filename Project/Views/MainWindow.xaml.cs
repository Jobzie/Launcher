using System.Windows;
using System.Windows.Controls;
using Launcher.Code.Settings;
using Launcher.Code.Starter;
using Launcher.Code.Monitor;
using Launcher.Code.Helper;
using Launcher.Code.Data;
using System.IO;
using System;
using System.Windows.Media.Imaging;

namespace Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LauncherSettings laucherSettings = null;
        private ServerSettings serverSettings = null;
        private ProfileSettings ProfileSettings = null;
        private Watcher clientWatcher = new Watcher("EmuTarkov-Client.exe");
        private Watcher serverWatcher = new Watcher("EmuTarkov-Server.exe");
        private ErrorHandler ErrorHandler = new ErrorHandler();
        private bool LoggedIn = false;
        public MainWindow()
        {
            InitializeComponent();
            OnIntro(null, null);
            ErrorGrid.Visibility = Visibility.Hidden;
            LoadAllSettings();

            // prepare for some errors to display if something goes wrong
            CheckAllErrors();

        }

        private void CheckAllErrors() {
            string check_client_file = laucherSettings.GetClientLocation() + @"\" + laucherSettings.GetClientFilename() + ".exe";
            if (!File.Exists(check_client_file))
            {
                ErrorHandler.AddError(111, "Cannot find client at this location.");
            }
            string check_server_file = laucherSettings.GetServerLocation() + @"\" + laucherSettings.GetServerFilename() + ".exe";
            if (!File.Exists(check_server_file))
            {
                ErrorHandler.AddError(112, "Cannot find server at this location.");
            }
            DisplayErrors();
        }
        private void HideAllGrids()
        {
            IntroGrid.Visibility = Visibility.Hidden;
            // account
            LoginGrid.Visibility = Visibility.Hidden;
            RegisterGrid.Visibility = Visibility.Hidden;
            AccountSettingsGrid.Visibility = Visibility.Hidden;
            // AccountGrid.Visibility = Visibility.Hidden;
            ChangeEmailGrid.Visibility = Visibility.Hidden;
            ChangePasswordGrid.Visibility = Visibility.Hidden;
            ChangeNicknameGrid.Visibility = Visibility.Hidden;
            ChangeAppearanceGrid.Visibility = Visibility.Hidden;

            // server
            ServerGeneralGrid.Visibility = Visibility.Hidden;
            ServerBotsGrid.Visibility = Visibility.Hidden;
            ServerWeatherGrid.Visibility = Visibility.Hidden;

            // launcher
            SettingsGrid.Visibility = Visibility.Hidden;
        }

        private void LoadAllSettings()
        {
            laucherSettings = new LauncherSettings();
            serverSettings = new ServerSettings(System.IO.Path.Combine(laucherSettings.GetServerLocation(), "data"));
            ProfileSettings = new ProfileSettings(Path.Combine(laucherSettings.GetServerLocation(), "data/profiles"));
        }

        private void DisplayErrors() {
            string errText = ErrorHandler.ReturnErrorAsText();
            int StringErrLines = ErrorHandler.StringErrLines(errText);
            ErrorText.Height = (double)(StringErrLines*14);
            ErrorGrid.Height = ErrorText.Height + 20;
            ErrorText.Text = errText;
            if(ErrorHandler.isAnyErrors())
                ErrorGrid.Visibility = Visibility.Visible;
            else
                ErrorGrid.Visibility = Visibility.Hidden;
        }

        private void OnIntro(object sender, RoutedEventArgs e) {
            HideAllGrids();
            IntroGrid.Visibility = Visibility.Visible;
        }

        #region MENU_BAR
        // TODO: Check if user is logged in
        private void OnAccount(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            if (LoggedIn)
            {
                AccountSettingsGrid.Visibility = Visibility.Visible;
                LoadAccountSettings();
            }
            else
            {
                LoginGrid.Visibility = Visibility.Visible;
                LoadLoginSettings();
            }
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
            HideAllGrids();
            ServerWeatherGrid.Visibility = Visibility.Visible;

            // need more code here :)
        }

        private void OnSettings(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            SettingsGrid.Visibility = Visibility.Visible;
            LoadLauncherSettings();
        }
        #endregion

        #region APPLICATION_START
        private void OnStartClient(object sender, RoutedEventArgs e)
        {
            string check_client_file = laucherSettings.GetClientLocation() + @"\" + laucherSettings.GetClientFilename() + ".exe";
            if (!File.Exists(check_client_file))
            {
                ErrorHandler.AddError(111, "Cannot find client at this location.");
            }
            else
            {
                ErrorHandler.RemoveError(111, "Cannot find client at this location.");
                // allow only one instance to run
                if (clientWatcher.IsProcessAlive())
                {
                    ErrorHandler.AddError(201, "Client is already running");
                }
                else
                {
                    ErrorHandler.RemoveError(201, "Client is already running");
                    ClientStarter starter = new ClientStarter(ClientLocation.Text, LoginBackendURL.Text, LoginEmail.Text, LoginPassword.Text, ClientFileName.Text);
                }
            }
            DisplayErrors();
        }

        private void OnStartServer(object sender, RoutedEventArgs e)
        {
            string check_server_file = laucherSettings.GetServerLocation() + @"\" + laucherSettings.GetServerFilename() + ".exe";
            if (!File.Exists(check_server_file))
            {
                ErrorHandler.AddError(112, "Cannot find server at this location.");
            }
            else
            {
                ErrorHandler.RemoveError(112, "Cannot find server at this location.");
                // allow only one instance to run
                if (serverWatcher.IsProcessAlive())
                {
                    ErrorHandler.AddError(201, "Server is already running");
                }
                else
                {
                    ErrorHandler.RemoveError(201, "Client is already running");
                    ServerStarter starter = new ServerStarter(ServerLocation.Text, ServerFileName.Text);
                }
            }
            DisplayErrors();
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
            LoginBackendURL.Text = laucherSettings.GetBackendURL();
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
            laucherSettings.SetBackendURL(LoginBackendURL.Text);
        }

        private void OnLogin(object sender, RoutedEventArgs e)
        {
            if (ProfileSettings.ListExists())
            {
                if (ProfileSettings.CheckLoginApprove(LoginEmail.Text, LoginPassword.Text))
                {
                    HideAllGrids();
                    AccountSettingsGrid.Visibility = Visibility.Visible;
                    LoadAccountSettings();
                    ErrorHandler.RemoveError(100, "No Such User");
                    LoggedIn = true;
                }
                else
                {
                    ErrorHandler.AddError(100,"No Such User");
                }
                ErrorHandler.RemoveError(101, "Unable to find proper file; Check Server location.");
            }
            else
            {
                ErrorHandler.AddError(101, "Unable to find proper file; Check Server location.");
            }
            DisplayErrors();
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
            string Nickname = "EmuTarkov user";
            PlayerName.Content = "Hello, " + Nickname;  // replace this with the actual player nickname!
        }

        private void OnChangeEmail(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            ChangeEmailGrid.Visibility = Visibility.Visible;
        }

        private void OnChangePassword(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            ChangePasswordGrid.Visibility = Visibility.Visible;
        }

        private void OnChangeNickname(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            ChangeNicknameGrid.Visibility = Visibility.Visible;
        }

        private void OnChangeAppearance(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            ChangeAppearanceGrid.Visibility = Visibility.Visible;
            LoadAppearanceSettings();
        }

        private void OnLogout(object sender, RoutedEventArgs e)
        {
            // logout user
            LoggedIn = false;
            // show account panel
            HideAllGrids();
            LoginGrid.Visibility = Visibility.Visible;
            LoadLoginSettings();
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
            LoadBotsPmcWarSettings();
            LoadBotsLimitSettings();
            LoadBotsSpawnSettings();
        }
        #endregion

        #region BOTS_PMCWAR
        private void LoadBotsPmcWarSettings()
        {
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

        #region BOTS_LIMIT
        private void LoadBotsLimitSettings()
        {
            LimitKilla.Text = serverSettings.GetBotsLimitKilla();
            LimitBully.Text = serverSettings.GetBotsLimitBully();
            LimitBullyFollowers.Text = serverSettings.GetBotsLimitBullyFollowers();
            LimitMarksman.Text = serverSettings.GetBotsLimitMarksman();
            LimitPmcBot.Text = serverSettings.GetBotsLimitPmcBot();
            LimitScav.Text = serverSettings.GetBotsLimitScav();
        }

        private void OnChangeLimitKilla(object sender, TextChangedEventArgs e)
        {
            serverSettings.SetBotsLimitKilla(LimitKilla.Text);
        }

        private void OnChangeLimitBully(object sender, TextChangedEventArgs e)
        {
            serverSettings.SetBotsLimitBully(LimitBully.Text);
        }

        private void OnChangeLimitBullyFollower(object sender, TextChangedEventArgs e)
        {
            serverSettings.SetBotsLimitBullyFollowers(LimitBullyFollowers.Text);
        }

        private void OnChangeLimitMarksman(object sender, TextChangedEventArgs e)
        {
            serverSettings.SetBotsLimitMarksman(LimitMarksman.Text);
        }

        private void OnChangeLimitPmcBot(object sender, TextChangedEventArgs e)
        {
            serverSettings.SetBotsLimitPmcBot(LimitPmcBot.Text);
        }

        private void OnChangeLimitScav(object sender, TextChangedEventArgs e)
        {
            serverSettings.SetBotsLimitScav(LimitScav.Text);
        }
        #endregion

        #region BOTS_SPAWN
        private void LoadBotsSpawnSettings()
        {
            SpawnGlasses.Text = serverSettings.GetBotsSpawnGlasses();
            SpawnFaceCover.Text = serverSettings.GetBotsSpawnFaceCover();
            SpawnHeadwear.Text = serverSettings.GetBotsSpawnHeadwear();
            SpawnBackpack.Text = serverSettings.GetBotsSpawnBackpack();
            SpawnArmorVest.Text = serverSettings.GetBotsSpawnArmorVest();
            SpawnMedPocket.Text = serverSettings.GetBotsSpawnMedPocket();
            SpawnItemPocket.Text = serverSettings.GetBotsSpawnItemPocket();
        }

        private void OnChangeSpawnGlasses(object sender, TextChangedEventArgs e)
        {
            serverSettings.SetBotsSpawnGlasses(SpawnGlasses.Text);
        }

        private void OnChangeSpawnFaceCover(object sender, TextChangedEventArgs e)
        {
            serverSettings.SetBotsSpawnFaceCover(SpawnFaceCover.Text);
        }

        private void OnChangeSpawnHeadwear(object sender, TextChangedEventArgs e)
        {
            serverSettings.SetBotsSpawnHeadwear(SpawnHeadwear.Text);
        }

        private void OnChangeSpawnBackpack(object sender, TextChangedEventArgs e)
        {
            serverSettings.SetBotsSpawnBackpack(SpawnBackpack.Text);
        }

        private void OnChangeSpawnArmorVest(object sender, TextChangedEventArgs e)
        {
            serverSettings.SetBotsSpawnArmorVest(SpawnArmorVest.Text);
        }

        private void OnChangeSpawnMedsPocket(object sender, TextChangedEventArgs e)
        {
            serverSettings.SetBotsSpawnMedPocket(SpawnMedPocket.Text);
        }

        private void OnChangeSpawnItemPocket(object sender, TextChangedEventArgs e)
        {
            serverSettings.SetBotsSpawnItemPocket(SpawnItemPocket.Text);
        }
        #endregion

        #region LAUNCHER_SETTINGS
        private void LoadLauncherSettings()
        {
            // reload config files
            LoadAllSettings();

            // load the settings
                //load locations
            ClientLocation.Text = laucherSettings.GetClientLocation();
            ServerLocation.Text = laucherSettings.GetServerLocation();
                //load filenames
            ClientFileName.Text = laucherSettings.GetClientFilename();
            ServerFileName.Text = laucherSettings.GetServerFilename();
        }

        private void OnChangeClientLocation(object sender, TextChangedEventArgs e)
        {
            laucherSettings.SetClientLocation(ClientLocation.Text);
            string check_client_file = laucherSettings.GetClientLocation() + @"\" + laucherSettings.GetClientFilename() + ".exe";
            if (!File.Exists(check_client_file))
                ErrorHandler.AddError(111, "Cannot find client at this location.");
            else
                ErrorHandler.RemoveError(111, "Cannot find client at this location.");
            DisplayErrors();
        }
        private void OnChangeClientFilename(object sender, TextChangedEventArgs e)
        {
            laucherSettings.SetClientFilename(ClientFileName.Text);
            string check_client_file = laucherSettings.GetClientLocation() + @"\" + laucherSettings.GetClientFilename() + ".exe";
            if (!File.Exists(check_client_file))
                ErrorHandler.AddError(111, "Cannot find client at this location.");
            else
                ErrorHandler.RemoveError(111, "Cannot find client at this location.");
            DisplayErrors();
        }

        private void OnChangeServerLocation(object sender, TextChangedEventArgs e)
        {
            laucherSettings.SetServerLocation(ServerLocation.Text);
            string check_server_file = laucherSettings.GetServerLocation() + @"\" + laucherSettings.GetServerFilename() + ".exe";
            if (!File.Exists(check_server_file))
                ErrorHandler.AddError(112, "Cannot find server at this location.");
            else
                ErrorHandler.RemoveError(112, "Cannot find server at this location.");
            DisplayErrors();
        }
        private void OnChangeServerFilename(object sender, TextChangedEventArgs e)
        {
            laucherSettings.SetServerFilename(ServerFileName.Text);
            string check_server_file = laucherSettings.GetServerLocation() + @"\" + laucherSettings.GetServerFilename() + ".exe";
            if (!File.Exists(check_server_file))
                ErrorHandler.AddError(112, "Cannot find server at this location.");
            else
                ErrorHandler.RemoveError(112, "Cannot find server at this location.");
            DisplayErrors();
        }
        #endregion

        #region ACCOUNT_CHANGE_APPEARANCE

        private string[] Head = { "bear_head", "bear_head_1", "head_boss_killa", "usec_head", "usec_head_1", "wild_dealmaker_head", "wild_head", "wild_head_1", "wild_head_2", "wild_head_3" };
        private string[] Hand = { "bear_hands_skin", "usec_hands_skin", "wild_body_1_firsthands", "wild_body_2_firsthands", "wild_body_3_firsthands", "wild_body_firsthands" };
        private string[] Legs = { "bear_feet", "bear_feet_1", "pant_boss_killa", "pants_wild_scavelite", "usec_feet", "wild_dealmaker_feet", "wild_feet", "wild_feet_1", "wild_feet_2", "wild_security_feet_1" };
        private string[] Body = { "bear_body", "top_boss_killa", "top_wild_scavelite", "usec_body", "wild_body", "wild_body_1", "wild_body_2", "wild_body_3", "wild_dealmaker_body", "wild_security_body_1", "wild_security_body_2" };
        private void LoadAppearanceSettings()
        {
            // code here
        }

        private void OnAppearanceChange(object sender, RoutedEventArgs e)
        {
            // send changes to the server
            // show account panel
            HideAllGrids();
            AccountSettingsGrid.Visibility = Visibility.Visible;
            LoadAccountSettings();
        }
        #region Select Change EVENTS
        private void ChangeHead_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string s = (e.AddedItems[0] as ComboBoxItem).Content as string;
            s = s.Replace("Head ", "");
            Int32.TryParse(s, out int i);
            string item = Head[i - 1];
            if(HeadImage != null)
                HeadImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/character/head/" + item + ".png"));
        }
        private void ChangeLegs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string s = (e.AddedItems[0] as ComboBoxItem).Content as string;
            s = s.Replace("Legs ", "");
            Int32.TryParse(s, out int i);
            string item = Legs[i - 1];
            if (LegsImage != null)
                LegsImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/character/legs/" + item + ".png"));
        }
        private void ChangeBody_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string s = (e.AddedItems[0] as ComboBoxItem).Content as string;
            s = s.Replace("Body ", "");
            Int32.TryParse(s, out int i);
            string item = Body[i - 1];
            if (BodyImage != null)
                BodyImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/character/body/" + item + ".png"));
        }
        private void ChangeHands_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string s = (e.AddedItems[0] as ComboBoxItem).Content as string;
            s = s.Replace("Hands ", "");
            Int32.TryParse(s, out int i);
            string item = Hand[i - 1];
            if (HandsImage != null)
                HandsImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/character/hands/" + item + ".png"));
        }
        #endregion
        #endregion
    }
}
