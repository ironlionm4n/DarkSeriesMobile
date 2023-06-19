using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSpriteRender;
    [SerializeField] private SpriteRenderer gunSpriteRenderer;
    
    private Animator _animator;
    private bool shouldFaceRight;
    
    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");
    private static readonly int ShouldFaceRight = Animator.StringToHash("shouldFaceRight");
    private static readonly int IsWalking = Animator.StringToHash("isWalking");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        JoystickManager.DetectedMovementEnd += OnJoystickEnd;
    }

    private void OnDisable()
    {
        JoystickManager.DetectedMovementEnd -= OnJoystickEnd;
    }

    public void OnJoystickMove(Vector2 targetDirection)
    {
        if (targetDirection.x < 0)
        {
            shouldFaceRight = false;
        }
        else
        {
            shouldFaceRight = true;
        }
        
        _animator.SetFloat(MoveX, targetDirection.x);
        _animator.SetFloat(MoveY, targetDirection.y);
        _animator.SetBool(IsWalking, true);
    }

    private void OnJoystickEnd()
    {
        _animator.SetFloat(MoveX, 0f);
        _animator.SetFloat(MoveY, 0f);
        _animator.SetBool(ShouldFaceRight, shouldFaceRight);
        _animator.SetBool(IsWalking, false);
        gunSpriteRenderer.flipX = !shouldFaceRight;
        playerSpriteRender.flipX = shouldFaceRight;
    }
    
}
