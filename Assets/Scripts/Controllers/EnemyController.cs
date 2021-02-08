using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour, ITick
{
    
    private float speed = 5.1f;
    private float boostSpeed = 0.4f;
    private float rotateSpeed = 50f;
    private NavMeshAgent enemy;
    private int place;
    private String name;
    public Transform Visual;
    
    public Transform startPos;
    private Transform nextPoint;
    private int followPointIndex = 0;
    private LevelPath _levelPath;
    private bool haveOtherPoint;

    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        ManagerUpdate.AddTo(this);
        ResetStats();
        Toolbox.Get<EventManager>().OnLoudNextLevelEvent += ResetStats;
        Toolbox.Get<EventManager>().OnRestartLevelEvent += ResetStats;
        Toolbox.Get<EventManager>().OnGameStartEvent += UnStop;
    }

    public void Tick()
    {
        if (Toolbox.Get<GameManager>().CheckStartGame())
        {
            if (!enemy.isStopped)
            {
                Visual.DORotate(new Vector3(0f, -(Visual.rotation.y + rotateSpeed) * enemy.speed * Time.deltaTime, 0f), 0f, RotateMode.LocalAxisAdd);   
            }
            if (Vector3.Distance(transform.position, nextPoint.position) <= 2f)
            {
                Debug.Log("Next point");
                GetNextFollowPoint();
               // enemy.SetDestination(nextPoint.position);
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<KeepTriger>()  && !haveOtherPoint)
        {
            if (Random.Range(1,3) == 1)
            {
                StartCoroutine(Keep(other));
            }
        }
        if (other.gameObject.GetComponent<Boost>())
        {
            KeepBoost(other);
        }
        // if (other.GetComponent<Water>())
        // {
        //     Toolbox.Get<GameManager>().LoseGame();
        //     gameObject.SetActive(false);
        // }
        if (other.GetComponent<FinishController>())
        {
            enemy.isStopped = true;
            place = other.GetComponent<FinishController>().GetPlace();
        }
    }

    IEnumerator Keep (Collider boost)
    {
        Debug.Log("keep");
        haveOtherPoint = true;
        enemy.SetDestination(boost.transform.position);
        var ttt = boost.transform.position;
        //yield return new DOTweenCYInstruction.WaitForCompletion(gameObject.transform.DOMove(boost.transform.position,
            //Vector3.Distance(boost.transform.position, transform.position) / enemy.speed));
        //yield return new WaitUntil(() => Vector3.Distance(transform.position, ttt) < 1f);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("keepcomplit");
        enemy.SetDestination(nextPoint.position);
        yield return new WaitForSeconds(2f);
        haveOtherPoint = false;
    }

    private void KeepBoost(Collider boost)
    {
        enemy.speed += boostSpeed;
        gameObject.transform.localScale += new Vector3(0.075f, 0.075f, 0.075f);
        gameObject.GetComponent<Rigidbody>().mass += 0.5f;
        boost.gameObject.GetComponent<Renderer>().material = gameObject.GetComponent<Renderer>().material;
        boost.gameObject.transform.DOScale(new Vector3(0f, 0f, 0f), 0.5f);
    }
    
    private void ResetStats()
    {
        enemy.speed = speed;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        enemy.destination = startPos.position;
        followPointIndex = 0;
        transform.position = new Vector3 (startPos.position.x, startPos.position.y + 10f, startPos.position.z);
        transform.DOMove(startPos.position, Random.Range(1,3));
         // nextpoint хранит на начало раунда нужную позицию
        transform.rotation = Quaternion.identity;
        transform.localScale = new Vector3(1f, 1f, 1f);
        gameObject.GetComponent<Rigidbody>().mass = 5f;
        haveOtherPoint = false;
        enemy.isStopped = true;
        enemy.enabled = false;
    }
    
    public void GetNextFollowPoint()
    {
        if (followPointIndex < _levelPath.followPoints.Length - 1)
        {
            followPointIndex++;
            nextPoint =  GameObject.Find("LevelPath").GetComponent<LevelPath>().GetNextPoint(followPointIndex);
            enemy.SetDestination(nextPoint.position); 
        }
    }

    public void UnStop()
    {
        enemy.enabled = true;
        enemy.isStopped = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        _levelPath = GameObject.Find("LevelPath").GetComponent<LevelPath>();
        Debug.Log(_levelPath.name);
        //_levelPath = GameObject.FindWithTag("LevelPath").GetComponent<LevelPath>();
        nextPoint =  _levelPath.followPoints[followPointIndex];
        enemy.SetDestination(nextPoint.position);
    }
    
    IEnumerator InitEnemy()
    {
        yield return new WaitForSeconds(Random.Range(1,3));
        transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
    }
}