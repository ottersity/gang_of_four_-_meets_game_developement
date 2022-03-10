using UnityEngine;

namespace Components.MovementBehavior
{
    /// <summary>
    /// Moves the attached game object to the mouse clicked position. 
    /// </summary>
    public class ClickMovementBehaviorComponent : MovementBehaviorComponent
    {

        /// <summary>
        /// Moves the game object towards the position of targetPosition.
        /// </summary>
        private void Update()
        {
            HandleMouseClick();
        }

        /// <summary>
        ///  Executes the actions needed if the left mouse button is pressed.
        /// E.g. moving the game object towards the clicked tile.
        /// </summary>
        private void HandleMouseClick()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            Debug.Assert(Camera.main != null, "Camera.main != null");
            SendMessage("MoveTo", (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}