using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float timeBetweenShots;

    private bool _isSwiping;
    private Vector2 _targetDirection;
    private bool _isShooting;

    private void Update()
    {
        if (_isSwiping && LeanTouch.Fingers.Count > 1)
            if (!_isShooting)
                StartCoroutine(ShootBulletRoutine());
    }

    private IEnumerator ShootBulletRoutine()
    {
        _isShooting = true;
        var spawnedBullet = Instantiate(bulletPrefab,
            transform.position + new Vector3(_targetDirection.x / 2, _targetDirection.y / 2, 0), Quaternion.identity);
        var bullet = spawnedBullet.GetComponent<Bullet>();
        bullet.MoveDirection = _targetDirection;
        bullet.MoveSpeed = bulletSpeed;
        yield return new WaitForSeconds(timeBetweenShots);
        _isShooting = false;
    }

    public void OnFingerSwipe(Vector2 direction)
    {
        _isSwiping = true;
        _targetDirection = direction;
    }

    public void OnFingerRelease()
    {
        _isSwiping = false;
    }
}