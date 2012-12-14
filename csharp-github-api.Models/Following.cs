namespace csharp_github_api.Models
{
    public class Following
    {
        public virtual string Login { get; set; }
        public virtual string Id { get; set; }
        public virtual string GravatarUrl { get; set; }
        public virtual string Url { get; set; }
    }
}