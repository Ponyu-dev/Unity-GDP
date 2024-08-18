using UnityEngine;

namespace ShootEmUp
{
    public class Character : MonoBehaviour,
        IGameTimerListener
    {
        [SerializeField] public MoveData moveData;
        [SerializeField] public HitPointsData hitPointsData;
        [SerializeField] public TeamData teamData;
        [SerializeField] public WeaponData weaponData;

        public void OnStartTimer()
        {
            transform.position = moveData.DefaultPosition;
        }
    }
}