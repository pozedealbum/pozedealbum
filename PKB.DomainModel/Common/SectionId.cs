using System;

namespace PKB.DomainModel.Common
{
    [Serializable]
    public struct SectionId : IEquatable<SectionId>
    {
        private readonly Guid _value;

        private SectionId(Guid value)
        {
            _value = value;
        }

        public SectionId(string value)
            : this(new Guid(value))
        {
        }

        public static SectionId NewId()
        {
            return new SectionId(Guid.NewGuid());
        }

        public override bool Equals(object obj)
        {
            return obj is SectionId && Equals((SectionId)obj);
        }

        public bool Equals(SectionId other)
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

        public static bool operator !=(SectionId a, SectionId b)
        {
            return a._value != b._value;
        }

        public static bool operator ==(SectionId a, SectionId b)
        {
            return a._value == b._value;
        }
    }
}
