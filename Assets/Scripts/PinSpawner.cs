using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PinSpawner : MonoBehaviour
{
    [FormerlySerializedAs("stageManager")]
    [Header("Commons")] 
    [SerializeField] private StageController stageController;
    [SerializeField] private GameObject pinPrefab;
    [SerializeField] private GameObject pinIndexTextPrefab;
    [SerializeField] private Transform textParent;

    [Header("Stuck")] 
    [SerializeField] private Transform target;
    
    [Header("Throwable")] 
    private List<Pin> _throwablePins;
    private readonly float _bottomAngle = 270;

    private void Update()
    {
        if (!stageController.IsGameStart || stageController.IsGameOver)
            return;
        
        if (Input.GetMouseButtonDown(0) && _throwablePins.Count > 0)
        {
            _throwablePins[0].GetComponent<Pin>().AttachToTarget(target, _bottomAngle);
            _throwablePins.RemoveAt(0);
            
            foreach (Pin pin in _throwablePins)
            {
                pin.MoveOneStep(stageController.TPinDistance);
            }
            stageController.DecreaseTPin();
        }
    }

    public void Setup()
    {
        _throwablePins = new List<Pin>();
    }
    
    public void SpawnThrowablePin(Vector3 position, int index)
    {
        Pin pin = Instantiate(pinPrefab, position, Quaternion.identity).GetComponent<Pin>();
        pin.Setup(stageController);
        _throwablePins.Add(pin);
        SpawnTextUI(pin.transform, index);
        
    }

    public void SpawnStuckPin(float angle, int index)
    {
        Pin pin = Instantiate(pinPrefab).GetComponent<Pin>();
        pin.Setup(stageController);
        pin.AttachToTarget(target, angle);
        SpawnTextUI(pin.transform, index);
    }
    
    private void SpawnTextUI(Transform target, int index)
    {
        GameObject textClone = Instantiate(pinIndexTextPrefab);
        textClone.transform.SetParent(textParent);
        textClone.transform.localScale = Vector3.one;
        textClone.GetComponent<FollowTargetOnScreen>().SetUp(target);
        textClone.GetComponent<TextMeshProUGUI>().text = index.ToString();
    }
}
