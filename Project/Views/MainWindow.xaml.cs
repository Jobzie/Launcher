#region  using;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Threading;
    using System.Windows.Media.Animation;
    using System.Windows.Media.Imaging;
    using System.IO;
    using System.Text;
    using System.Collections.Generic;
    using Launcher.Code.Settings;
    using Launcher.Code.Starter;
    using Launcher.Code.Monitor;
    using Launcher.Code.Helper;
    using Launcher.Code.Data;
    using Launcher.Views.core;
    using Newtonsoft.Json;
#endregion

namespace Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Initials - Global variables
            private Tools Tools = new Tools();
            #region Background change Variables
                private DispatcherTimer _tmr = new DispatcherTimer();
                private int _intCurrentImageIndex = 0;
                private List<BitmapImage> _lstImages = new List<BitmapImage>();
            #endregion
            #region WATCHER - Main Vars
                private Watcher clientWatcher = new Watcher("EmuTarkov-Client.exe");
                private Watcher serverWatcher = new Watcher("EmuTarkov-Server.exe");
            #endregion
            #region ERROR - Main Vars
                private ErrorHandler ErrorHandler = new ErrorHandler();
                private ErrorTypes ErrorType = new ErrorTypes();
                private DispatcherTimer _updater = new DispatcherTimer();
            #endregion
            #region SETTINGS - Main Vars
                private LauncherSettings laucherSettings = null;
                private ServerSettings serverSettings = null;
            #endregion
            #region PROFILE_DATA - Main Vars
                private LoginToken LoginToken = new LoginToken();
                private ProfileSettings ProfileSettings = null;
                private ProfileCharacters Characters = null;
            #endregion
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            OnIntro(null, null);
            ErrorGrid.Visibility = Visibility.Hidden;
            laucherSettings = new LauncherSettings();
            #region background iniciator
            string[] files = Directory.GetFiles("./data/images/background/");
            for(int f_i = 0; f_i < files.Length; f_i++)
            {
                _lstImages.Add(new BitmapImage(new Uri(files[f_i].Replace("./data", "pack://siteoforigin:,,,/data"))));
            }

            Bg1.Source = _lstImages[_intCurrentImageIndex];
            _tmr.Interval = new TimeSpan(0, 1, 0);
            _tmr.Tick += new EventHandler(BackgroundUpdater);
            _tmr.Start();
            #endregion
            #region Updater
            _updater.Interval = new TimeSpan(0, 0, 1);
            _updater.Tick += new EventHandler(Auto_update_1sec);
            _updater.Start();
            #endregion
            CheckAllErrors();
            LoadAllSettings();
            #region allow external links to be execute in webbrowser
            creatorsWebpage.RequestNavigate += (sender, e) =>
            {
                 System.Diagnostics.Process.Start(e.Uri.ToString());
            };
            #endregion
        }

        #region TIMERs
        void BackgroundUpdater(object sender, EventArgs e)
        {
            Image imgSet;
            Image imgOther;
            Storyboard sb;
            if ((int)Bg1.GetValue(Panel.ZIndexProperty) == 1)
            {
                imgSet = Bg2;
                imgOther = Bg1;
                sb = (Storyboard)this.FindResource("bg2_bg1");
            }
            else
            {
                imgSet = Bg1;
                imgOther = Bg2;
                sb = (Storyboard)this.FindResource("bg1_bg2");
            }
            if (_intCurrentImageIndex + 1 >= _lstImages.Count)
                _intCurrentImageIndex = 0;
            else
                _intCurrentImageIndex++;
            imgSet.Source = _lstImages[_intCurrentImageIndex];
            imgSet.SetValue(Panel.ZIndexProperty, 1);
            imgOther.SetValue(Panel.ZIndexProperty, 0);
            DoubleAnimation da = (DoubleAnimation)sb.Children[1];
            da.From = this.ActualHeight * -1;
            sb.Begin();
        }
        void Auto_update_1sec(object sender, EventArgs e)
        {
            CheckAllErrors();
            DisplayErrors();
            //update server each second if its changed

            if (!ErrorHandler.isError(102))
            {
                if (laucherSettings.GetClientLocation() != serverSettings.GetClientLocation())
                {
                    serverSettings.SetClientLocation(laucherSettings.GetClientLocation());
                }
            }
        }
        #endregion

        #region FUNCTIONS - not used in FORM
            #region Initial error checks
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
            #endregion

            #region Grids Hide ALL ( HideAllGrids() )
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
        #endregion

            #region LOAD - SETTINGS ( LoadAllSettings() )
            private void LoadAllSettings()
                {
                    laucherSettings = new LauncherSettings();
                    ScreenMode.SelectedIndex = laucherSettings.GetScreenMode();
                //replace names if no error exist for client and server
                    if (!ErrorHandler.isError(101))
                        clientWatcher.ChangeAppName(laucherSettings.GetClientFilename());
                    if (!ErrorHandler.isError(102))
                    {
                        serverWatcher.ChangeAppName(laucherSettings.GetServerFilename());
                        serverSettings = new ServerSettings(laucherSettings.GetServerLocation() + @"\data");
                        ProfileSettings = new ProfileSettings(laucherSettings.GetServerLocation() + @"\data\profiles");
                    }
                }
            #endregion

            #region ERROR DISPLAYER - DisplayErrors()
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
                        {
                            ErrorGrid.Visibility = Visibility.Hidden;
                            LoadAllSettings();
                        }
            
                }
        #endregion

            #region LOGIN - TOKEN_CREATOR - AUTOLOGIN HANDLER ( Set_LoginToken(x), Clear_LoginToken(), CreateArguments() )
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
                string mode = ((ScreenMode.SelectedItem as ComboBoxItem).Content as string).ToLowerInvariant();
                ret += (ScreenMode.SelectedIndex != 0) ? " -screenmode=" + ((ScreenMode.SelectedIndex != 2) ? mode + " -popupwindow": mode) : "";
                return ret;
            }
            #endregion
        #endregion

        #region START_PAGE
        private void OnIntro(object sender, RoutedEventArgs e) {
            HideAllGrids();
            IntroGrid.Visibility = Visibility.Visible;
        }
        #endregion

        #region MENU_BAR
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
            if (!ErrorHandler.isError(102))
            {
                HideAllGrids();
                ServerGeneralGrid.Visibility = Visibility.Visible;
                LoadServerGeneralSettings();
            }
        }

        private void OnServerBots(object sender, RoutedEventArgs e)
        {
            if (!ErrorHandler.isError(102))
            {
                HideAllGrids();
                ServerBotsGrid.Visibility = Visibility.Visible;
                LoadServerBotsSettings();
            }
        }

        private void OnServerWeather(object sender, RoutedEventArgs e)
        {
            if (!ErrorHandler.isError(102))
            {
                HideAllGrids();
                ServerWeatherGrid.Visibility = Visibility.Visible;
            }
            // need more code here :)
        }

        private void OnSettings(object sender, RoutedEventArgs e)
        {
            HideAllGrids();
            SettingsGrid.Visibility = Visibility.Visible;
            LoadLauncherSettings();
        }
        #endregion

        #region APPLICATION_START - START BUTTONS HANDLER
        private void OnStartClient(object sender, RoutedEventArgs e)
        {
            if (Tools.ExistF(laucherSettings.GetClientLocation() + @"\" + laucherSettings.GetClientFilename() + ".exe"))
            {
                ErrorHandler.RemoveError(ErrorType.error_Client_noLocation);
                if (clientWatcher.IsProcessAlive())
                {
                    ErrorHandler.AddError(ErrorType.error_ClientAlreadyRunning);
                }
                else
                {
                    ErrorHandler.RemoveError(ErrorType.error_ClientAlreadyRunning);
                    if (Tools.ExistF(laucherSettings.GetServerLocation() + @"\" + laucherSettings.GetServerFilename() + ".exe"))
                    {
                        ClientStarter starter = new ClientStarter(laucherSettings.GetClientLocation(), serverSettings.GetServerBackend(), laucherSettings.GetEmail(), laucherSettings.GetPassword(), laucherSettings.GetClientFilename(), CreateArguments());
                    }
                    else
                    {
                        ErrorHandler.AddError(ErrorType.error_Server_noLocation);
                    }
                }
            }
            else
            {
                ErrorHandler.AddError(ErrorType.error_Client_noLocation);
            }
            DisplayErrors();
        }

        private void OnStartServer(object sender, RoutedEventArgs e)
        {
            string check = laucherSettings.GetServerLocation() + @"\" + laucherSettings.GetServerFilename() + ".exe";
            if (Tools.ExistF(check))
            {
                ErrorHandler.RemoveError(ErrorType.error_Server_noLocation);
                if (serverWatcher.IsProcessAlive())
                {
                    ErrorHandler.AddError(ErrorType.error_ServerAlreadyRunning);
                }
                else
                {
                    ErrorHandler.RemoveError(ErrorType.error_ServerAlreadyRunning);
                    ServerStarter starter = new ServerStarter(laucherSettings.GetServerLocation(), laucherSettings.GetServerFilename());
                }
            }
            else
            {

                ErrorHandler.AddError(ErrorType.error_Client_noLocation);
            }
            DisplayErrors();
        }
        #endregion

        #region ACCOUNT_LOGIN - Handling Account Login Page
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
            if (!ErrorHandler.isError(102))
            {
                if (ProfileSettings.ListExists())
                {
                    if (ProfileSettings.CheckLoginApprove(LoginEmail.Text, LoginPassword.Text))
                    {
                        HideAllGrids();
                        Set_LoginToken(LoginEmail.Text, LoginPassword.Text, true, 0);
                        Characters = new ProfileCharacters(laucherSettings.GetServerLocation() + "/data/profiles/" + ProfileSettings.GetProfile(LoginToken.email, LoginToken.password));
                        LoadAccountSettings();
                        ErrorHandler.RemoveError(ErrorType.error_noUserLikeThis);
                        AccountSettingsGrid.Visibility = Visibility.Visible;
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
            }
            DisplayErrors();
        }
        #endregion

        #region ACCOUNT_REGISTER - Handling Profile creator page
        private void OnRegister(object sender, RoutedEventArgs e)
        {
            if (!ErrorHandler.isError(102))
            {
                HideAllGrids();
                RegisterGrid.Visibility = Visibility.Visible;
            }
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
            tempCharacter.SetProfileID(ID);
            tempCharacter = null;//remove temp characters data
            HideAllGrids();
            LoginGrid.Visibility = Visibility.Visible;
        }
        #endregion

        #region ACCOUNT_SETTINGS - Handling Page buttons after logged in
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

        #region SERVER_GENERAL - Hangling General settings for server
        private void LoadServerGeneralSettings()
        {
            // reload config files
            LoadAllSettings();
               
            // load the settings
            BackendIP.Text = serverSettings.GetServerBackend();
        }
        #endregion

        #region SERVER_BOTS - Handling bots settings inputs/buttons
            private void LoadServerBotsSettings()
            {
                // reload config files
                LoadAllSettings();
                LoadBotsPmcWarSettings();
                LoadBotsLimitSettings();
                LoadBotsSpawnSettings();
      
            }
        
            #region BOTS_PMCWAR
                private void LoadBotsPmcWarSettings()
                {
                    if (!ErrorHandler.isError(102))
                    {
                        PmcWarEnabled.IsChecked = serverSettings.GetBotsPmcWarEnabled();
                        PmcWarUsecChance.Text = serverSettings.GetBotsPmcWarUsecChance();
                    }
                }

                private void OnChangePmcWarEnabled(object sender, RoutedEventArgs e)
                {
                if (!ErrorHandler.isError(102))
                    serverSettings.SetBotsPmcWarEnabled((bool)PmcWarEnabled.IsChecked);
                }

                private void OnChangePmcWarUsecChance(object sender, TextChangedEventArgs e)
                {
                if (!ErrorHandler.isError(102))
                    serverSettings.SetBotsPmcWarUsecChance(PmcWarUsecChance.Text);
                }
            #endregion

            #region BOTS_LIMIT
                private void LoadBotsLimitSettings()
                {
                    if (!ErrorHandler.isError(102))
                    {
                        LimitKilla.Text = serverSettings.GetBotsLimitKilla();
                        LimitBully.Text = serverSettings.GetBotsLimitBully();
                        LimitBullyFollowers.Text = serverSettings.GetBotsLimitBullyFollowers();
                        LimitMarksman.Text = serverSettings.GetBotsLimitMarksman();
                        LimitPmcBot.Text = serverSettings.GetBotsLimitPmcBot();
                        LimitScav.Text = serverSettings.GetBotsLimitScav();
                    }
                }

                private void OnChangeLimitKilla(object sender, TextChangedEventArgs e)
                {
                if (!ErrorHandler.isError(102))
                    serverSettings.SetBotsLimitKilla(LimitKilla.Text);
                }

                private void OnChangeLimitBully(object sender, TextChangedEventArgs e)
                {
                if (!ErrorHandler.isError(102))
                    serverSettings.SetBotsLimitBully(LimitBully.Text);
                }

                private void OnChangeLimitBullyFollower(object sender, TextChangedEventArgs e)
                {
                if (!ErrorHandler.isError(102))
                    serverSettings.SetBotsLimitBullyFollowers(LimitBullyFollowers.Text);
                }

                private void OnChangeLimitMarksman(object sender, TextChangedEventArgs e)
                {
                if (!ErrorHandler.isError(102))
                    serverSettings.SetBotsLimitMarksman(LimitMarksman.Text);
                }

                private void OnChangeLimitPmcBot(object sender, TextChangedEventArgs e)
                {
                if (!ErrorHandler.isError(102))
                    serverSettings.SetBotsLimitPmcBot(LimitPmcBot.Text);
                }

                private void OnChangeLimitScav(object sender, TextChangedEventArgs e)
                {
                if (!ErrorHandler.isError(102))
                    serverSettings.SetBotsLimitScav(LimitScav.Text);
                }
            #endregion

            #region BOTS_SPAWN
                private void LoadBotsSpawnSettings()
                {
                if (!ErrorHandler.isError(102))
                {
                    SpawnGlasses.Text = serverSettings.GetBotsSpawnGlasses();
                    SpawnFaceCover.Text = serverSettings.GetBotsSpawnFaceCover();
                    SpawnHeadwear.Text = serverSettings.GetBotsSpawnHeadwear();
                    SpawnBackpack.Text = serverSettings.GetBotsSpawnBackpack();
                    SpawnArmorVest.Text = serverSettings.GetBotsSpawnArmorVest();
                    SpawnMedPocket.Text = serverSettings.GetBotsSpawnMedPocket();
                    SpawnItemPocket.Text = serverSettings.GetBotsSpawnItemPocket();
                }
                }

                private void OnChangeSpawnGlasses(object sender, TextChangedEventArgs e)
                {
                if (!ErrorHandler.isError(102))
                    serverSettings.SetBotsSpawnGlasses(SpawnGlasses.Text);
                }

                private void OnChangeSpawnFaceCover(object sender, TextChangedEventArgs e)
                {
                if (!ErrorHandler.isError(102))
                    serverSettings.SetBotsSpawnFaceCover(SpawnFaceCover.Text);
                }

                private void OnChangeSpawnHeadwear(object sender, TextChangedEventArgs e)
                {
                if (!ErrorHandler.isError(102))
                    serverSettings.SetBotsSpawnHeadwear(SpawnHeadwear.Text);
                }

                private void OnChangeSpawnBackpack(object sender, TextChangedEventArgs e)
                {
                if (!ErrorHandler.isError(102))
                    serverSettings.SetBotsSpawnBackpack(SpawnBackpack.Text);
                }

                private void OnChangeSpawnArmorVest(object sender, TextChangedEventArgs e)
                {
                if (!ErrorHandler.isError(102))
                    serverSettings.SetBotsSpawnArmorVest(SpawnArmorVest.Text);
                }

                private void OnChangeSpawnMedsPocket(object sender, TextChangedEventArgs e)
                {
                if (!ErrorHandler.isError(102))
                    serverSettings.SetBotsSpawnMedPocket(SpawnMedPocket.Text);
                }

                private void OnChangeSpawnItemPocket(object sender, TextChangedEventArgs e)
                {
                if (!ErrorHandler.isError(102))
                    serverSettings.SetBotsSpawnItemPocket(SpawnItemPocket.Text);
                }
            #endregion
        #endregion

        #region LAUNCHER_SETTINGS
            private void LoadLauncherSettings()
            {
                // reload config files
                LoadAllSettings();
                    //load locations
                ClientLocation.Text = laucherSettings.GetClientLocation();
                ServerLocation.Text = laucherSettings.GetServerLocation();
                    //load filenames
                ClientFileName.Text = laucherSettings.GetClientFilename();
                ServerFileName.Text = laucherSettings.GetServerFilename();
                    //load screenmode for game launch
                ScreenMode.SelectedIndex = laucherSettings.GetScreenMode();
            }

        #region CLIENT INPUTS CHANGED
            private void OnChangeClientLocation(object sender, TextChangedEventArgs e)
            {
                laucherSettings.SetClientLocation(ClientLocation.Text);
                if (Tools.ExistF(laucherSettings.GetClientLocation() + @"\" + laucherSettings.GetClientFilename() + ".exe"))
                    ErrorHandler.RemoveError(ErrorType.error_Client_noLocation);
                else
                    ErrorHandler.AddError(ErrorType.error_Client_noLocation);
                DisplayErrors();
                LoadAllSettings();
            }
            private void OnChangeClientFilename(object sender, TextChangedEventArgs e)
            {
                laucherSettings.SetClientFilename(ClientFileName.Text);
                if (Tools.ExistF(laucherSettings.GetClientLocation() + @"\" + laucherSettings.GetClientFilename() + ".exe"))
                {
                    clientWatcher.ChangeAppName(laucherSettings.GetClientFilename());
                    ErrorHandler.RemoveError(ErrorType.error_Client_noLocation);
                }
                else
                    ErrorHandler.AddError(ErrorType.error_Client_noLocation);
                DisplayErrors();
                LoadAllSettings();
            }
        #endregion

        #region SERVER INPUTS CHANGED
        private void OnChangeServerLocation(object sender, TextChangedEventArgs e)
        {
            laucherSettings.SetServerLocation(ServerLocation.Text);
            if (Tools.ExistF(laucherSettings.GetServerLocation() + @"\" + laucherSettings.GetServerFilename() + ".exe"))
                ErrorHandler.RemoveError(ErrorType.error_Server_noLocation);
            else
                ErrorHandler.AddError(ErrorType.error_Server_noLocation);
            LoadAllSettings();
        }
        private void OnChangeServerFilename(object sender, TextChangedEventArgs e)
        {
            laucherSettings.SetServerFilename(ServerFileName.Text);
            if (Tools.ExistF(laucherSettings.GetServerLocation() + @"\" + laucherSettings.GetServerFilename() + ".exe"))
            {
                serverWatcher.ChangeAppName(laucherSettings.GetServerFilename());
                ErrorHandler.RemoveError(ErrorType.error_Server_noLocation);
            }
            else
                ErrorHandler.AddError(ErrorType.error_Server_noLocation);
            LoadAllSettings();
        }
        #endregion
        #endregion

        #region Change Screen Mode
        public string MyDocumentsEFTSettings = "";
        dynamic profile_content = JsonConvert.DeserializeObject("{\"Resolution\":{\"Width\":1920,\"Height\":1080,\"RefreshRate\":60,\"IsWideScreen\":false},\"ResolutionIndex\":15,\"AspectRatio\":1,\"GraphicsQuality\":3,\"CustomGraphicsQuality\":false,\"MultimonitorSupport\":false,\"VSync\":0,\"IsFullscreen\":false,\"LobbyFramerate\":30,\"GameFramerate\":119,\"TextureQuality\":2,\"ShadowsQuality\":3,\"LightingQuality\":0,\"ObjectLodQuality\":0,\"ContactSSAO\":3,\"AnisotropicFiltering\":2,\"OverallVisibility\":5,\"ShadowVisibility\":17,\"Ssao\":2,\"Sharpen\":7,\"SSR\":0,\"AntiAliasing\":1,\"Bloom\":1,\"ChromaticAberrations\":1,\"Noise\":1,\"Hdr\":1,\"ZBlur\":1}");
    
        //script to change client settings
        private void ScreenMode_DropDownClosed(object sender, EventArgs e)
        {
            // 0 no changes, 1 Fullscreen, 2 Borderless, 3 Windowed //
            int i = ScreenMode.SelectedIndex;
            if (i != 0)
            {
                laucherSettings.SetScreenMode(i);
                string testDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Escape from Tarkov\";
                if (Tools.ExistD(testDir))
                {
                    MyDocumentsEFTSettings = testDir + @"local.ini";
                    if (Tools.ExistF(MyDocumentsEFTSettings))
                    {
                        using (StreamReader sr = new StreamReader(MyDocumentsEFTSettings))
                        {
                            string json = sr.ReadToEnd();
                            profile_content = JsonConvert.DeserializeObject(json);
                        }
                        bool changedFullScreen = true;
                        if (i == 3 || i == 2) // for Windowed and Borderless
                            changedFullScreen = false;

                        if (i != 0)
                            profile_content.IsFullscreen = changedFullScreen;

                        JsonSerializer serializer = new JsonSerializer
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        };
                        using (StreamWriter sw = new StreamWriter(MyDocumentsEFTSettings))
                        {
                            serializer.Serialize(sw, profile_content);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Escape from Tarkov/local.ini not found");
                    }
                }
            }
        }
        #endregion
    }
}
