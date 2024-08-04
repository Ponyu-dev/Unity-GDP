using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public class CompositeCondition
    {
        private readonly HashSet<Func<bool>> m_Conditions = new();

        public void Append(Func<bool> cond) => this.m_Conditions.Add(cond);

        public bool IsAllTrue()
        {
            return m_Conditions.All(condition => condition.Invoke());
        }
        
        public bool IsAllFalse()
        {
            return !IsAllTrue();
        }
    }
}