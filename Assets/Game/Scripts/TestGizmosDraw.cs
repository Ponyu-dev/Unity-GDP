using UnityEngine;

namespace Game.Scripts
{
    public class TestGizmosDraw : MonoBehaviour
    {
        public float _attackRange = 2;
        
        public void OnDrawGizmos()
        {
            Debug.Log("OnDrawGizmos");
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }
    }
}