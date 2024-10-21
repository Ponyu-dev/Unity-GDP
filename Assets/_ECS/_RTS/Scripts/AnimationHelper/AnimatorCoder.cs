using _ECS._RTS.Scripts.AnimationHelper.Base;
using Cysharp.Threading.Tasks;

namespace _ECS._RTS.Scripts.AnimationHelper
{
    public class AnimatorCoder : BaseAnimatorCoder
    {
        public void Start()
        {
            Initialize();
        }
        
        public override void DefaultAnimation(int layer)
        {
            Play(new AnimationData(Animations.IDLE));
        }
    }
}