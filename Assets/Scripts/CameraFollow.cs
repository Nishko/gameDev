using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;

    public float vectorX = 0, vectorY = 1, vectorZ = -5;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(vectorX, vectorY, -vectorZ);
        transform.rotation = player.transform.rotation;
    }
}