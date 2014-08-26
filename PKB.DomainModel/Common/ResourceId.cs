using System;

namespace PKB.DomainModel.Common
{
    [Serializable]
    public struct ResourceId : IEquatable<ResourceId>
    {
        public static readonly ResourceId Empty = new ResourceId();

        private readonly Guid _value;

        private ResourceId(Guid value)
        {
            _value = value;
        }

        public ResourceId(string value)
            : this(new Guid(value))
        {
        }


        public static ResourceId NewId()
        {
            return new ResourceId(Guid.NewGuid());
        }

        public override bool Equals(object obj)
        {
            return obj is ResourceId && Equals((ResourceId)obj);
        }

        public bool Equals(ResourceId other)
        {
            return _value.Equals(other._value);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public static bool operator !=(ResourceId a, ResourceId b)
        {
            return a._value != b._value;
        }

        public static bool operator ==(ResourceId a, ResourceId b)
        {
            return a._value == b._value;
        }
    }
}
