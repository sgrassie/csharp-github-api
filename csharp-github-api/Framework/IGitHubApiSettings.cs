namespace csharp_github_api.Framework
{
    /// <summary>
    /// An interface for classes which are settings objects for the API.
    /// </summary>
    public interface IGitHubApiSettings
    {
        /// <summary>
        /// Gets or sets the username for Github.
        /// </summary>
        string Username { get; set; }

        /// <summary>
        /// Gets or sets the password for Github. If <see cref="Token"/> is set, this setting is ignoredl
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Gets or sets the users Github API token. 
        /// </summary>
        string Token { get; set; }

        /// <summary>
        /// Gets or sets the base url to use for Github. Only use the Github JSON API.
        /// </summary>
        string BaseUrl { get; set; }
    }
}