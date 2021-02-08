using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GameManager", menuName = "Managers/GameManager")]
public class GameManager : ManagerBase, IStart
{
    private bool isGameStart;
    private bool isLose;
    private int startDelay = 3;
    
    public void OnStart()
    {
        isGameStart = false;
        isLose = false;
        
        Toolbox.Get<EventManager>().OnLoudNextLevelEvent += StopGame;
        Toolbox.Get<EventManager>().OnLoudNextLevelEvent += UnloseGame;
        
        Toolbox.Get<EventManager>().OnRestartLevelEvent += StopGame;
        Toolbox.Get<EventManager>().OnRestartLevelEvent += UnloseGame;
        
        Toolbox.Get<EventManager>().OnGameOverEvent += LoseGame;

        Toolbox.Get<EventManager>().OnGameStartEvent += StartGame;
        Toolbox.Get<EventManager>().OnGameStartEvent += UnloseGame;

        Toolbox.Get<EventManager>().OnFinishEvent += StopGame;
    }

    public void StartGame()
    {
        isGameStart = true;
    }
    
    public void StopGame()
    {
        isGameStart = false;
    }
    
    public void LoseGame()
    {
        isLose = true;
    }
    
    public void UnloseGame()
    {
        isLose = false;
    }
    
    public bool CheckStartGame()
    {
        return isGameStart;
    }
    
    public bool CheckLoseGame()
    {
        return isLose;
    }
}