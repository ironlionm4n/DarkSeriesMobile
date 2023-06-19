using Lean.Touch;
using UnityEngine;

public class RigidbodySwipeMovement : MonoBehaviour
{
    [SerializeField] private PlayerAnimator playerAnimator;
    private Rigidbody2D _rigidBody;
    private Gun _gun;
    public float speed = 10f;

    private Vector2 swipeStartPos;
    private bool isSwiping;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _gun = GetComponent<Gun>();
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
            _gun.OnFingerSwipe(targetDirection);
            playerAnimator.OnFingerSwipe(targetDirection);
            _rigidBody.velocity = targetDirection * speed;
        }
    }

    private void OnFingerUp(LeanFinger finger)
    {
        isSwiping = false;
        _gun.OnFingerRelease();
        _rigidBody.velocity = Vector2.zero;
        playerAnimator.OnFingerUp();
        playerAnimator.OnFingerSwipe(Vector2.zero);
    }
}