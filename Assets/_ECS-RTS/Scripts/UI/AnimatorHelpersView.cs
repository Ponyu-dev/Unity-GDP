using _ECS_RTS.Scripts.EcsEngine;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace _ECS_RTS.Scripts.UI
{
    public class AnimatorHelpersView : MonoBehaviour
    {
        [Inject] private readonly IEcsStartup _ecsStartup;

        [Button]
        private void Walk()
        {
            //_ecsStartup?.CreateEntity(EcsWorlds.EVENTS);
        }
    }
}