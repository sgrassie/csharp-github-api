using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csharp_github_api.Core
{
    public interface IHeader
    {
        string Name { get; set; }
        string Value { get; set; }
    }

    public class Header : IHeader, IEquatable<Header>
    {
        public Header(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public bool Equals(Header other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && string.Equals(Value, other.Value);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0)*397) ^ (Value != null ? Value.GetHashCode() : 0);
            }
        }

        public static bool operator ==(Header left, Header right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Header left, Header right)
        {
            return !Equals(left, right);
        }

        public string Name { get; set; }
        public string Value { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as Header;
            return other != null && Equals(other);
        }
    }
}
