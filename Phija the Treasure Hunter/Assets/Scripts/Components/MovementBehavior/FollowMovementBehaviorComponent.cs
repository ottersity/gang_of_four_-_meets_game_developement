using System;
using UnityEngine;

namespace Components.MovementBehavior
{
    /// <summary>
    /// Follows the given game object.
    /// </summary>
    public class FollowMovementBehaviorComponent : MovementBehaviorComponent
    {

        public FollowMovementBehaviorComponent(GameObject followTarget)
        {
            FollowTarget = followTarget;
        }

        [field: SerializeField] public GameObject FollowTarget { get; set; }
        
        private void Update()
        {
            if (FollowTarget == null) return;
            SendMessage("MoveTo", (Vector2) FollowTarget.transform.position);
        }
    }
}