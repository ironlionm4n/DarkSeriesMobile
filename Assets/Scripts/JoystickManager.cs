using System;
using System.Collections.Generic;
using UnityEngine;

public class JoystickManager : MonoBehaviour
{
    [SerializeField] private FixedJoystick fixedJoystick;

    public static event Action<Vector2> DetectedMovement;
    public static event Action DetectedMovementEnd;

    private void Update()
    {
        DetectJoystickMoved();
    }

    private void DetectJoystickMoved()
    {
        if (fixedJoystick.Direction != Vector2.zero)
        {
            DetectedMovement?.Invoke(fixedJoystick.Direction);
        }
        else
        {
            DetectedMovementEnd?.Invoke();
        }
    }
}
