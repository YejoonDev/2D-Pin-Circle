using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PinSpawner : MonoBehaviour
{
    [Header("Commons")] 
    [SerializeField] private StageController stageController;
    [SerializeField] private GameObject pinPrefab;

    [Header("StuckPin")] 
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Vector3 targetPosition = Vector3.up * 2;
    [SerializeField] private float targetRadius = 0.8f;
    [SerializeField] private float pinLength = 1.5f;

    [Header("Throwable")] 
    private List<Pin> _throwablePins;
    private readonly float _bottomAngle = 270;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _throwablePins.Count > 0)
        {
            AttachStuckPin(_throwablePins[0].transform, _bottomAngle);
            _throwablePins.RemoveAt(0);
            
            foreach (Pin pin in _throwablePins)
            {
                pin.MoveOneStep(stageController.TPinDistance);
            }
            
        }
    }

    public void Setup()
    {
        _throwablePins = new List<Pin>();
    }
    
    public void SpawnThrowablePin(Vector3 position)
    {
        GameObject clone = Instantiate(pinPrefab, position, Quaternion.identity);
        _throwablePins.Add(clone.GetComponent<Pin>());
    }

    public void SpawnStuckPin(float angle)
    {
        GameObject clone = Instantiate(pinPrefab);
        AttachStuckPin(clone.transform, angle);
    }

    private void AttachStuckPin(Transform pin, float angle)
    {
        pin.position = Utils.PositionFromAngle(targetRadius + pinLength, angle) + targetPosition;
        pin.rotation = Quaternion.Euler(0, 0, angle);
        pin.SetParent(targetTransform);
        
        pin.GetComponent<Pin>().OnAttached();
    }
}
