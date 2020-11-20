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
        }

        if(transform.position == p_waypoint)
        {
            p_hasTarget = false;
        }
    }

    bool FindTarget(float start = 1f, float end = 2f) //speed change constraint
    {
        p_waypoint = p_GManager.RandomWaypoint();
        if(p_lastWaypoint == p_waypoint)
        {
            p_waypoint = p_GManager.RandomWaypoint();
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

    void RotateNPC(Vector3 waypoint, float curSpeed)
    {
        float turnSpeed = curSpeed * Random.Range(1f, 2f);// adjust speed
        Vector3 lookAt = waypoint - this.transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAt), turnSpeed * Time.deltaTime);

    }
}
