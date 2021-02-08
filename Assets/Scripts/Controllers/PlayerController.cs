using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, ITick
{
    private float speed = 5f;
    private float boostSpeed = 0.25f;
    private float torsionSpeed = 50f;
    private float tapPoint;
    private float swipeDelta; 
    private float yEulerAngles;
    private Vector3 dirRotate;
    private bool isDragging = false;
    public Transform Visual;
    public Transform startPos;
    
    private String name = "Pablo4U";
    private int place;

    void Start()
    {
        Toolbox.Get<EventManager>().OnLoudNextLevelEvent += ResetStats;
        Toolbox.Get<EventManager>().OnRestartLevelEvent += ResetStats;
        Toolbox.Get<EventManager>().OnLoudNextLevelEvent += stopcour;
        Toolbox.Get<EventManager>().OnRestartLevelEvent += stopcour;
        Toolbox.Get<EventManager>().OnGameOverEvent += stopcour;
        ManagerUpdate.AddTo(this);
    }
    
    public void Tick()
    {
        HandleInput();
        HandleMove();
        if (Toolbox.Get<GameManager>().CheckStartGame() && !Toolbox.Get<GameManager>().CheckLoseGame())
        {
            transform.Translate(Vector3.forward * Time.deltaTime * (speed ) * transform.localScale.x);
            Visual.DORotate(new Vector3(0f, -(Visual.rotation.y + torsionSpeed) * (speed + boostSpeed) * Time.deltaTime, 0f), 0f, RotateMode.LocalAxisAdd);
        }
    }
    
    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tapPoint = Input.mousePosition.x;
            isDragging = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            yEulerAngles = transform.eulerAngles.y;
        }
    }
    
    private void HandleMove()
    {
        if (isDragging) 
        {
            swipeDelta = Input.mousePosition.x - tapPoint;
            if (swipeDelta > 0)
            {
                swipeDelta = Mathf.Lerp(0f, 180f, Math.Abs(swipeDelta / Screen.width)); 
            }
            else
            {
                swipeDelta = Mathf.Lerp(0f, 180f, Math.Abs(swipeDelta / Screen.width)) * -1f;
            }
            dirRotate = new Vector3(0f, transform.rotation.y + swipeDelta + yEulerAngles,  0f);
            transform.DORotate(dirRotate, 0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Boost>())
        {
            KeepBoost(other);
        }
        if (other.GetComponent<Water>())
        {
            Toolbox.Get<EventManager>().GameOverEvent();
        }
        if (other.GetComponent<FinishController>())
        {
            place = other.GetComponent<FinishController>().GetPlace();
            Debug.Log(place+ " место занимает: " + name);
            if (place == 1)
            {
                StartCoroutine(FirstPlase());
            }
            else
            {
                speed = 0;
                Toolbox.Get<EventManager>().FinishEvent();
            }
        }
    }
    
    private void KeepBoost(Collider boost)
    {
        boost.enabled = false;
        boostSpeed += 0.3f;
        gameObject.transform.localScale += new Vector3(0.075f, 0.075f, 0.075f);
        gameObject.GetComponent<Rigidbody>().mass += 0.5f;
        boost.gameObject.GetComponent<Renderer>().material = gameObject.GetComponent<Renderer>().material;
        boost.gameObject.transform.DOScale(new Vector3(0f, 0f, 0f), 0.5f);
    }
    
    private void ResetStats()
    {
        transform.position = startPos.position;
        transform.rotation = Quaternion.identity;
        transform.localScale = new Vector3(1,1,1);
        boostSpeed = 0.3f;
        speed = 5f;
        gameObject.GetComponent<Rigidbody>().mass = 5f;
        gameObject.SetActive(true);
        place = 0;
    }
    
    IEnumerator FirstPlase()
    {
        speed += 5f;
        yield return new DOTweenCYInstruction.WaitForCompletion(gameObject.transform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), transform.localScale.x / 2));
        Toolbox.Get<EventManager>().FinishEvent();
    }

    public void stopcour()
    {
        StopCoroutine(FirstPlase());
    }
}
