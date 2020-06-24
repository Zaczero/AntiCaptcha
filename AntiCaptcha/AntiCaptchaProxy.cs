namespace AntiCaptchaAPI
{
	public struct AntiCaptchaProxy
	{
		public ProxyType Type;
		public string Address;
		public int Port;

		public string Login;
		public string Password;

		public AntiCaptchaProxy(ProxyType type, string address, int port, string login = null, string password = null)
		{
			Type = type;
			Address = address;
			Port = port;

			Login = login;
			Password = password;
		}

		public bool IsAuth()
		{
			return !string.IsNullOrEmpty(Login);
		}
	}
}
