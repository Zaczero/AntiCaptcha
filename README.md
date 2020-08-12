# ![Zaczero/AntiCaptcha logo](https://github.com/Zaczero/AntiCaptcha/blob/master/resources/AntiCaptcha.png)

[![Build Status](https://travis-ci.com/Zaczero/AntiCaptcha.svg?branch=master)](https://travis-ci.com/Zaczero/AntiCaptcha)
[![GitHub Release](https://img.shields.io/github/v/release/Zaczero/AntiCaptcha)](https://github.com/Zaczero/AntiCaptcha/releases/latest)
[![NuGet Release](https://img.shields.io/nuget/v/AntiCaptchaAPI)](https://www.nuget.org/packages/AntiCaptchaAPI/)
[![License](https://img.shields.io/github/license/Zaczero/AntiCaptcha)](https://github.com/Zaczero/AntiCaptcha/blob/master/LICENSE)

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
/*
 * Class initialization
 * Optionally you can pass 2nd parameter `httpClient` with custom HttpClient to use while requesting API
 */
var captcha = new AntiCaptcha("API_KEY");
var captchaCustomHttp = new AntiCaptcha("API_KEY", new HttpClient());

/*
 * Get current balance
 */
var balance = await captcha.GetBalance();

/*
 * Type: Image
 *
 * Documentation (anti-captcha): https://anti-captcha.com/apidoc/image
 */
var image = await captcha.SolveImage("BASE64_IMAGE");

/*
 * Type: ReCaptcha V2
 * Optionally you can pass 3rd parameter `isInvisible` to indicate if the reCaptcha is setup as invisible
 *
 * Homepage: https://www.google.com/recaptcha/
 * Documentation (vendor): https://developers.google.com/recaptcha/docs/display
 * Documentation (anti-captcha): https://anti-captcha.com/apidoc/recaptcha
 */
var reCaptcha = await captcha.SolveReCaptchaV2("SITE_KEY", "https://WEBSITE_URL");
var reCaptchaInvisible = await captcha.SolveReCaptchaV2("SITE_KEY", "https://WEBSITE_URL", true);
var reCaptchaWithProxy = await captcha.SolveReCaptchaV2("SITE_KEY", "https://WEBSITE_URL", new AntiCaptchaProxy(ProxyType.Http, "PROXY_ADDRESS", 8080), "USER_AGENT");

/*
 * Type: ReCaptcha V3
 * If you get ERROR_INCORRECT_SESSION_DATA error you may need to increase minScore value
 *
 * Homepage: https://www.google.com/recaptcha/
 * Documentation (vendor): https://developers.google.com/recaptcha/docs/v3
 * Documentation (anti-captcha): https://anti-captcha.com/apidoc/recaptcha
 */
var reCaptchaV3 = await captcha.SolveReCaptchaV3("SITE_KEY", "https://WEBSITE_URL", 0.9, "SOME_ACTION");

/*
 * Type: hCaptcha
 *
 * Homepage: https://www.hcaptcha.com/
 * Documentation (vendor): https://docs.hcaptcha.com/
 * Documentation (anti-captcha): https://anti-captcha.com/apidoc/hcaptcha
 */
var hCaptcha = await captcha.SolveHCaptcha("SITE_KEY", "https://WEBSITE_URL");
var hCaptchaWithProxy = await captcha.SolveHCaptcha("SITE_KEY", "https://WEBSITE_URL", new AntiCaptchaProxy(ProxyType.Http, "PROXY_ADDRESS", 8080), "USER_AGENT");

/*
 * Type: GeeTest
 *
 * Homepage: https://www.geetest.com/en
 * Documentation (vendor): https://docs.geetest.com/en
 * Documentation (anti-captcha): https://anti-captcha.com/apidoc/geetest
 */
var geeTest = await captcha.SolveGeeTest("SITE_KEY", "https://WEBSITE_URL", "CHALLENGE");
var geeTestWithProxy = await captcha.SolveGeeTest("SITE_KEY", "https://WEBSITE_URL", "CHALLENGE", new AntiCaptchaProxy(ProxyType.Http, "PROXY_ADDRESS", 8080), "USER_AGENT");

/*
 * Type: FunCaptcha
 *
 * Documentation (anti-captcha): https://anti-captcha.com/apidoc/funcaptcha
 */
var funCaptcha = await captcha.SolveFunCaptcha("SITE_KEY", "https://WEBSITE_URL");
var funCaptchaWithProxy = await captcha.SolveFunCaptcha("SITE_KEY", "https://WEBSITE_URL", new AntiCaptchaProxy(ProxyType.Http, "PROXY_ADDRESS", 8080), "USER_AGENT");

/*
 * Type: SquareNet
 */
var squareNet = await captcha.SolveSquareNet("BASE64_IMAGE", "OBJECT_NAME", 3, 3);
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
