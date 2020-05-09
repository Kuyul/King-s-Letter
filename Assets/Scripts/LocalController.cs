using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalController : MonoBehaviour
{
    public static LocalController instance;
    public GameNetworkManager networkManager;
    public Text info;
    private GamePlayerController player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetLocalPlayer(GamePlayerController p)
    {
        player = p;
        info.text = "connected to " + networkManager.networkAddress + ", port: " + networkManager.networkPort;
    }

    public void ButtonA()
    {
        player.CmdButtonA();
    }

    public void ButtonB()
    {
        player.CmdButtonB();
    }

    public void ButtonC()
    {
        player.CmdButtonC();
    }

    public void ButtonD()
    {
        player.CmdButtonD();
    }
}
