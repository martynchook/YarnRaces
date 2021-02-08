using System.Collections;
using UnityEngine;

public class Water : MonoBehaviour
{
    public IEnumerator HideOther (Collider other)
    {
        yield return new WaitForSeconds(1f);
        other.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //StartCoroutine(HideOther(other));
        other.gameObject.SetActive(false);
    }
    
}
