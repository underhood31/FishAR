﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
     public GameObject ObjectToSpawn;
     private PlacementIndicator placementIndicator;
     public GameObject placementManager;

     void Start(){
          placementIndicator=FindObjectOfType<PlacementIndicator>();
          StartCoroutine(SpawnStage());
     }

//    void Update(){
//        if(Input.touchCount>0 && Input.touches[0].phase==TouchPhase.Began){
//             GameObject gameObject=Instantiate(ObjectToSpawn,placementIndicator.transform.position, placementIndicator.transform.rotation);

//        }
//    }

   IEnumerator SpawnStage() {
        
        yield return new WaitForSeconds(0.01666f);

        if(Input.touchCount>0 && Input.touches[0].phase==TouchPhase.Began){
            GameObject gameObject=Instantiate(ObjectToSpawn,placementIndicator.transform.position, placementIndicator.transform.rotation);
            Destroy(placementManager);
       }else{
            StartCoroutine(SpawnStage());
       }
    }
}