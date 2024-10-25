using UnityEngine;

namespace _ECS_RTS.Scripts.UI
{
    public class RangeTargetFinder : MonoBehaviour
    {
        [SerializeField] private Color defaultColor = Color.white;
        [SerializeField] private Color foundColor = Color.red;
        [SerializeField] private float detectionRadius = 10f;
        [SerializeField] private LayerMask targetLayer;

        private bool _isFound;

        private void OnDrawGizmos()
        {
            Gizmos.color = _isFound ? foundColor : defaultColor;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }

        private void Update()
        {
            _isFound = Physics.OverlapSphere(transform.position, detectionRadius, targetLayer).Length > 0;
        }
    }
}