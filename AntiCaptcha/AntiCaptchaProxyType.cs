using System;

namespace AntiCaptchaAPI
{
	public enum ProxyType
	{
		Http,
		Https,
		Socks4,
		Socks5,
	}

	internal static class ProxyTypeEx
	{
		internal static string GetExtension(this ProxyType proxyType)
		{
			switch (proxyType)
			{
				case ProxyType.Http:
					return "http";
				case ProxyType.Https:
					return "https";
				case ProxyType.Socks4:
					return "socks4";
				case ProxyType.Socks5:
					return "socks5";
				default:
					throw new ArgumentOutOfRangeException(nameof(proxyType), proxyType, "Unsupported proxy type");
			}
		}
	}
}
