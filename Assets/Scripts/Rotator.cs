using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Vector3 rotateAngle = Vector3.forward;
    
    void Update()
    {
        transform.Rotate(rotateAngle, rotateSpeed * Time.deltaTime);
    }
}
