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
		///		Convert reponse category to <see cref="Category"/>.
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

		#endregion Private Method

	}
}
