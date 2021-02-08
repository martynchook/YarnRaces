using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class UIController : MonoBehaviour
{
    [SerializeField] private RectTransform MainMenuPnl, StartGamePnl, FinishPnl;
    [SerializeField] private Button restartButton;
    [SerializeField] private Text startDelayText;
    private int startDelay = 3;
    

    private void Start()
    {
        ShowMainMenuPnl();
        Toolbox.Get<EventManager>().OnLoudNextLevelEvent += ShowMainMenuPnl;
        Toolbox.Get<EventManager>().OnLoudNextLevelEvent += HideFinishGamePnl;
        
        Toolbox.Get<EventManager>().OnRestartLevelEvent += ShowMainMenuPnl;
        
        Toolbox.Get<EventManager>().OnGameOverEvent += SetActivRestartBtnOn;

        Toolbox.Get<EventManager>().OnBtnPlayClickEvent += StartGame;
        
        Toolbox.Get<EventManager>().OnFinishEvent += ShowFinishGamePnl;
    }
    
    public void StartGame()
    {
        HideMainMenuPnl();
        ShowStartGamePnl();
        StartCoroutine(UpdateDelay());
    }

    public void ShowMainMenuPnl()
    {
        MainMenuPnl.gameObject.SetActive(true);
        SetActivRestartBtnOff();
    }
    
    public void HideMainMenuPnl()
    {
        MainMenuPnl.gameObject.SetActive(false);
    }
    
    public void ShowStartGamePnl()
    {
        StartGamePnl.gameObject.SetActive(true);
    }
    
    public void HideStartGamePnl()
    {
        StartGamePnl.gameObject.SetActive(false);
    }
    
    public void ShowFinishGamePnl()
    {
        FinishPnl.gameObject.SetActive(true);
    }
    
    public void HideFinishGamePnl()
    {
        FinishPnl.gameObject.SetActive(false);
    }

    public void SetActivRestartBtnOn()
    {
        restartButton.gameObject.SetActive(true);
    }
    
    public void SetActivRestartBtnOff()
    {
        restartButton.gameObject.SetActive(false);
    }
    
    public IEnumerator UpdateDelay()
    {
        for (int i = startDelay; i > 0; i--)
        {
            startDelayText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        Toolbox.Get<EventManager>().GameStartEvent();
        HideStartGamePnl();
    }
}