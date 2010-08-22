
namespace csharp_github_api.Models
{
    /// <summary>
    /// Maps to an authenticated users plan
    /// </summary>
    public class Plan
    {
        /// <summary>
        /// Gets or sets the name of the GitHub plan.
        /// </summary>
        /// <remarks>
        /// One of: Free, Micro, Small, Medium
        /// </remarks>
        public virtual string Name { get; set;}

        /// <summary>
        /// Gets or sets the number of collaborators in the private plan
        /// </summary>
        public virtual int Collaborators { get; set;}

        /// <summary>
        /// Gets or sets the amount of disk space in the private plan.
        /// </summary>
        public virtual long Space { get; set;}

        /// <summary>
        /// Gets or sets the number of private repositories the user has.
        /// </summary>
        public virtual int PrivateRepos { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Plan)
            {
                var compareTo = (Plan) obj;

                if (compareTo.Equals(Name) && compareTo.Collaborators.Equals(Collaborators)
                    && compareTo.Space.Equals(Space) && compareTo.PrivateRepos.Equals(PrivateRepos))
                {
                    return true;
                }

                return false;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + Collaborators.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
