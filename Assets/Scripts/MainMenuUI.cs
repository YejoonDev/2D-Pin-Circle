using System.Collections;
using System.Collections.Generic;
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
    
    public void ButtonClickStartEvent()
    {
        menuPanel.MoveTo(AfterStart, _inactivePosition);
    }

    private void AfterStart()
    {
        stageController.IsGameStart = true;
    }
    
    public void ButtonClickResetEvent()
    {
        Debug.Log("Reset");
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
        menuPanel.MoveTo(AfterStageExitEvent, _activePosition);
    }

    private void AfterStageExitEvent()
    {
        SceneManager.LoadScene(0);
    }
}
