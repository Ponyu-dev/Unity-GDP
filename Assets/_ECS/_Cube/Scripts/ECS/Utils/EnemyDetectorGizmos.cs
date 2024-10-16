using UnityEngine;

namespace CubeECS.Scripts.ECS.Utils
{
    public class EnemyDetectorGizmos : MonoBehaviour
    {
        public float detectionRadius = 5f;  // Радиус сферы обнаружения
        public Color gizmoColor = Color.red; // Цвет сферы
        public LayerMask enemyLayer; // Слой врагов для фильтрации
        private bool _isEnemyDetected;

        void Update()
        {
            // Проверяем, есть ли враги в радиусе обнаружения
            _isEnemyDetected = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer).Length > 0;
            //_isEnemyDetected = Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, null, enemyLayer) > 0;
        }
        
        // Метод для отрисовки Gizmos в редакторе
        private void OnDrawGizmos()
        {
            // Меняем цвет гизмо в зависимости от обнаружения врагов
            Gizmos.color = _isEnemyDetected ? gizmoColor : Color.white;
            
            Gizmos.DrawWireSphere(transform.position, detectionRadius); // Отрисовка сферы с радиусом
        }
    }
}