using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public override void OnStartServer()
    {
        base.OnStartServer();

        if (isServer)
        {
            GameControl.instance.AddPlayer(this);
        }
    }
}
