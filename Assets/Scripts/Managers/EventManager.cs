using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "EventManager", menuName = "Managers/EventManager")]
public class EventManager : ManagerBase
{
    // Загрузка следующего уровня
    public delegate void OnLoudNextLevel();
    public event OnLoudNextLevel OnLoudNextLevelEvent;
    
    // Перезапуск текущего уровня
    public delegate void OnRestartLevel();
    public event OnRestartLevel OnRestartLevelEvent;
    
    // Проигрыш (касание воды)
    public delegate void OnGameOver();
    public event OnGameOver OnGameOverEvent;
    
    // Нажати не кнопку play
    public delegate void OnBtnPlayClick();
    public event OnBtnPlayClick OnBtnPlayClickEvent;
    
    // Начало игры после отчета
    public delegate void OnGameStart();
    public event OnGameStart OnGameStartEvent;
    
    // Нажати не кнопку play
    public delegate void OnFinish();
    public event OnFinish OnFinishEvent;

    public void LoudNextLevelEvent()
    {
        Debug.Log("Событие: загрузка следующего уровня");
        OnLoudNextLevelEvent?.Invoke();
    }
    
    public void RestartLevelEvent()
    {
        OnRestartLevelEvent?.Invoke();
    }
    
    public void GameOverEvent()
    {
        OnGameOverEvent?.Invoke();
    }
    
    public void GameStartEvent()
    {
        OnGameStartEvent?.Invoke();
    }
    
    public void BtnPlayClick()
    {
        OnBtnPlayClickEvent?.Invoke();
    }
    
    public void FinishEvent()
    {
        OnFinishEvent?.Invoke();
    }
}
