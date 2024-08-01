using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetOnScreen : MonoBehaviour
{
    private RectTransform _rectTransform;
    private Transform _targetTransform; // 따라다닐 타겟

    public void SetUp(Transform targetTransform)
    {
        _rectTransform = GetComponent<RectTransform>();
        _targetTransform = targetTransform;
    }

    private void FixedUpdate()
    {
        if (_targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(_targetTransform.position);
        _rectTransform.position = screenPoint;
    }
}


