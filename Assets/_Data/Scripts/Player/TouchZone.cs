using UnityEngine;
using UnityEngine.EventSystems;

public class TouchZone : MonoBehaviour, IPointerMoveHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private PlayerCamera playerCamera;

    private bool isTouched;

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouched = true;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            Vector2 touchPosition = new Vector2(Input.GetTouch(0).deltaPosition.x, Input.GetTouch(0).deltaPosition.y);
            playerCamera.SetRotation(touchPosition);
        }      
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouched = false;
    }
}
