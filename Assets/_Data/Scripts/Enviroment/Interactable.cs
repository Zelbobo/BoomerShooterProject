using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private UnityEvent onInteract;

    public virtual void Interact(InteractionController interactor)
    {
        Debug.Log(transform.name);

        onInteract?.Invoke();
    }
}
