namespace EmuTarkov_Launcher
{
	public class LoginToken
	{
		public string email;
		public string password;
		public bool toggle;
		public long timestamp;

		public LoginToken(string email, string password)
		{
			this.email = email;
			this.password = password;
			toggle = true;
			timestamp = 1577572839;	// 23:00:00 28-12-2019, used to unlock christmas tree in hideout
		}
	}
}
