using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace GameEngine
{
    public class TestSave : MonoBehaviour
    {
        [Inject]
        private EntryPoint _entryPoint;

        [Button]
        public void SaveGameAsync()
        {
            _entryPoint.SaveGameAsync().Forget();
        }
        
        [Button]
        public void LoadGameAsync()
        {
            _entryPoint.LoadGameAsync().Forget();
        }
    }
}