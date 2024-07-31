using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public GameObject square;
    private readonly float _moveTime = 0.2f;

    public void OnAttached()
    {
        StopCoroutine("MoveTo");
        square.SetActive(true);
    }
    
    public void MoveOneStep(float moveDistance)
    {
        StartCoroutine("MoveTo", moveDistance);
    }
    
    private IEnumerator MoveTo(float moveDistance)
    {
        Debug.Log(moveDistance);
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
}
