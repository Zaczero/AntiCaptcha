using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace _AntiCaptcha
{
	public class AntiCaptcha
	{
#if NETSTANDARD2_0
		[Serializable]
#endif
		private struct AntiCaptchaApiResponse
		{
			public int ErrorId;
			public string ErrorCode;
			public int TaskId;
			public string Status;
			public Dictionary<string, object> Solution;
			public float Balance;
		}

		private const string BaseUrl = "https://api.anti-captcha.com/";

		private readonly HttpClient _httpClient;
		private readonly string _apiKey;

		public AntiCaptcha(string apiKey, HttpClient httpClient = null)
		{
			_httpClient = httpClient ?? new HttpClient();
			_apiKey = apiKey;
		}

		public async Task<AntiCaptchaResult> GetBalance(CancellationToken cancellationToken = default)
		{
			var content = new Dictionary<string, object>
			{
				{"clientKey", _apiKey},
			};

			var contentJson = JsonConvert.SerializeObject(content);
			var inResponse = await _httpClient.PostAsync(BaseUrl + "getBalance", new StringContent(contentJson), cancellationToken).ConfigureAwait(false);
			var inJson = await inResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

			var @in = JsonConvert.DeserializeObject<AntiCaptchaApiResponse>(inJson);
			if (@in.ErrorId != 0)
			{
				return new AntiCaptchaResult(false, @in.ErrorCode);
			}

			return new AntiCaptchaResult(true, @in.Balance.ToString());
		}
		
		private async Task<AntiCaptchaResultInternal> Solve(int delaySeconds, IDictionary<string, object> content, CancellationToken cancellationToken = default)
		{
			content["clientKey"] = _apiKey;
			content["softId"] = 935;
			
			var contentJson = JsonConvert.SerializeObject(content);
			var inResponse = await _httpClient.PostAsync(BaseUrl + "createTask", new StringContent(contentJson), cancellationToken).ConfigureAwait(false);
			var inJson = await inResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

			var @in = JsonConvert.DeserializeObject<AntiCaptchaApiResponse>(inJson);
			if (@in.ErrorId != 0)
			{
				return new AntiCaptchaResultInternal(false, @in.ErrorCode, null);
			}
			
			await Task.Delay(delaySeconds * 1000, cancellationToken).ConfigureAwait(false);
			return await GetResponse(@in.TaskId, cancellationToken).ConfigureAwait(false);
		}

		private async Task<AntiCaptchaResultInternal> GetResponse(int taskId, CancellationToken cancellationToken = default)
		{
			var content = new Dictionary<string, object>
			{
				{"clientKey", _apiKey},
				{"taskId", taskId},
			};
			
			var contentJson = JsonConvert.SerializeObject(content);

			while (true)
			{
				var response = await _httpClient.PostAsync(BaseUrl + "getTaskResult", new StringContent(contentJson), cancellationToken).ConfigureAwait(false);
				var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

				var res = JsonConvert.DeserializeObject<AntiCaptchaApiResponse>(responseJson);
				if (res.ErrorId != 0)
				{
					return new AntiCaptchaResultInternal(false, res.ErrorCode, null);
				}

				if (res.Status == "processing")
				{
					await Task.Delay(5 * 1000, cancellationToken).ConfigureAwait(false);
					continue;
				}

				return new AntiCaptchaResultInternal(true, null, res.Solution);
			}
		}



		public async Task<AntiCaptchaResult> SolveImage(string imageBase64, 
			bool phrase = false, 
			bool @case = false, 
			int numeric = 0, 
			bool math = false, 
			int minLength = 0, 
			int maxLength = 0, 
			string comment = null,
			CancellationToken cancellationToken = default)
		{
			var result = await Solve(5, new Dictionary<string, object>
				{
					{"task", new Dictionary<string, object>
						{
							{"type", "ImageToTextTask"},
							{"body", imageBase64},
							{"phrase", phrase},
							{"case", @case},
							{"numeric", numeric},
							{"math", math},
							{"minLength", minLength},
							{"maxLength", maxLength},
							{"comment", comment},
						}
					}
				}, cancellationToken).ConfigureAwait(false);

			if (!result.Success)
				return new AntiCaptchaResult(false, result.Response);

			return new AntiCaptchaResult(true, result.Dictionary["text"].ToString());
		}

		public async Task<AntiCaptchaResult> SolveReCaptchaV2(string googleSiteKey, string pageUrl, bool isInvisible = false, CancellationToken cancellationToken = default)
		{
			var result = await Solve(10, new Dictionary<string, object>
			{
				{"task", new Dictionary<string, object>
					{
						{"type", "NoCaptchaTaskProxyless"},
						{"websiteURL", pageUrl},
						{"websiteKey", googleSiteKey},
						{"isInvisible", isInvisible},
					}
				}
			}, cancellationToken).ConfigureAwait(false);

			if (!result.Success)
				return new AntiCaptchaResult(false, result.Response);

			return new AntiCaptchaResult(true, result.Dictionary["gRecaptchaResponse"].ToString());
		}

		public async Task<AntiCaptchaResult> SolveReCaptchaV3(string googleSiteKey, string pageUrl, double minScore, string pageAction, CancellationToken cancellationToken = default)
		{
			var result = await Solve(5, new Dictionary<string, object>
			{
				{"task", new Dictionary<string, object>
					{
						{"type", "RecaptchaV3TaskProxyless"},
						{"websiteURL", pageUrl},
						{"websiteKey", googleSiteKey},
						{"minScore", minScore},
						{"pageAction", pageAction},
					}
				}
			}, cancellationToken).ConfigureAwait(false);

			if (!result.Success)
				return new AntiCaptchaResult(false, result.Response);

			return new AntiCaptchaResult(true, result.Dictionary["gRecaptchaResponse"].ToString());
		}

		public async Task<AntiCaptchaResult> SolveHCaptcha(string siteKey, string pageUrl, CancellationToken cancellationToken = default)
		{
			var result = await Solve(10, new Dictionary<string, object>
			{
				{"task", new Dictionary<string, object>
					{
						{"type", "HCaptchaTaskProxyless"},
						{"websiteURL", pageUrl},
						{"websiteKey", siteKey},
					}
				}
			}, cancellationToken).ConfigureAwait(false);

			if (!result.Success)
				return new AntiCaptchaResult(false, result.Response);

			return new AntiCaptchaResult(true, result.Dictionary["gRecaptchaResponse"].ToString());
		}

		public async Task<AntiCaptchaResult> SolveFunCaptcha(string funCaptchaPublicKey, string pageUrl, CancellationToken cancellationToken = default)
		{
			var result = await Solve(10, new Dictionary<string, object>
			{
				{"task", new Dictionary<string, object>
					{
						{"type", "FunCaptchaTaskProxyless"},
						{"websiteURL", pageUrl},
						{"websitePublicKey", funCaptchaPublicKey},
					}
				}
			}, cancellationToken).ConfigureAwait(false);

			if (!result.Success)
				return new AntiCaptchaResult(false, result.Response);

			return new AntiCaptchaResult(true, result.Dictionary["token"].ToString());
		}

		public async Task<AntiCaptchaResult> SolveSquareNet(string imageBase64, string objectName, int rowsCount, int columnsCount, CancellationToken cancellationToken = default)
		{
			var result = await Solve(5, new Dictionary<string, object>
			{
				{"task", new Dictionary<string, object>
					{
						{"type", "SquareNetTask"},
						{"body", imageBase64},
						{"objectName", objectName},
						{"rowsCount", rowsCount},
						{"columnsCount", columnsCount},
					}
				}
			}, cancellationToken).ConfigureAwait(false);

			if (!result.Success)
				return new AntiCaptchaResult(false, result.Response);

			return new AntiCaptchaResult(true, result.Dictionary["cellNumbers"].ToString());
		}

		public async Task<AntiCaptchaResult> SolveGeeTest(string geeTestKey, string pageUrl, string challenge, CancellationToken cancellationToken = default)
		{
			var result = await Solve(10, new Dictionary<string, object>
			{
				{"task", new Dictionary<string, object>
					{
						{"type", "GeeTestTaskProxyless"},
						{"websiteURL", pageUrl},
						{"gt", geeTestKey},
						{"challenge", challenge},
					}
				}
			}, cancellationToken).ConfigureAwait(false);

			if (!result.Success)
				return new AntiCaptchaResult(false, result.Response);

			return new AntiCaptchaResult(true, $"{result.Dictionary["challenge"]};{result.Dictionary["validate"]};{result.Dictionary["seccode"]}");
		}
	}
}
