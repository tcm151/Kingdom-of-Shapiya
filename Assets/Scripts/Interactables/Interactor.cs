
using UnityEngine;

using KOS.Events;
using KOS.Interactables;

namespace KOS.Interactables
{
    public class Interactor : MonoBehaviour
    {
        public float interactionDistance = 2.5f;

        //> CONNECT TO CAMERA
        private Camera view;
        private void Awake() => view = Camera.main;

        private void Update()
        {
            if (Cursor.lockState != CursorLockMode.Locked) return;

            Ray inputRay = view.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(inputRay, out RaycastHit hit, interactionDistance))
            {
                var interactable = hit.collider.GetComponentInParent<IInteractable>();
                if (interactable == null) return;
                
                EventManager.Active.ShowPopup(true, interactable.interactionPrompt);
                if (Input.GetKeyDown(KeyCode.E)) interactable.InteractWith();
            }
            else EventManager.Active.ShowPopup(false, "you shouldn't be seeing this");
        }
    }
}
