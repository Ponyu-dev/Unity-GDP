using UnityEngine;

namespace ShootEmUp
{
    public interface ICharacter
    {
        Transform GetTransform();
    }
    
    public class Character : MonoBehaviour,
        ICharacter,
        ITimerGameListener
    {
        [SerializeField] public MoveData moveData;
        [SerializeField] public HitPointsData hitPointsData;
        [SerializeField] public TeamData teamData;
        [SerializeField] public WeaponData weaponData;

        public void OnStartTimer()
        {
            Debug.Log("[Character] OnStartTimer");
            transform.position = moveData.DefaultPosition;
        }

        public Transform GetTransform()
        {
            return gameObject.transform;
        }
    }
}