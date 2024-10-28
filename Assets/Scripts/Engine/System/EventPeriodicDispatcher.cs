using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Engine
{
    public abstract class EventReceiver : MonoBehaviour
    {
        public abstract void OnEventTriggered();
    }
    
    public class EventPeriodicDispatcher : MonoBehaviour
    {
        [SerializeField] private EventReceiver eventReceiver;
        [SerializeField] private bool isPeriodic;
        
        private void Start()
        {
            DispatchEventsPeriodically().Forget();
        }

        private async UniTaskVoid DispatchEventsPeriodically()
        {
            while (isPeriodic)
            {
                eventReceiver.OnEventTriggered();
                var delayTime = Random.Range(1000, 5001);
                await UniTask.Delay(delayTime);
            }
        }
        
        private void OnDestroy()
        {
            isPeriodic = false;
        }
    }
}