using UnityEngine;

public class Ladder : Interactable
{
    public override void Interact(InteractionController interactor)
    {
        if (interactor.TryGetComponent(out MoveController controller))
        {
            controller.SetClimbing(true);
        }
    }
}
