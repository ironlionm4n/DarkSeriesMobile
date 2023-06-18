using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnFingerSwipe(Vector2 obj)
    {
        _animator.SetFloat(MoveX, obj.x);
        _animator.SetFloat(MoveY, obj.y);
    }
}
