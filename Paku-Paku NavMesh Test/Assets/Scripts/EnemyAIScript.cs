using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAIScript : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;
    
    [Header("Layer Mask")] 
    public LayerMask whatIsGround, whatIsPlayer;

    [Header("Waypoints")]
    public Transform start;
    public Transform end;
    public Transform target;
 
    [Header("Enemy Range Settings")] 
    public float areaRange, sightRange;
    private bool playerInRangeArea;
    private bool playerInRangeLine;

    // For raycast;
    private RaycastHit hit;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        start = GameObject.FindGameObjectWithTag("start").transform;
        end = GameObject.FindGameObjectWithTag("end").transform;
        enemy = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // target = start;
        Patroling();
    }

    // Update is called once per frame
    void Update()
    {
        playerInRangeArea = Physics.CheckSphere(transform.position, areaRange, whatIsPlayer);
        playerInRangeLine = Physics.Raycast(transform.position, transform.forward, out hit, 
        sightRange,LayerMask.GetMask("Default", "Player"));

        // Checks Player in Radius
        if(!playerInRangeArea)
        {
            Debug.Log("Player not in range");
            Patroling();
        }
        
        // Chase player if in radius range
        else
        {
            Debug.Log("Player in range");
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
        }
    }

    void searchWaypoints()
    {
        // wayPoint = new Vector3(transform.position.x + waypointStart.position.x)
    }
    void Patroling()
    {
        if (enemy.remainingDistance <= enemy.stoppingDistance && start){
            enemy.SetDestination(end.position);
        }

        if (enemy.remainingDistance <= enemy.stoppingDistance && end){
            enemy.SetDestination(start.position);
        }
        
    }

    void Chasing()
    {
        enemy.SetDestination(player.position);
    }

    // void UpdateDestination()
    // {
    //     target = waypoints[waypointIndex];
    //     enemy.SetDestination(target.position);
    // }

    // void IterateWaypoint()
    // {
    //     waypointIndex++;
    //     if(waypointIndex == waypoints.Length)
    //     {
    //         waypointIndex = 0;
    //     }
    // }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, areaRange);
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * sightRange;
        Gizmos.DrawRay(transform.position, direction);
    }
}
