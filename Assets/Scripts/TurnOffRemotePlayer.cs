using Mirror;
using UnityEngine;
using UnityEngine.Networking;

//Note: add script to player gameobject
//      when multiplayer game starts
//      turns off scripts off other players that are not you
//      why? so you dont control them

public class TurnOffRemotePlayer : NetworkBehaviour
{
    private void Start()
    {
        string id = string.Format("{0}", this.netId);
        PlayerScript scr = this.GetComponent<PlayerScript>();

        if (this.isLocalPlayer == true)
        {
            scr.enabled = true;
            scr.SetPlayerCaption(id);
            // scr.SetTitle("MultiPlayer #" + id);
        }
        else
        {
            scr.SetPlayerCaption(id);
            scr.enabled = false;
        }

    }
}
