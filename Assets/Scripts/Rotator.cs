using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private StageController stageController;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float maxRotateSpeed = 500f;
    [SerializeField] private Vector3 rotateAngle = Vector3.forward;
    
    void Update()
    {
        if (!stageController.IsGameStart)
            return;
        
        transform.Rotate(rotateAngle, rotateSpeed * Time.deltaTime);
    }

    public void Stop()
    {
        rotateSpeed = 0;
    }

    public void RotateFast()
    {
        rotateSpeed = maxRotateSpeed;
    }
}
