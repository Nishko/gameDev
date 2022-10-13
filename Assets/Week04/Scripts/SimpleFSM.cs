using UnityEngine;
using System.Collections;

public class SimpleFSM : MonoBehaviour
{
    public enum FSMState
    {
        None,
        Patrol,
        Dead,
        Chase,
    }

    // Current state that the NPC is reaching
    public FSMState curState;

    public float moveSpeed = 12.0f; // Speed of the tank
    public float rotSpeed = 2.0f; // Tank Rotation Speed

    public float chaseRange = 35; // Range of player to initiate chase state
    public float attackRange = 20; // Range of player to initiate attack state
    public float stopRange = 10; // Range of player to stop movement of the tank

    protected Transform playerTransform;// Player Transform
    protected Vector3 destPos; // Next destination position of the NPC Tank
    protected GameObject[] pointList; // List of points for patrolling

    // Whether the NPC is destroyed or not
    protected bool bDead;
    public int health = 100;

    //Bullet shooting rate
    public float shootRate = 1.5f;
    protected float elapsedTime;


    /*
     * Initialize the Finite state machine for the NPC tank
     */
    void Start()
    {
        curState = FSMState.Patrol;

        bDead = false;

        // Get the list of patrol points
        pointList = GameObject.FindGameObjectsWithTag("PatrolPoint");
        FindNextPoint();  // Set a random destination point first

        elapsedTime = shootRate; // allow first press to fire
       // rotSpeed = rotSpeed * 180 / Mathf.PI; // convert from rad to deg for rot function

        // Get the target (Player)
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerTransform = objPlayer.transform;
        if (!playerTransform)
            print("Player doesn't exist.. Please add one with Tag named 'Player'");
    }


    // Update each frame
    void Update()
    {
        // Update the time
        elapsedTime += Time.deltaTime;

        switch (curState)
        {
            case FSMState.Patrol: UpdatePatrolState(); break;
            case FSMState.Dead: UpdateDeadState(); break;
            case FSMState.Chase: UpdateChaseState(); break;
        }

        // Go to dead state if no health left
        if (health <= 0)
            curState = FSMState.Dead;

        // goto chase state if within chase range
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if (distance <= chaseRange)
        {
            curState = FSMState.Chase;
        } else
        {
            curState = FSMState.Patrol;
        }
    }

    /*
     * Patrol state
     */
    protected void UpdatePatrolState()
    {
        // Find another random patrol point if the current point is reached
        if (Vector3.Distance(transform.position, destPos) <= 2.0f)
        {
            FindNextPoint();
        }

        // Rotate to the target point
        Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
        GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed));

        // Go Forward
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.forward * Time.deltaTime * moveSpeed);
    }



    /*
     * Dead state
     */
    protected void UpdateDeadState()
    {
        // Show the dead animation with some physics effects
        if (!bDead)
        {
            bDead = true;
        }
    }


    protected void UpdateChaseState()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if ((distance <= chaseRange) & (distance > stopRange))
        {
            Vector3 chaserPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
            Vector3 targetPos = new Vector3(playerTransform.position.x, 0.0f, playerTransform.position.z);

            GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPos - chaserPos), rotSpeed * Time.deltaTime));

            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.forward * moveSpeed * Time.deltaTime);
        }
    }


    // Find the next semi-random patrol point
    protected void FindNextPoint()
    {
        int rndIndex = Random.Range(0, pointList.Length);
        destPos = pointList[rndIndex].transform.position;
    }

}
