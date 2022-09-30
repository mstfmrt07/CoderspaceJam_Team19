using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonScaler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Button Button;
    public float pressedScale = 0.9f;
    public float normalScale = 1f;

    private void Awake()
    {
        Button = GetComponent<Button>();   
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Button.transform.localScale = Vector3.one * pressedScale;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Button.transform.localScale = Vector3.one * normalScale;
    }
}
