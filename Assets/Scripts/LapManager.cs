using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LapManager : MonoBehaviour
{
    public List<Checkpoint> checkpoints;
    public int totalLaps;

    public GameObject finishRaceUI;

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<CarController>())
        {
            CarController car = other.gameObject.GetComponent<CarController>();
            if(car.checkpointIndex == checkpoints.Count)
            {
                car.checkpointIndex = 0;
                car.lapNumber++;

                Debug.Log($"Player on lap {car.lapNumber}");

                if(car.lapNumber > totalLaps)
                {
                    Debug.Log("RaceOver");
                    finishRaceUI.SetActive(true);
                }
            }       
        }
    }
}
