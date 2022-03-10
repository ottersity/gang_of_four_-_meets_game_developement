using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Components.MovementBehavior
{
    /// <summary>
    /// Let the attach game object move to random positions.
    /// </summary>
    public class RandomMovementBehaviorComponent : MovementBehaviorComponent
    {

        private static readonly Vector2[] Directions = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

        void FixedUpdate()
        {
            SendMessage(
                "MoveTo",
                (Vector2)transform.position + Directions[Random.Range(0, Directions.Length)]);
        }

    }
}