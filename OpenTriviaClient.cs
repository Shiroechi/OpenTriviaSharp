using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using OpenTriviaSharp.Exceptions;
using OpenTriviaSharp.Models;

namespace OpenTriviaSharp
{
	/// <summary>
	/// Open Trivia Database client.
	/// </summary>
	public class OpenTriviaClient
	{
		#region Member

		private HttpClient _HttpClient;
		private bool _Supplied;
		private string _BaseApiUrl = "https://opentdb.com/api.php?";
		private string _BaseTokenApiUrl = "https://opentdb.com/api.php?";
		private string _SessionToken;

		#endregion Member

		#region Constructor & Destructor

		/// <summary>
		/// Create <see cref="OpenTriviaClient"/> instance.
		/// </summary>
		/// <param name="client">
		///		<see cref="HttpClient"/> object to send and receiving response.
		/// </param>
		/// <param name="sessionToken">
		///		Session token of Open Trivia for tracking requested question.
		/// </param>
		public OpenTriviaClient(HttpClient client = null, string sessionToken = null)
		{
			if (client == null)
			{
				this._HttpClient = new HttpClient();
				this._Supplied = false;
			}
			else
			{
				this._HttpClient = client;
				this._Supplied = true;
			}

			this.AddUserAgent();

			if(token == null || token.Trim() == "")
			{
				token = "";
			}
			else
			{
				this.Token = token;
			}

			ServicePointManager.SecurityProtocol =
				SecurityProtocolType.Tls12 |
				SecurityProtocolType.Tls11 |
				SecurityProtocolType.Tls |
				SecurityProtocolType.Ssl3;
		}

		/// <summary>
		/// Release all resource that this object handle.
		/// </summary>
		~OpenTriviaClient()
		{
			if (this._Supplied == false)
			{
				this._HttpClient.Dispose();
			}
		}

		#endregion Constructor & Destructor

		#region Properties

		/// <summary>
		/// Get current token.
		/// </summary>
		public string Token
		{
			set
			{
				if (value == null || value.Trim() == "")
				{
					return;
				}

				this._SessionToken = value;
			}
			get
			{
				return this._SessionToken;
			}
		}

		#endregion Properties

		#region Protected Method

		/// <summary>
		///		Get JSON response from url.
		/// </summary>
		/// <typeparam name="T">
		///		The type of the object to deserialize.
		///	</typeparam>
		/// <param name="url">
		///		API URL. 
		/// </param>
		/// <returns>
		///		The instance of <typeparamref name="T"/> being deserialized.
		///	</returns>
		/// <exception cref="OpenTriviaException">
		///		Unexpected error occured.
		/// </exception>
		/// <exception cref="HttpRequestException">
		///		The request failed due to an underlying issue such as network connectivity, DNS
		///     failure, server certificate validation or timeout.
		/// </exception>
		/// <exception cref="JsonException">
		///		The JSON is invalid.
		/// </exception>
		protected async Task<T> GetJsonResponseAsync<T>(string url)
		{
			try
			{
				using (var request = new HttpRequestMessage(HttpMethod.Get, url))
				using (var response = await this._HttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
				using (var stream = await response.Content.ReadAsStreamAsync())
				{
					if (response.IsSuccessStatusCode)
					{
						try
						{
							return await JsonSerializer.DeserializeAsync<T>(stream);
						}
						catch (JsonException)
						{
							throw;
						}
					}

					throw new OpenTriviaException(
						$"Unexpected error occured.\n Status code = { (int)response.StatusCode }\n Reason = { response.ReasonPhrase }.");
				}
			}
			catch (HttpRequestException)
			{
				throw;
			}
		}

		/// <summary>
		/// Read JSON data.
		/// </summary>
		/// <param name="item"></param>
		/// <returns>
		///		A <see cref="TriviaQuestion"/>.
		/// </returns>
		protected TriviaQuestion ReadTriviaQuestion(JsonElement item)
		{
			var incorrect = new List<string>();

			foreach (var ans in item.GetProperty("incorrect_answers").EnumerateArray())
			{
				incorrect.Add(ans.GetString());
			}

			return new TriviaQuestion(
				this.DetermineCategory(item.GetProperty("category").GetString()), 
				this.DetermineType(item.GetProperty("type").GetString()), 
				this.DetermineDifficulty(item.GetProperty("difficulty").GetString()), 
				item.GetProperty("question").GetString(), 
				item.GetProperty("correct_answer").GetString(),
				incorrect.ToArray()
				);
		}

		/// <summary>
		/// Add browser user agent to <see cref="HttpClient"/>.
		/// </summary>
		protected void AddUserAgent()
		{
			if (this._HttpClient == null)
			{
				return;
			}

			if (this._HttpClient.DefaultRequestHeaders.UserAgent.Count == 0)
			{
				this._HttpClient.DefaultRequestHeaders.Add(
					"User-Agent",
					"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.66 Safari/537.36");
			}
		}

		/// <summary>
		///		Convert JSON reponse category property to <see cref="Category"/>.
		/// </summary>
		/// <param name="category">
		///		Category string from JSON response.
		/// </param>
		/// <returns>
		///		<see cref="Category"/> based on paramater <paramref name="category"/>.
		/// </returns>
		protected Category DetermineCategory(string category)
		{
			if (category.Contains("General"))
			{
				return Category.General;
			}
			else if (category.Contains("Books"))
			{
				return Category.Books;
			}
			else if (category.Contains("Film"))
			{
				return Category.Film;
			}
			else if (category.Contains("Music"))
			{
				return Category.Music;
			}
			else if (category.Contains("Theatres"))
			{
				return Category.Theatres;
			}
			else if (category.Contains("Television"))
			{
				return Category.Television;
			}
			else if (category.Contains("Video Games"))
			{
				return Category.VideoGames;
			}
			else if (category.Contains("Board Games"))
			{
				return Category.BoardGames;
			}
			else if (category.Contains("Science"))
			{
				return Category.Science;
			}
			else if (category.Contains("Computers"))
			{
				return Category.Computers;
			}
			else if (category.Contains("Mathematics"))
			{
				return Category.Mathematics;
			}
			else if (category.Contains("Mythology"))
			{
				return Category.Mythology;
			}
			else if (category.Contains("Sports"))
			{
				return Category.Sports;
			}
			else if (category.Contains("Geography"))
			{
				return Category.Geography;
			}
			else if (category.Contains("History"))
			{
				return Category.History;
			}
			else if (category.Contains("Politics"))
			{
				return Category.Politics;
			}
			else if (category.Contains("Art"))
			{
				return Category.Art;
			}
			else if (category.Contains("Celebrities"))
			{
				return Category.Celebrities;
			}
			else if (category.Contains("Animals"))
			{
				return Category.Animals;
			}
			else if (category.Contains("Vehicles"))
			{
				return Category.Vehicles;
			}
			else if (category.Contains("Comics"))
			{
				return Category.Comics;
			}
			else if (category.Contains("Gadgets"))
			{
				return Category.Gadgets;
			}
			else if (category.Contains("Anime") || category.Contains("Manga"))
			{
				return Category.AnimeManga;
			}
			else if (category.Contains("Cartoon"))
			{
				return Category.Cartoon;
			}
			else
			{
				return Category.Any;
			}
		}

		/// <summary>
		///		Convert JSON reponse difficulty property to <see cref="Difficulty"/>.
		/// </summary>
		/// <param name="difficulty">
		///		Difficulty string from JSON response.
		/// </param>
		/// <returns>
		///		<see cref="Difficulty"/> based on parameter <paramref name="difficulty"/>.
		/// </returns>
		protected Difficulty DetermineDifficulty(string difficulty)
		{
			switch (difficulty)
			{
				case "hard":
					return Difficulty.Hard;
				case "medium":
					return Difficulty.Medium;
				case "easy":
					return Difficulty.Easy;
				default:
					return Difficulty.Any;
			}
		}

		/// <summary>
		///		Convert JSON reponse type property to <see cref="TriviaType"/>.
		/// </summary>
		/// <param name="type">
		///		Type string from JSON response.
		/// </param>
		/// <returns>
		///		<see cref="TriviaType"/> based on parameter <paramref name="type"/>.
		/// </returns>
		protected TriviaType DetermineType(string type)
		{
			switch (type)
			{
				case "multiple":
					return TriviaType.Multiple;
				case "boolean":
					return TriviaType.Boolean;
				default:
					return TriviaType.Any;
			}
		}

		/// <summary>
		///		Get response message based on response code.
		/// </summary>
		/// <param name="responseCode">
		///		Response code in the JSON response.
		/// </param>
		/// <returns>
		///		Message based on response code.
		/// </returns>
		protected string ResponseError(byte responseCode)
		{
			switch (responseCode)
			{
				case 0:
					return "Returned results successfully.";
				case 1:
					return "The API doesn't have enough questions for your query.";
				case 2:
					return "Invalid parameter(s). Arguments passed aren't valid.";
				case 3:
					return "Invalid session token.";
				case 4:
					return "Session token has retrieved all possible questions for the specified query. Reset the token.";
				default:
					return "An error has occured in the API";
			}
		}

		#endregion Protected Method

	}
}
