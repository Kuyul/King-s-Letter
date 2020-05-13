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

    //This method is called from the GamePlayerController when a remote client joins as a controller
    public void SetLocalPlayer(GamePlayerController p)
    {
        player = p;
        info.text = "connected to " + networkManager.networkAddress + ", port: " + networkManager.networkPort;
    }

    public void PressButton(string button)
    {
        player.CmdButton(button);
    }
}
