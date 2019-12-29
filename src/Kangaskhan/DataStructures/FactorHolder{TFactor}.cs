using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace Kangaskhan.DataStructures
{
    public class FactorHolder<TFactor> where TFactor : Factor
    {
        public readonly TFactor[] Factors;

        public FactorHolder(IEnumerable<TFactor> factors)
        {
            this.Factors = (from i in factors select (TFactor)i.Clone()).ToArray();
        }

        public T GetFactor<T>() where T : TFactor
        {
            //TODO: Check if this need acceleration
            for (int i = 0; i < this.Factors.Length; i++)
            {
                var rawFactor = this.Factors[i];
                var castedFactor = rawFactor as T;
                if (castedFactor != null)
                {
                    return castedFactor;
                }
            }
            return null;
        }
    }
}