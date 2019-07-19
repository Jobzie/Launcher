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
using System.Text;

namespace Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Tools Tools = new Tools();
        private LauncherSettings laucherSettings = null;
        private ServerSettings serverSettings = null;
        private LoginToken LoginToken = new LoginToken();
        private ProfileSettings ProfileSettings = null;
        private ProfileCharacters Characters = null;
        private Watcher clientWatcher = new Watcher("EmuTarkov-Client.exe");
        private Watcher serverWatcher = new Watcher("EmuTarkov-Server.exe");
        private ErrorHandler ErrorHandler = new ErrorHandler();
        private ErrorTypes ErrorType = new ErrorTypes();
        public MainWindow()
        {
            InitializeComponent();
            OnIntro(null, null);
            ErrorGrid.Visibility = Visibility.Hidden;
            LoadAllSettings();

            // prepare for some errors to display if something goes wrong
            CheckAllErrors();

        }
        #region Help Functions
        private void CheckAllErrors() {
            string check_client_file = laucherSettings.GetClientLocation() + @"\" + laucherSettings.GetClientFilename() + ".exe";
            if (!File.Exists(check_client_file))
            {
                ErrorHandler.AddError(ErrorType.error_Client_noLocation);
            }
            string check_server_file = laucherSettings.GetServerLocation() + @"\" + laucherSettings.GetServerFilename() + ".exe";
            if (!File.Exists(check_server_file))
            {
                ErrorHandler.AddError(ErrorType.error_Server_noLocation);
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
            BackendIP.Text = laucherSettings.LoadIP();
            Port.Text = laucherSettings.LoadPort();

        }

        private void DisplayErrors() {
            string errText = ErrorHandler.ReturnErrorAsText();
            int StringErrLines = ErrorHandler.StringErrLines(errText);
            ErrorText.Height = (double)(StringErrLines*14);
            ErrorGrid.Height = ErrorText.Height + 20;
            ErrorText.Text = errText;
            ErrorHandler.ButtonDisplayer(btn_Start_Client, btn_Start_Server);
            if (ErrorHandler.isAnyErrors())
                ErrorGrid.Visibility = Visibility.Visible;
            else
                ErrorGrid.Visibility = Visibility.Hidden;
        }

        private void Set_LoginToken(string login, string pass, bool toggle = false, int timestamp = 0)
        {
            LoginToken.email = login;
            LoginToken.password = pass;
            LoginToken.toggle = toggle;
            LoginToken.timestamp = timestamp;
        }
        private void Clear_LoginToken()
        {
            LoginToken.email = "";
            LoginToken.password = "";
            LoginToken.toggle = false;
            LoginToken.timestamp = 0;
        }
        private string CreateArguments()
        {
            // < Convert.ToBase64String(Encoding.UTF8.GetBytes("l.o.g.i.n")) == bC5vLmcuaS5u >
            string ret = "";
            if (LoginToken.toggle)
            {
                string login = LoginToken.email;
                string password = LoginToken.password;
                string hashedLoginData = "{ email: " + login + ", password: " + password + ", remember: true, timestamp: " + DateTime.Now.ToFileTime() + "}";
                int ProfileID = ProfileSettings.GetProfile(login, password);
                ret += "-bC5vLmcuaS5u=" + Convert.ToBase64String(Encoding.UTF8.GetBytes(hashedLoginData)) + " -token=" + ProfileID.ToString();
            }
            ret += (ScreenMode.SelectedIndex != 0) ? " -screenmode=" + ((ScreenMode.SelectedItem as ComboBoxItem).Content as string) : "";
            return ret;
        }
        #endregion

        private void OnIntro(object sender, RoutedEventArgs e) {
            HideAllGrids();
            IntroGrid.Visibility = Visibility.Visible;
        }

        #region MENU_BAR
        // TODO: Check if user is logged in
        private void OnAccount(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            if (LoginToken.toggle)
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
                ErrorHandler.AddError(ErrorType.error_Client_noLocation);
            }
            else
            {
                ErrorHandler.RemoveError(ErrorType.error_Client_noLocation);
                // allow only one instance to run
                if (clientWatcher.IsProcessAlive())
                {
                    ErrorHandler.AddError(ErrorType.error_ClientAlreadyRunning);
                }
                else
                {
                    ErrorHandler.RemoveError(ErrorType.error_ClientAlreadyRunning);
                    ClientStarter starter = new ClientStarter(ClientLocation.Text, laucherSettings.PrepareBackendURL(), LoginEmail.Text, LoginPassword.Text, ClientFileName.Text, CreateArguments());
                }
            }
            DisplayErrors();
        }

        private void OnStartServer(object sender, RoutedEventArgs e)
        {
            string check_server_file = laucherSettings.GetServerLocation() + @"\" + laucherSettings.GetServerFilename() + ".exe";
            if (!File.Exists(check_server_file))
            {
                ErrorHandler.AddError(ErrorType.error_Client_noLocation);
            }
            else
            {
                ErrorHandler.RemoveError(ErrorType.error_Server_noLocation);
                // allow only one instance to run
                if (serverWatcher.IsProcessAlive())
                {
                    ErrorHandler.AddError(ErrorType.error_ServerAlreadyRunning);
                }
                else
                {
                    ErrorHandler.RemoveError(ErrorType.error_ServerAlreadyRunning);
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
        }

        private void OnChangeLoginEmail(object sender, TextChangedEventArgs e)
        {
            laucherSettings.SetEmail(LoginEmail.Text);
        }

        private void OnChangeLoginPassword(object sender, TextChangedEventArgs e)
        {
            laucherSettings.SetPassword(LoginPassword.Text);
        }

        private void OnLogin(object sender, RoutedEventArgs e)
        {
            if (ProfileSettings.ListExists())
            {
                if (ProfileSettings.CheckLoginApprove(LoginEmail.Text, LoginPassword.Text))
                {
                    //LoggedProfile = new ProfileSettings(Path.Combine(laucherSettings.GetServerLocation(), "data/profiles/" + ProfileSettings.GetProfile(LoginEmail.Text, LoginPassword.Text).ToString()));
                    Set_LoginToken(LoginEmail.Text, LoginPassword.Text, true, 0);
                    Characters = new ProfileCharacters(System.IO.Path.Combine(laucherSettings.GetServerLocation() + "/data/profiles/" + ProfileSettings.GetProfile(LoginToken.email, LoginToken.password)));
                    HideAllGrids();
                    AccountSettingsGrid.Visibility = Visibility.Visible;
                    LoadAccountSettings();
                    ErrorHandler.RemoveError(ErrorType.error_noUserLikeThis);
                    //LoggedIn = true;
                }
                else
                {
                    ErrorHandler.AddError(ErrorType.error_noUserLikeThis);
                }
                ErrorHandler.RemoveError(ErrorType.error_Server_noLocation);
            }
            else
            {
                ErrorHandler.AddError(ErrorType.error_Server_noLocation);
            }
            DisplayErrors();
        }
        #endregion

        #region ACCOUNT_REGISTER
        private void OnRegister(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            RegisterGrid.Visibility = Visibility.Visible;
        }


        private void OnCreateProfile(object sender, RoutedEventArgs e) {
            //grab data
            string email = RegisterEmail.Text;
            string password = RegisterPassword.Text;
            string nickname = RegisterNickname.Text;
            string side = RegisterSide.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", "");
            //Create account
            ProfileSettings.AddProfile(email, password);
            //create folder
            int ID = ProfileSettings.GetProfile(email, password);
            string path = laucherSettings.GetServerLocation() + "/data/profiles/" + ID + "";
                string charactersJSON = "/character.json";
                string purchasesJSON = "/purchases.json";
            Directory.CreateDirectory(Path.GetDirectoryName(path + charactersJSON));
            Directory.CreateDirectory(Path.GetDirectoryName(path + purchasesJSON));
            //put contents into created files
            Uri charactersDefault = new Uri("pack://application:,,,/Resources/Data/characterDefault.json");
            Uri purchasesDefault  = new Uri("pack://application:,,,/Resources/Data/purchasesDefault.json");
            Tools.SaveFileStream(charactersDefault, path + charactersJSON);
            Tools.SaveFileStream(purchasesDefault, path + purchasesJSON);
            //edit character and release it
            ProfileCharacters tempCharacter = new ProfileCharacters(path);
            tempCharacter.ChangeNickname(nickname);
            tempCharacter.ChangeSide(side);
            tempCharacter = null;//remove temp characters data
            HideAllGrids();
            LoginGrid.Visibility = Visibility.Visible;
        }
        #endregion

        #region ACCOUNT_SETTINGS
        private void LoadAccountSettings()
        {
            // load the settings
            string Nickname = Characters.GetNickname();
            PlayerName.Content = "Hello, " + Nickname;  // replace this with the actual player nickname!
        }
        #region Email/Login
        private void OnChangeEmail(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            ChangeEmailGrid.Visibility = Visibility.Visible;
        }
        private void OnSaveProfile_Email(object sender, RoutedEventArgs e)
        {
            ProfileSettings.ChangeProfileEmail(LoginToken.email, LoginToken.password, NewEmail.Text);
        }
        #endregion
        #region Password
        private void OnChangePassword(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            ChangePasswordGrid.Visibility = Visibility.Visible;
        }
        private void OnSaveProfile_Password(object sender, RoutedEventArgs e)
        {
            ProfileSettings.ChangeProfilePassword(LoginToken.email, LoginToken.password, NewPassword.Text);
        }
        #endregion
        #region Nickname
        private void OnChangeNickname(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            ChangeNicknameGrid.Visibility = Visibility.Visible;
        }
        private void OnSaveProfile_Nickname(object sender, RoutedEventArgs e)
        {
            if (NewNickname.Text != "" && NewNickname.Text != Characters.GetNickname())
            {
                Characters.ChangeNickname(NewNickname.Text);
                Characters.ReloadProfiles();
                HideAllGrids();
                LoadAccountSettings();
                AccountSettingsGrid.Visibility = Visibility.Visible;
            }
        }
        #endregion
        #region Appearance
        private void OnChangeAppearance(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            ChangeAppearanceGrid.Visibility = Visibility.Visible;
            LoadAppearanceSettings();
        }
        #region ACCOUNT_CHANGE_APPEARANCE

        private string[] Head = { "bear_head", "bear_head_1", "head_boss_killa", "usec_head", "usec_head_1", "wild_dealmaker_head", "wild_head", "wild_head_1", "wild_head_2", "wild_head_3" };
        private string[] Hand = { "bear_hands_skin", "usec_hands_skin", "wild_body_1_firsthands", "wild_body_2_firsthands", "wild_body_3_firsthands", "wild_body_firsthands" };
        private string[] Legs = { "bear_feet", "bear_feet_1", "pant_boss_killa", "pants_wild_scavelite", "usec_feet", "wild_dealmaker_feet", "wild_feet", "wild_feet_1", "wild_feet_2", "wild_security_feet_1" };
        private string[] Body = { "bear_body", "top_boss_killa", "top_wild_scavelite", "usec_body", "wild_body", "wild_body_1", "wild_body_2", "wild_body_3", "wild_dealmaker_body", "wild_security_body_1", "wild_security_body_2" };
        private void LoadAppearanceSettings()
        {
            string[][] bodyParts = { Head, Hand, Body, Legs };
            string[] loadParts = { Characters.GetCharacterCustomizationPart("Head"),
                                Characters.GetCharacterCustomizationPart("Hands"),
                                Characters.GetCharacterCustomizationPart("Body"),
                                Characters.GetCharacterCustomizationPart("Feet")};
            for (int p_las_i = 0; p_las_i < loadParts.Length; p_las_i++)
            {
                for (int p_las_i2 = 0; p_las_i2 < bodyParts[p_las_i].Length; p_las_i2++)
                {
                    if (bodyParts[p_las_i][p_las_i2] == loadParts[p_las_i])
                    {
                        switch (p_las_i)
                        {
                            case 0:
                                ChangeHead.SelectedIndex = p_las_i2;
                                if (HeadImage != null)
                                    HeadImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/character/head/" + loadParts[p_las_i] + ".png"));

                                break;
                            case 1:
                                ChangeHands.SelectedIndex = p_las_i2;
                                if (HandsImage != null)
                                    HandsImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/character/hands/" + loadParts[p_las_i] + ".png"));
                                break;
                            case 2:
                                ChangeBody.SelectedIndex = p_las_i2;
                                if (BodyImage != null)
                                    BodyImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/character/body/" + loadParts[p_las_i] + ".png"));
                                break;
                            case 3:
                                ChangeLegs.SelectedIndex = p_las_i2;
                                if (LegsImage != null)
                                    LegsImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/character/legs/" + loadParts[p_las_i] + ".png"));
                                break;
                        }
                    }
                }
            }
            // code here
        }

        private void OnAppearanceChange(object sender, RoutedEventArgs e)
        {
            // send changes to the server
            // show account panel
            string[] changed = {
                (ChangeHead.SelectedItem as ComboBoxItem).Content.ToString().Replace("Head ", ""),
                (ChangeHands.SelectedItem as ComboBoxItem).Content.ToString().Replace("Hands ", ""),
                (ChangeBody.SelectedItem as ComboBoxItem).Content.ToString().Replace("Body ", ""),
                (ChangeLegs.SelectedItem as ComboBoxItem).Content.ToString().Replace("Legs ", "") };
            string[] type = { "Head", "Hands", "Body", "Feet" };
            string returnVar = "";
            for (int i = 0; i < type.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        returnVar = "assets/content/characters/character/prefabs/" + Head[Int32.Parse(changed[i]) - 1] + ".bundle";
                        break;
                    case 1:
                        switch (Hand[Int32.Parse(changed[i]) - 1])
                        {
                            case "bear_hands_skin":
                                returnVar = "assets/content/hands/bear/" + Hand[Int32.Parse(changed[i]) - 1] + ".bundle";
                                break;
                            case "usec_hands_skin":
                                returnVar = "assets/content/hands/usec/" + Hand[Int32.Parse(changed[i]) - 1] + ".bundle";
                                break;
                            case "wild_body_1_firsthands":
                            case "wild_body_2_firsthands":
                            case "wild_body_3_firsthands":
                            case "wild_body_firsthands":
                                returnVar = "assets/content/hands/wild/" + Hand[Int32.Parse(changed[i]) - 1] + ".bundle";
                                break;
                        }
                        break;
                    case 2:
                        returnVar = "assets/content/characters/character/prefabs/" + Body[Int32.Parse(changed[i]) - 1] + ".bundle";
                        break;
                    case 3:
                        returnVar = "assets/content/characters/character/prefabs/" + Legs[Int32.Parse(changed[i]) - 1] + ".bundle";
                        break;
                }
                Characters.SaveCharacterCustomization(type[i], returnVar);
            }
            HideAllGrids();
            AccountSettingsGrid.Visibility = Visibility.Visible;
            LoadAccountSettings();
        }
        #region Select Change EVENTS
        private void ChangeHead_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string s = (e.AddedItems[0] as ComboBoxItem).Content as string;
            s = s.Replace("Head ", "");
            Int32.TryParse(s, out int i10);
            string item = Head[i10 - 1];
            if (HeadImage != null)
                HeadImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/character/head/" + item + ".png"));
        }
        private void ChangeLegs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string s = (e.AddedItems[0] as ComboBoxItem).Content as string;
            s = s.Replace("Legs ", "");
            Int32.TryParse(s, out int i11);
            string item = Legs[i11 - 1];
            if (LegsImage != null)
                LegsImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/character/legs/" + item + ".png"));
        }
        private void ChangeBody_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string s = (e.AddedItems[0] as ComboBoxItem).Content as string;
            s = s.Replace("Body ", "");
            Int32.TryParse(s, out int i12);
            string item = Body[i12 - 1];
            if (BodyImage != null)
                BodyImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/character/body/" + item + ".png"));
        }
        private void ChangeHands_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string s = (e.AddedItems[0] as ComboBoxItem).Content as string;
            s = s.Replace("Hands ", "");
            Int32.TryParse(s, out int i13);
            string item = Hand[i13 - 1];
            if (HandsImage != null)
                HandsImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/character/hands/" + item + ".png"));
        }
        #endregion

        #endregion
        #endregion
        #region Logout
        private void OnLogout(object sender, RoutedEventArgs e)
        {
            // logout user
            Clear_LoginToken();
            // show account panel
            HideAllGrids();
            LoginGrid.Visibility = Visibility.Visible;
            LoadLoginSettings();
        }
        #endregion
        #endregion

        #region SERVER_GENERAL
        private void LoadServerGeneralSettings()
        {
            // reload config files
            LoadAllSettings();

            // load the settings
            Port.Text = serverSettings.GetServerPort();
            BackendIP.Text = laucherSettings.LoadIP();
        }

        private void OnChangePort(object sender, RoutedEventArgs e)
        {
            laucherSettings.SavePort(Int32.Parse(Port.Text));
            serverSettings.SetServerPort(Port.Text);
            //serverSettings.SetServerPort(Port.Text);
        }

        private void OnChangeBackendIP(object sender, TextChangedEventArgs e)
        {
            laucherSettings.SaveIP(BackendIP.Text);
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
                ErrorHandler.AddError(ErrorType.error_Client_noLocation);
            else
                ErrorHandler.RemoveError(ErrorType.error_Client_noLocation);
            DisplayErrors();
        }
        private void OnChangeClientFilename(object sender, TextChangedEventArgs e)
        {
            laucherSettings.SetClientFilename(ClientFileName.Text);
            string check_client_file = laucherSettings.GetClientLocation() + @"\" + laucherSettings.GetClientFilename() + ".exe";
            if (!File.Exists(check_client_file))
                ErrorHandler.AddError(ErrorType.error_Client_noLocation);
            else
                ErrorHandler.RemoveError(ErrorType.error_Client_noLocation);
            DisplayErrors();
        }

        private void OnChangeServerLocation(object sender, TextChangedEventArgs e)
        {
            laucherSettings.SetServerLocation(ServerLocation.Text);
            string check_server_file = laucherSettings.GetServerLocation() + @"\" + laucherSettings.GetServerFilename() + ".exe";
            if (!File.Exists(check_server_file))
                ErrorHandler.AddError(ErrorType.error_Server_noLocation);
            else
                ErrorHandler.RemoveError(ErrorType.error_Server_noLocation);
            DisplayErrors();
        }
        private void OnChangeServerFilename(object sender, TextChangedEventArgs e)
        {
            laucherSettings.SetServerFilename(ServerFileName.Text);
            string check_server_file = laucherSettings.GetServerLocation() + @"\" + laucherSettings.GetServerFilename() + ".exe";
            if (!File.Exists(check_server_file))
                ErrorHandler.AddError(ErrorType.error_Server_noLocation);
            else
                ErrorHandler.RemoveError(ErrorType.error_Server_noLocation);
            DisplayErrors();
        }
        #endregion

    }
}
