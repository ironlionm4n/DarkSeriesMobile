using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSpriteRender;
    [SerializeField] private SpriteRenderer gunSpriteRenderer;
    private Animator _animator;
    private Vector2 _lastDirection;
    
    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnFingerSwipe(Vector2 obj)
    {
        _lastDirection = obj;
        _animator.SetFloat(MoveX, obj.x);
        _animator.SetFloat(MoveY, obj.y);
    }

    public void OnFingerUp()
    {
        Debug.Log(_lastDirection);
        if (_lastDirection.x > 0)
        {
            playerSpriteRender.flipX = true;
            gunSpriteRenderer.flipX = false;
        }
        else
        {
            playerSpriteRender.flipX = false;
            gunSpriteRenderer.flipX = true;
        }
    }
}
