using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoldShoot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private static bool _isHeldDown;
    public static bool IsHeldDown => _isHeldDown;

    public void OnPointerDown(PointerEventData eventData)
    {
        _isHeldDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isHeldDown = false;
    }
}
