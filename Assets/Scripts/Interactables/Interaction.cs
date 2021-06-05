
using UnityEngine;
using UnityEngine.Events;

using KOS.Interfaces;

namespace KOS.Interactables
{
    public class Interaction : MonoBehaviour, IInteractable
    {
        [SerializeField] protected string promptText;
        public string interactionPrompt => promptText;

        [Space(12)] public UnityEvent onInteraction;

        public void InteractWith()
        {
            onInteraction?.Invoke();
            // maybe do other things...
        }
    }
}
