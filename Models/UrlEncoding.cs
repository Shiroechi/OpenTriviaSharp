namespace OpenTriviaSharp.Models
{
	/// <summary>
	/// Encoding of JSON response.
	/// </summary>
	public enum UrlEncoding
	{
		/// <summary>
		/// All values will be encoded in HTML encoding.
		/// </summary>
		Default,
		/// <summary>
		/// All values will be enoded in Base64.
		/// </summary>
		Base64,
		/// <summary>
		/// All values will be encoded in URL encoding (RFC 3986).
		/// </summary>
		Url3986
	}
}
