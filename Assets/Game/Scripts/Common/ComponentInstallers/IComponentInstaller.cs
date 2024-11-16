using System;
using Atomic.Entities;

namespace Game.Scripts.Common.ComponentInstallers
{
    public interface IComponentInstaller
    {
        void Install(IEntity entity);
    }

    public interface IAddExpression
    {
        void Append(Func<bool> func);
    }
}