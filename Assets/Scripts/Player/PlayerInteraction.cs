using UnityEngine;

public class PlayerInteraction : InteractionController
{
    [Header("Interaction")]
    [SerializeField] private LayerMask layerInteractable;
    [SerializeField] private float interactionDistance;

    [Header("UI")]
    [SerializeField] private GameObject interactButton;

    #region [PrivateVars]

    private Camera _camera;

    private Interactable currentInteractableObject;
    private Interactable CurrentInteractableObject
    {
        get => currentInteractableObject;
        set
        {
            currentInteractableObject = value;

            interactButton.SetActive(currentInteractableObject == null ? false : true);
        }
    }

    #endregion

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        CheckInteractableObject();
    }

    private void CheckInteractableObject()
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, layerInteractable))
        {
            if (hit.transform.TryGetComponent(out Interactable interactable))
            {
                CurrentInteractableObject = interactable;
            }
            else
            {
                CurrentInteractableObject = null;
            }
        }
        else
        {
            CurrentInteractableObject = null;
        }
    }

    public void Interact()
    {
        if (currentInteractableObject == null)
        {
            return;
        }

        currentInteractableObject.Interact();
    }
}
