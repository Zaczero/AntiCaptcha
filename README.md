# ![Zaczero/AntiCaptcha logo](https://github.com/Zaczero/AntiCaptcha/blob/master/resources/AntiCaptcha.png)

![github version](https://img.shields.io/github/release/Zaczero/AntiCaptcha.svg)
![nuget version](https://img.shields.io/nuget/v/AntiCaptchaAPI.svg)
![license type](https://img.shields.io/github/license/Zaczero/AntiCaptcha.svg)

Simple HTTP API wrapper for [anti-captcha.com](https://anti-captcha.com/)  
An online captcha solving and image recognition service.

## 🌤️ Installation

### Install with NuGet (recommended)

`Install-Package AntiCaptchaAPI`

### Install manually

[Browse latest GitHub release](https://github.com/Zaczero/AntiCaptcha/releases/latest)

## 🏁 Getting started

### Sample code

```cs
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
```

### And here is the result structure *(the same for all methods)*

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

## Footer

### 📧 Contact

* Email: [kamil@monicz.pl](mailto:kamil@monicz.pl)

### 📃 License

* [Zaczero/AntiCaptcha](https://github.com/Zaczero/AntiCaptcha/blob/master/LICENSE)
* [JamesNK/Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json/blob/master/LICENSE.md)
