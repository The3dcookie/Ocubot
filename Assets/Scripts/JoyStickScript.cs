using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStickScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    // Anything with "I" infront is called an interface
    [SerializeField] private float joystickVisualDistance = 100;

    private Image backgroundImage;
    private Image joystickImage;
    private Vector2 direction;
    private Vector3 Direction { get { return direction; } }


    // How to make a singleton
    private static JoyStickScript main;

    public static JoyStickScript Main
    {
        get
        {
            if (main == null)
            {
                main = FindObjectOfType<JoyStickScript>();
            }
            return main;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        backgroundImage = GetComponent<Image>();
        joystickImage = transform.GetChild(0).GetComponent<Image>();
    }

   

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = Vector2.zero;

        // using the rect transform utility, find out whether your screenpoint is inside your background image rect transform
        // position to find out the position using the event system
        // use the camera then add an out pos. this stores the position registered in the rect transform using the event system

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(backgroundImage.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            // to give youself a position within the rect transform of the joystickBackgroundImage also to know how far away you are from the anchor
            pos.x = (pos.x / backgroundImage.rectTransform.sizeDelta.x);
            pos.y = (pos.y / backgroundImage.rectTransform.sizeDelta.x);
            //Debug.Log("InputX " + pos.x.ToString() + "/" + "ImputY " + pos.y.ToString());

            //Anchor pivot point settings
            Vector2 p = backgroundImage.rectTransform.pivot;
            pos.x += p.x - 0.5f;
            pos.y += p.y - 0.5f;

            //Clamp your values so it doesnt go above or below a certain value
            float x = Mathf.Clamp(pos.x, -1, 1);
            float y = Mathf.Clamp(pos.y, -1, 1);

            direction = new Vector2(x,y).normalized;


            // Finally move the respective joystick pictures according to the code
            joystickImage.rectTransform.anchoredPosition = new Vector3(x * joystickVisualDistance, y * joystickVisualDistance);
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        direction = Vector3.zero;
        joystickImage.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float InputHorizontal()
    {
        if (direction.x != 0)
            return direction.x;
        else
            return Input.GetAxis("Horizontal");
       
    }

    public float InputVertical()
    {
        if (direction.y != 0)
            return direction.y;
        else
            return Input.GetAxis("Vertical");
    }
}
