using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    
    private bool _isSwiping;

    private void Update()
    {
        if (_isSwiping && LeanTouch.Fingers.Count > 1)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        }
    }

    public void OnFingerSwipe()
    {
        _isSwiping = true;
    }
    
    public void OnFingerRelease()
    {
        _isSwiping = false;
    }
}
