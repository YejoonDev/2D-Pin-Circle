using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StageController : MonoBehaviour
{
    [SerializeField] private MainMenuUI mainMenuUI;
    [SerializeField] private PinSpawner pinSpawner;
    [SerializeField] private int tPinCount;
    [SerializeField] private int sPinCount;
    [SerializeField] private Rotator rotator;
    
    private readonly Color _failBackgroundColor = new Color(0.4f, 0.1f, 0.1f);
    private readonly Color _clearBackgroundColor = new Color(0, 0.5f, 0.25f);
    public bool IsGameOver { set; get; } = false;
    public bool IsGameStart { set; get; } = false;
    
    private readonly Vector3 _firstTPinPosition = Vector3.down * 2;
    public float TPinDistance { get; private set; } = 1;

    [SerializeField] private AudioClip audioGameOver;
    [SerializeField] private AudioClip audioGameClear;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        pinSpawner.Setup();
        
        // Throwable Pin 생성
        for (int i = 0; i < tPinCount; i++)
        {
            pinSpawner.SpawnThrowablePin(_firstTPinPosition + Vector3.down * TPinDistance * i,
                tPinCount - i);
        }
        
        // Stuck Pin 생성
        for (int i = 0; i < sPinCount; i++)
        {
            float angle = (360f / sPinCount) * i;
            pinSpawner.SpawnStuckPin(angle,
                tPinCount + 1 + i);
        }
    }

    public void DecreaseTPin()
    {
        tPinCount--;
        if (tPinCount <= 0)
        {
            StartCoroutine(GameClear());
        }
    }
    
    public void GameOver()
    {
        IsGameOver = true;
        Camera.main.backgroundColor = _failBackgroundColor;
        rotator.Stop();

        _audioSource.clip = audioGameOver;
        _audioSource.Play();
        
        StartCoroutine("StageExit", 0.5f);
    }

    IEnumerator GameClear()
    {
        yield return new WaitForSeconds(0.1f);

        if (IsGameOver == true)
        {
            yield break;
        }

        Camera.main.backgroundColor = _clearBackgroundColor;
        rotator.RotateFast();

        int index = PlayerPrefs.GetInt("StageLevel");
        PlayerPrefs.SetInt("StageLevel", index+1);

        _audioSource.clip = audioGameClear;
        _audioSource.Play();
        
        StartCoroutine("StageExit", 1f);
    }

    private IEnumerator StageExit(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        mainMenuUI.StageExit();
    }
    
    
}
