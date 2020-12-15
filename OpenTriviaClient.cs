using System.Net.Http;

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

	}
}
