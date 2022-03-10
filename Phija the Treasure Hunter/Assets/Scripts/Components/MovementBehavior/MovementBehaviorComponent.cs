using UnityEngine;

namespace Components.MovementBehavior
{
    /// <summary>
    /// Movements behaviors are describing the movement behavior of the object.
    /// They decide to which position the object should move next.
    /// Used as base class for all movement behavior components to make sure that only one component of this type  
    /// is attached to the game object at the same time. 
    /// </summary>
    public abstract class MovementBehaviorComponent : MonoBehaviour { }
}