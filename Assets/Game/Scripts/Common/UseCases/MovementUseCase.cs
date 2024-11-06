using Unity.Burst;
using Unity.Mathematics;

namespace Game.Scripts.Common.UseCases
{
    [BurstCompile]
    public static class MovementUseCase
    {
        [BurstCompile]
        public static void MoveStep(
            in float3 position,
            in float3 direction,
            in float speed,
            in float deltaTime,
            out float3 newPosition
        )
        {
            newPosition = position + speed * deltaTime * direction;
        }
    }
}