using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RectTransformMover : MonoBehaviour
{
    private class EndMoveEvent : UnityEvent { }
    private EndMoveEvent _onEndMoveEvent;

    [SerializeField] private float moveTime = 1f;
    private RectTransform _rectTransform;
    private bool _isMoved = false;

    private void Awake()
    {
        _onEndMoveEvent = new EndMoveEvent();
        _rectTransform = GetComponent<RectTransform>();
    }

    public void MoveTo(UnityAction action, Vector3 position)
    {
        if (!_isMoved)
        {
            _onEndMoveEvent.AddListener(action);
            StartCoroutine(OnMove(action, position));
        }
    }

    IEnumerator OnMove(UnityAction action, Vector3 end)
    {
        float current = 0;
        float percent = 0;
        Vector3 start = _rectTransform.anchoredPosition;
        _isMoved = true;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / moveTime;
            _rectTransform.anchoredPosition = Vector3.Lerp(start, end, percent);
            yield return null;
        }

        _isMoved = true;
        _onEndMoveEvent.Invoke();
        _onEndMoveEvent.RemoveListener(action);
    }
}
