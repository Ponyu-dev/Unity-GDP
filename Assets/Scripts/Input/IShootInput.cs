using System;

namespace ShootEmUp
{
    public interface IShootInput
    {
        event Action OnShoot;
    }
}