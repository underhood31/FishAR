using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HookingScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject arCamera;
    public GameObject loadingScreen;
    public Slider slider;
    public Text message;
    public Text message2;
    private bool canCapture;
    void Start()
    {
        message.text="";
        message2.text="";
        canCapture=true;
    }

    // Update is called once per frame
    void Update() {
        message.text=canCapture.ToString();

       if(Input.touchCount>0 && Input.touches[0].phase==TouchPhase.Began){
           RaycastHit hit;
            if(Physics.Raycast(arCamera.transform.position,arCamera.transform.forward, out hit)){
                message2.text=hit.transform.name;
                
                if(hit.transform.tag == "Fish" && canCapture){
                    canCapture=false;
                    Destroy(hit.transform.gameObject);
                    StartCoroutine(showTimer(hit.distance));
                    // Instantiate(smoke,hit.point,Quaternion.LookRotation(hit.normal));
                }
            }
            
        }
    }

    IEnumerator showTimer(float distance){
        distance*=5;
        float initialDist=distance;
        loadingScreen.active=true;
        while(distance>0){
            float timeleft=Mathf.Clamp01(distance/initialDist);
            slider.value=timeleft; 
            distance-=0.1f;
            yield return 0.1f;
        }
        loadingScreen.active=false;
        canCapture=true;
    }
}
