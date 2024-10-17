using TMPro;
using UnityEngine;

namespace _ECS._RTS.Scripts.UI.Views
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private TextMeshPro healthText;
        [SerializeField] private SpriteRenderer healthBarFill;

        public void UpdateHealth(string health)
        {
            healthText.text = health;
        }

        public void UpdateHealthBarFill(Vector2 size)
        {
            healthBarFill.size = size;
        }
    }
}