using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GMove : MonoBehaviour
{

    private float global_speed;
    private RandomSpawner p_GManager;

    private bool p_hasTarget = false;
    private bool p_isTurning;

    private Vector3 p_waypoint;
    private Vector3 p_lastWaypoint = new Vector3(0f, 0f, 0f);

    Animator p_animator;
    float p_speed;
    float p_scale;

    private Collider p_collider;
    private RaycastHit p_hit;

    // Start is called before the first frame update
    void Start()
    {
        p_GManager = transform.parent.GetComponentInParent<RandomSpawner>();
        p_animator = this.GetComponent<Animator>();
        global_speed=0.001f;
        SetUp();
    }

    void SetUp()
    {
        float p_scale = 0.075f; // scale according to fish color 
        transform.localScale += new Vector3(p_scale , p_scale, p_scale);
        
        if(transform.GetComponent<Collider>()!= null && transform.GetComponent<Collider>().enabled == true){
            p_collider = transform.GetComponent<Collider>();
        }else if(transform.GetComponentInChildren<Collider>() != null && transform.GetComponentInChildren<Collider>().enabled == true)
        {
            p_collider = transform.GetComponentInChildren<Collider>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!p_hasTarget)
        {
            p_hasTarget = FindTarget();
        }
        else
        {
            RotateNPC(p_waypoint, p_speed);
            transform.position = Vector3.MoveTowards(transform.position, p_waypoint, p_speed * global_speed); // speed change acc to power ups
            
            CollidedNPC();
        }

        if(transform.position == p_waypoint)
        {
            p_hasTarget = false;
        }
    }

    Vector3 GetWaypoint(bool isRandom)
    {
        if (isRandom)
        {
            return p_GManager.RandomPosition();
        }
        else
        {
            return p_GManager.RandomWaypoint();
        }
    }
    
    bool FindTarget(float start = 1f, float end = 2f) //speed change constraint
    {
        p_waypoint = p_GManager.RandomWaypoint();
        if(p_lastWaypoint == p_waypoint)
        {
            p_waypoint = p_GManager.RandomWaypoint(); // call getwaypoint for random values
            return false;
        }
        else
        {
            p_lastWaypoint = p_waypoint;
            p_speed = Random.Range(start, end);
            // p_animator.speed = p_speed;
            return true;
        }
        
    }

    void CollidedNPC()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, transform.localScale.z))
        {
            if(hit.collider == p_collider | hit.collider.tag == "waypoint")
            {
                return;
            }
            int randomNum = Random.Range(1, 100);
            if (randomNum < 50)
                p_hasTarget = false;

            Debug.Log(hit.collider.transform.parent.name + " " + hit.collider.transform.parent.position);
        }
    }

    void RotateNPC(Vector3 waypoint, float curSpeed)
    {
        float turnSpeed = curSpeed * Random.Range(1f, 2f);// adjust speed
        Vector3 lookAt = waypoint - this.transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAt), turnSpeed * Time.deltaTime);

    }
}
