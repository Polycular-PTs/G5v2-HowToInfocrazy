using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClickDetect : MonoBehaviour, IPointerClickHandler
{
    public bool clicked = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        clicked = true;
        Debug.Log("A button click was detected");
    }

    public void ResetClicked()
    {
        clicked = false;
    }
}
