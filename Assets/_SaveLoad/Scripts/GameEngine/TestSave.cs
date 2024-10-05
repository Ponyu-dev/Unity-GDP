using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace GameEngine
{
    public class TestSave : MonoBehaviour
    {
        [Inject]
        private SaveLoadPoint _saveLoadPoint;

        [Button]
        public void SaveGameAsync()
        {
            _saveLoadPoint.SaveGameAsync().Forget();
        }
        
        [Button]
        public void LoadGameAsync()
        {
            _saveLoadPoint.LoadGameAsync().Forget();
        }
    }
}