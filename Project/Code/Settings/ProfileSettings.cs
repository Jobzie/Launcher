using Launcher.Code.Data;

namespace Launcher.Code.Settings
{
	public class ProfileSettings : SettingsBase<ProfileData>
	{
		public ProfileSettings(string filepath) : base(filepath, "profiles.json")
		{
			// for calling base constructor
		}

		public int GetProfile(string email, string password)
		{
			foreach (Profile profile in base.config.profiles)
			{
				if (profile.email == email && profile.password == password)
				{
					return profile.id;
				}
			}

			return base.config.profiles.Count;
		}

		public void AddProfile(string email, string password, string side)
		{
			Profile profile = new Profile();
			profile.email = email;
			profile.password = password;

			// profile exist
			if (GetProfile(email, password) < base.config.profiles.Count)
			{
				return;
			}

			// add the profile
			base.config.profiles.Add(profile);
			base.SaveSettings();

			// TODO: create character with side
		}

		public void RemoveProfile(string email, string password)
		{
			int profileID = GetProfile(email, password);

			// profile doesn't exist
			if (profileID >= base.config.profiles.Count)
			{
				return;
			}

			// remove the profile
			base.config.profiles.Remove(base.config.profiles[profileID]);
			base.SaveSettings();
		}

		public void ChangeProfileEmail(string email, string password, string newEmail)
		{
			int profileID = GetProfile(email, password);

			// profile doesn't exist
			if (profileID >= base.config.profiles.Count)
			{
				return;
			}

			// change the profile email
			base.config.profiles[profileID].email = newEmail;
			base.SaveSettings();
		}

		public void ChangeProfilePassword(string email, string password, string newPassword)
		{
			int profileID = GetProfile(email, password);

			// profile doesn't exist
			if (profileID >= base.config.profiles.Count)
			{
				return;
			}

			// change the profile password
			base.config.profiles[profileID].password = newPassword;
			base.SaveSettings();
		}

		public void ChangeProfileNickname(string email, string password, string nickname)
		{
			int profileID = GetProfile(email, password);

			// profile doesn't exist
			if (profileID >= base.config.profiles.Count)
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
			if (profileID >= base.config.profiles.Count)
			{
				return;
			}

			// change the profile appearance
			// code here
			base.SaveSettings();
		}
	}
}
