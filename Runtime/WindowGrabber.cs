using UnityEngine;
using UnityEngine.EventSystems;



namespace SatorImaging.AppWindowUtility
{
    public class WindowGrabber : MonoBehaviour
    {
        public enum MouseButton
        {
            Left = 0,
            Right = 1,
            Middle = 2,
        }
        public MouseButton mouseButton;
        public KeyCode modifierKey = KeyCode.None;
        public KeyCode[] temporarilyDisableIfKeyPressed;


        private bool isDragging = false;
        Vector2 targetPosition = Vector2.zero;


        void Update()
        {
#if UNITY_EDITOR
            if(isDragging.Equals(isDragging)) return; // to avoid CS0162 warning
#endif

            // do nothing if any uGUI is in use.
            if (EventSystem.current?.currentSelectedGameObject) return;


            // initialize dragging state. don't check modifier key.
            if (Input.GetMouseButtonUp((int)mouseButton)) isDragging = false;

            // key check.
            foreach (var k in temporarilyDisableIfKeyPressed) if (Input.GetKey(k)) return;
            if (modifierKey != KeyCode.None && !Input.GetKey(modifierKey)) return;



            if (Input.GetMouseButtonDown((int)mouseButton))
            {
                targetPosition = Event.current.mousePosition;
                isDragging = true;
            }

            if (isDragging && Input.GetMouseButton((int)mouseButton))
            {
                // do NOT use Event.current.delta. it's sampled in local window coordinate.
                // and moving window while mouse dragging changes coordinate sample by sample.
                // just remove the gap between current mouse position and drag starting position.
                AppWindowUtility.MoveWindowRelative(
                    (int)(Event.current.mousePosition.x - targetPosition.x),
                    (int)(Event.current.mousePosition.y - targetPosition.y)
                );

            }

        }//


    }//class
}//namespace
