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
        JoystickManager.DetectedMovement += HandleDetectedMovement;
        JoystickManager.DetectedMovementEnd += OnJoystickEnd;
    }

    private void OnDisable()
    {
        JoystickManager.DetectedMovement -= HandleDetectedMovement;
        JoystickManager.DetectedMovementEnd -= OnJoystickEnd;
    }

    private void HandleDetectedMovement(Vector2 targetDirection)
    {
        playerAnimator.OnJoystickMove(targetDirection);
        _rigidBody.velocity = targetDirection * speed;
    }

    private void OnJoystickEnd()
    {
        _rigidBody.velocity = Vector2.zero;
    }
}