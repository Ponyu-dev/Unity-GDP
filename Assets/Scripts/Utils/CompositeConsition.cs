using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public class CompositeConsition
    {
        private readonly HashSet<Func<bool>> _conditions = new();

        public void Append(Func<bool> cond) => this._conditions.Add(cond);

        public bool IsAllTrue()
        {
            return _conditions.All(condition => condition.Invoke());
        }
        
        public bool IsAllFalse()
        {
            return !IsAllTrue();
        }
    }
}