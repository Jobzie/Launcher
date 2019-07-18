using Launcher.Code.Data;

namespace Launcher.Code.Settings
{
    public class ProfileSettings : SettingsBase<ProfileData>
    {
        public ProfileSettings(string filepath, string filename = "profiles.json") : base(filepath, filename, true) // last parameter is for object oriented return
        {
            // for calling base constructor
        }

        public bool ListExists() {
                return (base.configObject.Count > 0) ? true : false;
        }
        public bool CheckLoginApprove(string email, string password)
        {
            foreach (ProfileData profile in base.configObject)
            {
                if (profile.email == email && profile.password == password)
                {
                    return true;
                }
            }
            return false;
        }

        public int GetProfile(string email, string password)
        {
            foreach (ProfileData profile in base.configObject)
            {
                if (profile.email == email && profile.password == password)
                {
                    return profile.id;
                }
            }

            return base.configObject.Count;
        }

        public void AddProfile(string email, string password, string side)
        {
            ProfileData profile = new ProfileData();
            profile.email = email;
            profile.password = password;

            // profile exist
            if (GetProfile(email, password) < base.configObject.Count)
            {
                return;
            }

            // add the profile
            base.configObject.Add(profile);
            base.SaveSettings();

            // TODO: create character with side
        }

        public void RemoveProfile(string email, string password)
        {
            int profileID = GetProfile(email, password);

            // profile doesn't exist
            if (profileID >= base.configObject.Count)
            {
                return;
            }

            // remove the profile
            base.configObject.Remove(base.configObject[profileID]);
            base.SaveSettings();
        }

        public void ChangeProfileEmail(string email, string password, string newEmail)
        {
            int profileID = GetProfile(email, password);

            // profile doesn't exist
            if (profileID >= base.configObject.Count)
            {
                return;
            }

            // change the profile email
            base.configObject[profileID].email = newEmail;
            base.SaveSettings();
        }

        public void ChangeProfilePassword(string email, string password, string newPassword)
        {
            int profileID = GetProfile(email, password);

            // profile doesn't exist
            if (profileID >= base.configObject.Count)
            {
                return;
            }

            // change the profile password
            base.configObject[profileID].password = newPassword;
            base.SaveSettings();
        }

        public void ChangeProfileNickname(string email, string password, string nickname)
        {
            int profileID = GetProfile(email, password);

            // profile doesn't exist
            if (profileID >= base.configObject.Count)
            {
                return;
            }

            // change the profile nickname
            // code here
            base.SaveSettings();
        }

        public void ChangeProfileAppearance(string email, string password, string body, string hands, string head, string legs)
        {
            int profileID = GetProfile(email, password);

            // profile doesn't exist
            if (profileID >= base.configObject.Count)
            {
                return;
            }

            // change the profile appearance
            // code here
            base.SaveSettings();
        }
    }
}
