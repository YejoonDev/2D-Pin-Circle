using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    
    [SerializeField] private PinSpawner pinSpawner;
    [SerializeField] private int throwablePinCount;
    [SerializeField] private int stuckPinCount;
    [SerializeField] private Rotator rotator;
    
    private readonly Color _failBackgroundColor = new Color(0.4f, 0.1f, 0.1f); 
    public bool IsGameOver { get; set; }
    
    private readonly Vector3 _firstTPinPosition = Vector3.down * 2;
    public float TPinDistance { get; private set; } = 1;

    private void Awake()
    {
        
        pinSpawner.Setup();
        
        // Throwable Pin 생성
        for (int i = 0; i < throwablePinCount; i++)
        {
            pinSpawner.SpawnThrowablePin(_firstTPinPosition + Vector3.down * TPinDistance * i,
                throwablePinCount - i);
        }
        
        // Stuck Pin 생성
        for (int i = 0; i < stuckPinCount; i++)
        {
            float angle = (360f / stuckPinCount) * i;
            pinSpawner.SpawnStuckPin(angle,
                throwablePinCount + i);
        }
    }

    public void GameOver()
    {
        IsGameOver = true;
        Camera.main.backgroundColor = _failBackgroundColor;
        rotator.Stop();
    }
}
