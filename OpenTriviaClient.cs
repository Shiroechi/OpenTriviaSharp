using System.Net.Http;
using System.Net.Http.Headers;

using OpenTriviaSharp.Models;

namespace OpenTriviaSharp
{
	/// <summary>
	/// Open Trivia Database client.
	/// </summary>
	public class OpenTriviaClient
	{
		#region Member

		private HttpClient _Client;
		private bool _Supplied;
		private string _BaseApiUrl = "https://opentdb.com/api.php?";
		private string _BaseTokenApiUrl = "https://opentdb.com/api.php?";
		private string _Token;

		#endregion Member

		#region Constructor & Destructor

		/// <summary>
		/// 
		/// </summary>
		/// <param name="client">
		///		<see cref="HttpClient"/> object to send and receiving response.
		/// </param>
		/// <param name="token">
		///		Session token of Open Trivia for tracking requested question.
		/// </param>
		public OpenTriviaClient(HttpClient client = null, string token = null)
		{
			if (client == null)
			{
				this._Client = new HttpClient();
				this._Supplied = false;
			}
			else
			{
				this._Client = client;
				this._Supplied = true;
			}

			if(token == null || token.Trim() == "")
			{
				token = "";
			}
			else
			{
				this.Token = token;
			}
		}

		/// <summary>
		/// Release all resource that this object handle.
		/// </summary>
		~OpenTriviaClient()
		{
			if (this._Supplied == false
			{
				this._Client.Dispose();
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

				this._Token = value;
			}
			get
			{
				return this._Token;
			}
		}

		#endregion Properties

		#region Private Method

		/// <summary>
		/// Add browser user agent to <see cref="HttpClient"/>.
		/// </summary>
		private void AddUserAgent()
		{
			if (this._Client == null)
			{
				return;
			}

			if (this._Client.DefaultRequestHeaders.UserAgent.Count == 0)
			{
				this._Client.DefaultRequestHeaders.Add(
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
		private Category DetermineCategory(string category)
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
		private Difficulty DetermineDifficulty(string difficulty)
		{
			switch(difficulty)
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
		private TriviaType DetermineType(string type)
		{
			switch(type)
			{
				case "multiple":
					return TriviaType.MultipleChoice;
				case "boolean":
					return TriviaType.TrueFalse;
				default:
					return TriviaType.Any;
			}
		}

		#endregion Private Method

	}
}
