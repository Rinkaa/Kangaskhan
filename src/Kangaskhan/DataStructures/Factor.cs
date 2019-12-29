using System;

namespace Kangaskhan.DataStructures
{
    public abstract class Factor : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}