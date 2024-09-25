using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class MoveData
    {
        [SerializeField] private Vector3 defaultPosition;
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private float speed;

        public Vector3 DefaultPosition => defaultPosition;
        public Rigidbody2D GetRigidbody2D() => rigidbody2D;
        public Vector2 CurrentPosition => rigidbody2D.position;
        public float Speed => speed;
    }
}