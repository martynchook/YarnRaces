using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boost : MonoBehaviour
{
    private Material initMaterial;
    private Vector3 initScale;
    
    private void Start()
    {
        initMaterial = gameObject.GetComponent<Renderer>().material;
        initScale = transform.localScale;
    }

    public IEnumerator scaleBack()
    {
        yield return new WaitForSeconds(3f);
        gameObject.GetComponent<Collider>().enabled = true; 
        gameObject.GetComponent<Renderer>().material = initMaterial;
        gameObject.SetActive(true);
        transform.DOScale(initScale, 0.5f);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<Collider>().enabled = false; 
        StartCoroutine(scaleBack());
    }
}