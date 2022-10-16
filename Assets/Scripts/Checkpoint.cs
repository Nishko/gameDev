using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int index;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CarController>())
        {
            CarController car = other.gameObject.GetComponent<CarController>();
            if(car.checkpointIndex == index - 1)
            {
                car.checkpointIndex = index;
            }
        }
    }
}
