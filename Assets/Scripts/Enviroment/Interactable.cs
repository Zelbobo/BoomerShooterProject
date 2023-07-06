using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private UnityAction onInteract;

    public virtual void Interact()
    {
        Debug.Log(transform.name);

        onInteract?.Invoke():
    }
}
