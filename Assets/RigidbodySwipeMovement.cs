using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Lean.Touch;
using UnityEngine;

public class RigidbodySwipeMovement : MonoBehaviour
{
    [SerializeField] private PlayerAnimator playerAnimator;
    private Rigidbody2D rigidBody;
    public float speed = 10f;

    private Vector2 swipeStartPos;
    private bool isSwiping;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        LeanTouch.OnFingerDown += OnFingerDown;
        LeanTouch.OnFingerUpdate += OnFingerUpdate;
        LeanTouch.OnFingerUp += OnFingerUp;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerDown -= OnFingerDown;
        LeanTouch.OnFingerUpdate -= OnFingerUpdate;
        LeanTouch.OnFingerUp -= OnFingerUp;
    }

    private void OnFingerDown(LeanFinger finger)
    {
        swipeStartPos = finger.ScreenPosition;
        isSwiping = true;
    }
    
    private void OnFingerUpdate(LeanFinger finger)
    {
        if (isSwiping)
        {
            var swipeDelta = finger.ScreenPosition - swipeStartPos;
            var targetDirection = swipeDelta.normalized;
            playerAnimator.OnFingerSwipe(targetDirection);
            rigidBody.velocity = targetDirection * speed;
        }
    }

    private void OnFingerUp(LeanFinger finger)
    {
        isSwiping = false;
        rigidBody.velocity = Vector2.zero;
    }
}