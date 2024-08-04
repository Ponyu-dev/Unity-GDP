using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour
    {
        private float m_StartPositionY;
        private float m_EndPositionY;
        private float m_MovingSpeedY;
        private float m_PositionX;
        private float m_PositionZ;
        private Transform m_MyTransform;
        
        [SerializeField]
        private LevelBackgroundParams levelBackgroundParams;

        private void Awake()
        {
            this.m_StartPositionY = this.levelBackgroundParams.startPositionY;
            this.m_EndPositionY = this.levelBackgroundParams.endPositionY;
            this.m_MovingSpeedY = this.levelBackgroundParams.movingSpeedY;
            this.m_MyTransform = this.transform;
            var position = this.m_MyTransform.position;
            this.m_PositionX = position.x;
            this.m_PositionZ = position.z;
        }

        private void FixedUpdate()
        {
            if (this.m_MyTransform.position.y <= this.m_EndPositionY)
            {
                this.m_MyTransform.position = new Vector3(
                    this.m_PositionX,
                    this.m_StartPositionY,
                    this.m_PositionZ
                );
            }

            this.m_MyTransform.position -= new Vector3(
                this.m_PositionX,
                this.m_MovingSpeedY * Time.fixedDeltaTime,
                this.m_PositionZ
            );
        }
    }
}