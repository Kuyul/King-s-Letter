using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Toggle tglNetworkHUD;
    public Toggle controller;

    // Start is called before the first frame update
    void Start()
    {
        // For a headless server, immediately load the game scene
        if (SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Null)
        {
            StartCoroutine(LoadGameScene());
        }
    }

    public void JoinMatch()
    {
        PlayerPrefs.SetInt("ShowUnityHUD", tglNetworkHUD.isOn ? 1 : 0);
        PlayerPrefs.SetInt("MainClient", 1);
        StartCoroutine(LoadGameScene());
    }

    private IEnumerator LoadGameScene()
    {
        var asyncload = SceneManager.LoadSceneAsync("MainClient");
        while (!asyncload.isDone)
        {
            yield return null;
        }
    }

    public void InitiateController()
    {
        PlayerPrefs.SetInt("ShowUnityHUD", 0);
        PlayerPrefs.SetInt("MainClient", 0);
        StartCoroutine(LoadController());
    }

    private IEnumerator LoadController()
    {
        var asyncload = SceneManager.LoadSceneAsync("Controller");
        while (!asyncload.isDone)
        {
            yield return null;
        }
    }
}
