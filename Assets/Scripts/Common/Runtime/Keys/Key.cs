using System;

namespace Modules.Common.Structures
{
    [Serializable]
    public struct Key : IEquatable<Key>
    {
        public int id;
        public int value;

        public Key(int id, int value)
        {
            this.id = id;
            this.value = value;
        }

        public bool Equals(Key other) => id == other.id && value == other.value;
        
        public override bool Equals(object obj) => obj is Key other && Equals(other);
        
        public override int GetHashCode() => HashCode.Combine(id, value);
        
        public static bool operator ==(Key self, Key other) => self.Equals(other);
        
        public static bool operator !=(Key self, Key other) => !(self == other);
    }
}
