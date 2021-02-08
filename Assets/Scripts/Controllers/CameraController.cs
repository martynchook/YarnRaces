using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour, ITick
{
    [SerializeField] private Transform followPoint;
    [SerializeField] private Transform player;
    
    void Start()
    {
        ManagerUpdate.AddTo(this);
        transform.position = followPoint.position;
    }
    
    public void Tick()
    { 
        if (!Toolbox.Get<GameManager>().CheckLoseGame())
        {
            transform.DORotate(new Vector3(transform.eulerAngles.x, player.transform.eulerAngles.y, player.transform.eulerAngles.z), 0.01f);
            //transform.DOLookAt(new Vector3(player.position.x, player.position.y + 4f, player.position.z), 0f);
            transform.DOMove(followPoint.position, 0.01f); 
        }
    }
}