using System.Diagnostics;
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
			var antiCaptcha = new AntiCaptcha(" ## YOUR API KEY ## ");

			// Get current balance
			var balance = await antiCaptcha.GetBalance();

			// Solve image captcha
			var image = await antiCaptcha.SolveImage("iVBORw0KGgo...");
			
			// Solve ReCaptchaV2
			var recaptcha = await antiCaptcha.SolveReCaptchaV2("GOOGLE_SITE_KEY", "https://example.com");
			var recaptchaInvisible = await antiCaptcha.SolveReCaptchaV2("GOOGLE_SITE_KEY", "https://example.com", true);

			// Solve FunCaptcha
			var fun = await antiCaptcha.SolveFunCaptcha("FUN_CAPTCHA_PUBLIC_KEY", "https://example.com");

			// Solve SquareNet
			var square = await antiCaptcha.SolveSquareNet("iVBORw0KGgo...", "banana", 3, 3);

			// Solve GeeTest
			var gee = await antiCaptcha.SolveGeeTest("GEE_TEST_KEY", "https://example.com", "CHALLENGE");

			Debugger.Break();
		}
	}
}
