using UnityEngine;
using UnityEngine.UI;
  
[RequireComponent(typeof(Rigidbody))]
public class PlayerScript : MonoBehaviour
{
    public float Movespeed = 3.5f;
    public float Turnspeed = 120f;
    private TextMesh tm = null;
    private Text ui = null;

    private void Update()
    {
        //move
        float vert = Input.GetAxis("Vertical");
        float horz = Input.GetAxis("Horizontal");
        this.transform.Translate(Vector3.forward * vert * Movespeed * Time.deltaTime);
        this.transform.localRotation *= Quaternion.AngleAxis(horz * Turnspeed * Time.deltaTime, Vector3.up);
    }
    public void SetPlayerCaption(string caption)
    {
        if (tm == null)
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                if (this.transform.GetChild(i).name == "Caption")
                {
                    tm = this.transform.GetChild(i).GetComponent<TextMesh>();
                }
            }
        }

        if (tm != null)
        {
            tm.text = caption;
        }
        else
        {
            tm.text = "err";
        }
    }
    // public void SetTitle(string caption)
    // {
    //     if (ui == null)
    //     {
    //         ui = GameObject.Find("txtPlayer").GetComponent<Text>();
    //     }

    //     if (ui != null)
    //     {
    //         ui.text = caption;
    //     }
    // }
}