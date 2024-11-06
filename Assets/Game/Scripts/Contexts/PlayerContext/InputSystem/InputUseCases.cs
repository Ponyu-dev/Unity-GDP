using Unity.Mathematics;
using UnityEngine;

namespace Game.Scripts.Contexts.PlayerContext.InputSystem
{
    public static class InputUseCases
    {
        public static float3 GetMoveDirection(this InputMap inputMap)
        {
            var moveDirection = float3.zero;

            if (Input.GetKey(inputMap.left))
            {
                moveDirection.x = -1;
            }
            else if (Input.GetKey(inputMap.right))
            {
                moveDirection.x = 1;
            }

            if (Input.GetKey(inputMap.back))
            {
                moveDirection.z = -1;
            }
            else if (Input.GetKey(inputMap.forward))
            {
                moveDirection.z = 1;
            }

            return moveDirection;
        }
    }
}