using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Application = UnityEngine.Device.Application;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private StageController stageController;
    [SerializeField] private RectTransformMover menuPanel;
    private readonly Vector3 _inactivePosition = Vector3.left * 1080;
    private readonly Vector3 _activePosition = Vector3.zero;
    
    [SerializeField] private TextMeshProUGUI textLevelInMenu;
    [SerializeField] private TextMeshProUGUI textLevelInGame;

    private void Awake()
    {
        int index = PlayerPrefs.GetInt("StageLevel");
        textLevelInMenu.text = $"Level {(index + 1)}";
    }

    public void ButtonClickStartEvent()
    {
        int index = PlayerPrefs.GetInt("StageLevel");
        textLevelInGame.text = (index + 1).ToString();
        
        menuPanel.MoveTo(AfterStart, _inactivePosition);
    }

    private void AfterStart()
    {
        stageController.IsGameStart = true;
    }
    
    public void ButtonClickResetEvent()
    {
        PlayerPrefs.SetInt("StageLevel", 0);
        menuPanel.MoveTo(AfterStart, _inactivePosition);
    }
    
    public void ButtonClickExitEvent()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
 #endif
    }

    public void StageExit()
    {
        int index = PlayerPrefs.GetInt("StageLevel");
        textLevelInMenu.text = $"Level {(index + 1)}";
        
        menuPanel.MoveTo(AfterStageExitEvent, _activePosition);
    }

    private void AfterStageExitEvent()
    {
        int index = PlayerPrefs.GetInt("StageLevel");
        
        // 마지막 스테이지를 클리어 했을 때 처리
        if (index == SceneManager.sceneCountInBuildSettings)
        {
            PlayerPrefs.SetInt("StageLevel", 0);
            SceneManager.LoadScene(0);
            return;
        }
        
        SceneManager.LoadScene(index);
    }
}
