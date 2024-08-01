using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public GameObject square;
    private readonly float _moveTime = 0.2f;
    
    [SerializeField] private float pinLength = 1.5f;
    private StageController _stageController;

    public void Setup(StageController stageController)
    {
        _stageController = stageController;
    }
    
    
    public void AttachToTarget(Transform target, float angle)
    {
        float targetRadius = target.localScale.x / 2;
        transform.position = Utils.PositionFromAngle(targetRadius + pinLength, angle)
                             + target.position;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.SetParent(target);
        StopCoroutine("MoveTo");
        square.SetActive(true);
    }
    
    
    public void MoveOneStep(float moveDistance)
    {
        StartCoroutine("MoveTo", moveDistance);
    }
    
    private IEnumerator MoveTo(float moveDistance)
    {
        Vector3 startPoint = transform.position;
        Vector3 endPoint = transform.position + (Vector3.up * moveDistance);
        float current = 0;
        float percent = 0;
        
        while (1 > percent)
        {
            current += Time.deltaTime;
            percent = current / _moveTime;
            transform.position = Vector3.Lerp(startPoint, endPoint, percent);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pin"))
        {
            Debug.Log("Game Over");
            _stageController.GameOver();
        }
    }
}
