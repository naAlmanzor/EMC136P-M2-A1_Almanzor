using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAIScript : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;
    
    [Header("Layer Mask")] 
    public LayerMask whatIsGround, whatIsPlayer;

    [Header("Waypoints")]
    public Transform[] waypoints;
    private int destPoints = 0;
 
    [Header("Enemy Range Settings")] 
    public float areaRange, sightRange;
    private bool playerInRangeArea;
    private bool playerInRangeLine;

    // For raycast;
    private RaycastHit hit;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Patroling();
    }

    // Update is called once per frame
    void Update()
    {
        playerInRangeArea = Physics.CheckSphere(transform.position, areaRange, whatIsPlayer);
        playerInRangeLine = Physics.Raycast(transform.position, transform.forward, out hit, 
        sightRange,LayerMask.GetMask("Default", "Player"));

        // Checks player in range
        if(!playerInRangeArea)
        {
            Patroling();
        }

        else
        {
            Chasing();
        }

        // Checks Player in line of sight
        if(playerInRangeLine)
        {
            // Chase player if in line of sight
            if(hit.transform == player){
                Debug.Log("Player in line of sight");
                Chasing();
            }

            else
            {
                Debug.Log("Player not in line of sight");
                Patroling();
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            Debug.Log("ENEMY HIT PLAYER!");
            StartCoroutine(WaitForFunction());
        }
    }

    void Patroling()
    {
        // Sets destination of enemy to current waypoint
        enemy.destination = waypoints[destPoints].position;
        
        // Checks if enemy is close to the waypoint
        if (!enemy.pathPending && enemy.remainingDistance <= 0.5f){

            // retrurns if there are no waypoints set
            if(waypoints.Length == 0)
            {
                return;
            }

            // Moves to next waypoint in the list and cycles
            destPoints = (destPoints + 1) % waypoints.Length;
        }
    }

    void Chasing()
    {
        enemy.SetDestination(player.position);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, areaRange);
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * sightRange;
        Gizmos.DrawRay(transform.position, direction);
    }

    IEnumerator WaitForFunction()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GameOverScene");  
    }
}
