using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour , IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image joystickBackgroundImage;
    private Image joystickImage;

    public Vector3 InputDirection{set; get;}
    public void Start()
    {
        joystickBackgroundImage = GetComponent<Image>();
        joystickImage = transform.GetChild(0).GetComponent<Image>();
        InputDirection = Vector3.zero;
    }
    public virtual void OnDrag(PointerEventData ped)
    {
        //Debug.Log("OnDrag");
        Vector2 position = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackgroundImage.rectTransform,
            ped.position,ped.pressEventCamera,out position))
            {
                position.x = (position.x / joystickBackgroundImage.rectTransform.sizeDelta.x);
                position.y = (position.y / joystickBackgroundImage.rectTransform.sizeDelta.y);

                float x = (joystickBackgroundImage.rectTransform.pivot.x == 1) ? position.x * 2 + 1 : position.x * 2 - 1;
                float y = (joystickBackgroundImage.rectTransform.pivot.y == 1) ? position.y * 2 + 1 : position.y * 2 - 1;

                InputDirection = new Vector3(x,0,y);
                InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

                joystickImage.rectTransform.anchoredPosition = new Vector3(InputDirection.x * (joystickBackgroundImage.rectTransform.sizeDelta.x / 3)
                , InputDirection.z * (joystickBackgroundImage.rectTransform.sizeDelta.y / 3));
                //Debug.Log(InputDirection );
            }
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        //Debug.Log("OnPointerDown");
        OnDrag (ped);
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        //Debug.Log("OnPointerUp");
        InputDirection = Vector3.zero;
        joystickImage.rectTransform.anchoredPosition = Vector3.zero;
    }
}
