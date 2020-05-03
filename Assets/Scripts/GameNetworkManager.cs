using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using Aws.GameLift.Server;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using Amazon.Runtime;
using Amazon.CognitoIdentity;
using Amazon;
using System.Text;
using UnityEngine.SceneManagement;

public class GameNetworkManager : NetworkManager
{
    public static GameNetworkManager instance;
    private bool isHeadlessServer = false;
    private bool isGameliftServer = false;
    private static int LISTEN_PORT = 7777;
    public bool mainClient = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UnityInitializer.AttachToGameObject(this.gameObject);
        // detect headless server mode
        if (SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Null)
        {
            Debug.Log("** SERVER MODE **");
            isHeadlessServer = true;
            SetupServerAndGamelift();
        } else if (PlayerPrefs.GetInt("MainClient") == 1)
        {
            mainClient = true;
            SetupMainClient();
        } else
        {
            SetupController();
        }
    }

    //Tick the tickbox on the main menu to connect locally.
    private void SetupController()
    {
        if (PlayerPrefs.GetInt("ShowUnityHUD", 1) == 0)
        {
            FindMatch();
        } else
        {
            ConnectLocal();
        }
    }

    private void SetupMainClient()
    {
        // in debug mode don't attempt to match with GameLift
        if (PlayerPrefs.GetInt("ShowUnityHUD", 1) == 0)
        {
            // TODO should set text in UIController while finding match
            FindMatch();
        }
    }

    private void ConnectLocal()
    {
        networkAddress = "localhost";
        networkPort = 7777;
        StartClient();
    }


    private void FindMatch()
    {
        Debug.Log("Reaching out to client service Lambda function");

        AWSConfigs.AWSRegion = "ap-southeast-1"; // Your region here
        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;
        // paste this in from the Amazon Cognito Identity Pool console
        // Initialize the Amazon Cognito credentials provider
        CognitoAWSCredentials credentials = new CognitoAWSCredentials(
            "ap-southeast-2:af3f91f3-0777-469f-b036-ec71a66068cc", // Identity pool ID
            RegionEndpoint.APSoutheast2 // Region
        );

        AmazonLambdaClient client = new AmazonLambdaClient(credentials, RegionEndpoint.APSoutheast2);
        InvokeRequest request = new InvokeRequest
        {
            FunctionName = "TestCreateGameSession",
            InvocationType = InvocationType.RequestResponse
        };

        client.InvokeAsync(request,
            (response) =>
            {
                if (response.Exception == null)
                {
                    if (response.Response.StatusCode == 200)
                    {
                        var payload = Encoding.ASCII.GetString(response.Response.Payload.ToArray()) + "\n";
                        Debug.Log(payload);
                        networkAddress = "localhost";
                        networkPort = 7777;
                        StartClient();
                        GameControl.instance.uiController.connection.text = "connected to " + networkAddress + " " + networkPort;
                        /*
                        var connectionObj = JsonUtility.FromJson<ConnectionObject>(payload);

                        if (connectionObj.GameSessionConnectionInfo.Port == null)
                        {
                            Debug.Log($"Error in Lambda assume matchmaking failed: {payload}");
                        }
                        else
                        {
                            Debug.Log($"Connecting! IP Address: {connectionObj.GameSessionConnectionInfo.IpAddress} Port: {connectionObj.GameSessionConnectionInfo.Port}");
                            networkAddress = connectionObj.GameSessionConnectionInfo.IpAddress;
                            networkPort = Int32.Parse(connectionObj.GameSessionConnectionInfo.Port);
                            StartClient();
                        }
                        */
                    }
                }
                else
                {
                    Debug.LogError(response.Exception);
                    //uiController.SetStatusText($"Client service failed: {response.Exception}");
                }
            });
    }

    private void SetupServerAndGamelift()
    {
        // start the unet server
        networkPort = LISTEN_PORT;
        StartServer();
        print($"Server listening on port {networkPort}");

        // initialize GameLift
        print("Starting GameLift initialization.");
        var initSDKOutcome = GameLiftServerAPI.InitSDK();
        if (initSDKOutcome.Success)
        {
            isGameliftServer = true;
            var processParams = new ProcessParameters(
                (gameSession) =>
                {
                    // onStartGameSession callback
                    GameLiftServerAPI.ActivateGameSession();
                },
                (updateGameSession) =>
                {

                },
                () =>
                {
                    // onProcessTerminate callback
                    GameLiftServerAPI.ProcessEnding();
                },
                () =>
                {
                    // healthCheck callback
                    return true;
                },
                LISTEN_PORT,
                new LogParameters(new List<string>()
                {
                    "/local/game/logs/myserver.log"
                })
            );
            var processReadyOutcome = GameLiftServerAPI.ProcessReady(processParams);
            if (processReadyOutcome.Success)
            {
                print("GameLift process ready.");
            }
            else
            {
                print($"GameLift: Process ready failure - {processReadyOutcome.Error.ToString()}.");
            }
        }
        else
        {
            print($"GameLift: InitSDK failure - {initSDKOutcome.Error.ToString()}.");
        }
    }

    private void OnApplicationQuit()
    {
        if (isHeadlessServer)
        {
            TerminateSession();
        }
    }

    public void TerminateSession()
    {
        Debug.Log("** TerminateSession Requested **");
        if (isGameliftServer)
        {
            GameLiftServerAPI.TerminateGameSession();
            GameLiftServerAPI.ProcessEnding();
        }
        Debug.Log("** Process Exit **");
        Application.Quit();
    }

}
