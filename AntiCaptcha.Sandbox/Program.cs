using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace _AntiCaptcha.Test
{
	class Program
	{
		static void Main(string[] args)
		{
			Foo().GetAwaiter().GetResult();
		}

		static async Task Foo()
		{
			var captcha = new AntiCaptcha(" ## YOUR API KEY ## ");
			// .. additionally you can pass your own httpClient class
			var captchaWithHttpClient = new AntiCaptcha(" ## YOUR API KEY ## ", new HttpClient());

			// Get current balance
			var balance = await captcha.GetBalance();

			// Solve image captcha
			var image = await captcha.SolveImage("iVBORw0KGgo...");
			
			// Solve ReCaptchaV2
			var recaptcha = await captcha.SolveReCaptchaV2("GOOGLE_SITE_KEY", "https://example.com");
			var recaptchaInvisible = await captcha.SolveReCaptchaV2("GOOGLE_SITE_KEY", "https://example.com", true);

			// Solve FunCaptcha
			var fun = await captcha.SolveFunCaptcha("FUN_CAPTCHA_PUBLIC_KEY", "https://example.com");

			// Solve SquareNet
			var square = await captcha.SolveSquareNet("iVBORw0KGgo...", "banana", 3, 3);

			// Solve GeeTest
			var gee = await captcha.SolveGeeTest("GEE_TEST_KEY", "https://example.com", "CHALLENGE");

			Debugger.Break();
		}
	}
}
