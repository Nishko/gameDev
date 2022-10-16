using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public GameObject failedRaceUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            failedRaceUI.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        Debug.Log($"{collision.gameObject.name}");
        if(collision.gameObject.tag == "Player")
        {
            failedRaceUI.SetActive(true);
        }

    }
}
