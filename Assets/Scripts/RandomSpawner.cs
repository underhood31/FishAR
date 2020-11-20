using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;


[System.Serializable]
public class GObjects
{

    public string GroupName { get { return p_GroupName; } }
    public GameObject objectPrefab { get { return p_objectPrefab; } }
    public int maxSpawnCount { get { return p_maxSpawnCount; } }
    public int spawnRate { get { return p_spawnRate; } }
    public int spawnAmount { get { return p_spawnAmount; } }
    public bool randomizeStats { get { return p_randomizeStats; } }
    public bool enableSpawner { get { return p_enableSpawner;  } } //>>

    [Header("Group Stats")]
    [SerializeField]
    private string p_GroupName;
    [SerializeField]
    private GameObject p_objectPrefab;
    [SerializeField]
    [Range(0f, 30f)]
    private int p_maxSpawnCount;
    [SerializeField]
    [Range(0f, 10f)]
    private int p_spawnRate;
    [SerializeField]
    [Range(0f, 10f)]
    private int p_spawnAmount;

    [Header("Settings")]
    [SerializeField]
    private bool p_enableSpawner;
    [SerializeField]
    private bool p_randomizeStats;



    public void setValues(int maxSpawnCount, int spawnRate, int spawnAmount)
    {
        this.p_maxSpawnCount = maxSpawnCount;
        this.p_spawnRate = spawnRate;
        this.p_spawnAmount = spawnAmount;
    }
}




public class RandomSpawner : MonoBehaviour
{

    public List<Transform> Waypoints = new List<Transform>();

    public float spawnTimer { get { return p_spawnTimer; } }
    public Vector3 spawnArea { get { return p_spawnArea; } }


    [Header("Global Setting")]
    [Range(0f, 600f)]
    [SerializeField]
    private float p_spawnTimer;
    [SerializeField]
    private Color p_spawnColor = new Color(1.000f, 0.000f, 0.000f, 0.300f); 
    [SerializeField]
    private Vector3 p_spawnArea = new Vector3(50f, 50f, 2f); // see size according to lake dimension




    [Header("Group Settings")]
    public GObjects[] Gobject = new GObjects[4];

 



    // Start is called before the first frame update
    void Start()
    {
        GetWayPoints();
        RandomizeGroups();
        CreateGroups();
        InvokeRepeating("SpawnNPC", 0.5f, spawnTimer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnNPC()
    {
        for(int i = 0; i < Gobject.Count(); i++)
        {
            if(Gobject[i].enableSpawner && Gobject[i].objectPrefab != null)
            {
                GameObject temp = GameObject.Find(Gobject[i].GroupName);
                if(temp.GetComponentInChildren<Transform>().childCount < Gobject[i].maxSpawnCount)
                {
                    for(int y = 0; y < Random.Range(0, Gobject[i].spawnAmount); y++)
                    {
                        Quaternion rRotation = Quaternion.Euler(Random.Range(-20, 20), Random.Range(0, 360), 0);
                        GameObject tempSpawn;
                        tempSpawn = (GameObject)Instantiate(Gobject[i].objectPrefab, RandomPosition(), rRotation);
                        tempSpawn.transform.parent = temp.transform;
                        tempSpawn.AddComponent<GMove>();
                    }
                }
            }
        }
    }

    public Vector3 RandomPosition()
    {
        Vector3 rPosition = new Vector3(
            Random.Range(-spawnArea.x, spawnArea.x),
            Random.Range(-spawnArea.y, spawnArea.y),
            Random.Range(-spawnArea.z, spawnArea.z)
        );

        rPosition = transform.TransformPoint(rPosition * .5f);
        return rPosition;
    }

    public Vector3 RandomWaypoint()
    {
        int rPoint = Random.Range(0, (Waypoints.Count - 1));
        Vector3 randomWayPoint = Waypoints[rPoint].transform.position;
        return randomWayPoint;
    }

    void RandomizeGroups()
    {
        for(int i = 0; i < Gobject.Count(); i++)
        {
            if (Gobject[i].randomizeStats)
            {
                Gobject[i].setValues(Random.Range(1, 30), Random.Range(1, 10), Random.Range(1, 10));
            }
        }
    }

    void CreateGroups()
    {
        for(int i = 0; i < Gobject.Count(); i++)
        {
            GameObject SpawnGroup;

            SpawnGroup = new GameObject(Gobject[i].GroupName);
            SpawnGroup.transform.parent = this.gameObject.transform;
        }
    }


    void GetWayPoints()
    {
        Transform[] wpList = this.transform.GetComponentsInChildren<Transform>();
        for(int i = 0; i < wpList.Length; i++)
        {
            if(wpList[i].tag == "waypoint")
            {
                Waypoints.Add(wpList[i]);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = p_spawnColor;
        Gizmos.DrawCube(transform.position, spawnArea);
    }
}
