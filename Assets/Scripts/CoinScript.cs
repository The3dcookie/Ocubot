using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public GameObject particleEffect;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Instantiate(particleEffect,transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
    }
}
