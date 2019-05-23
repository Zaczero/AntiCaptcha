# ![2Captcha logo](https://i.imgur.com/U0qKP3j.png)

![](https://img.shields.io/github/release/Zaczero/AntiCaptcha.svg)
![](https://img.shields.io/nuget/v/AntiCaptchaAPI.svg)
![](https://img.shields.io/github/license/Zaczero/AntiCaptcha.svg)

Simple HTTP API wrapper for https://anti-captcha.com/  
An online captcha solving and image recognition service.

## 🔗 Download
* Latest release: https://github.com/Zaczero/AntiCaptcha/releases/latest

## ☕ Support me
If you find this project useful and you are new to anti captcha please consider registering from my [referrral link](http://getcaptchasolution.com/i4lbjatsex).

## 🏁 Sample code

```cs
var antiCaptcha = new AntiCaptcha(" ## YOUR API KEY ## ");

// Get current balance
var balance = await antiCaptcha.GetBalance();

// Solve image captcha
var image = await antiCaptcha.SolveImage("data:image/png;base64,iVBORw0KGgo...");

// Solve ReCaptchaV2
var recaptcha = await antiCaptcha.SolveReCaptchaV2("GOOGLE_SITE_KEY", "https://example.com");
var recaptchaInvisible = await antiCaptcha.SolveReCaptchaV2("GOOGLE_SITE_KEY", "https://example.com", true);

// Solve FunCaptcha
var fun = await antiCaptcha.SolveFunCaptcha("FUN_CAPTCHA_PUBLIC_KEY", "https://example.com");

// Solve SquareNet
var square = await antiCaptcha.SolveSquareNet("data:image/png;base64,iVBORw0KGgo...", "banana", 3, 3);

// Solve GeeTest
var gee = await antiCaptcha.SolveGeeTest("GEE_TEST_KEY", "https://example.com", "CHALLENGE");

Debugger.Break();
```

### And here is the result structure *(same for all methods)*:

```cs
public struct AntiCaptchaResult
{
	public bool Success;
	public string Response;

	public AntiCaptchaResult(bool success, string response)
	{
		Success = success;
		Response = response;
	}
}
```

## 📎 License

MIT License

Copyright (c) 2019 Kamil Monicz

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
