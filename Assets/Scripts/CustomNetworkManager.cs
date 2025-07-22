using UnityEngine;
using Mirror;
using Steamworks;
using System.Collections;

public class CustomNetworkManager : NetworkManager
{
    NetworkManager netManager;
    override public void Start()
    {
        netManager = GetComponent<NetworkManager>();
    }



    public override void OnServerSceneChanged(string newSceneName)
    {
        if (NetworkServer.active)
        {
            SpawnableObjects so = FindAnyObjectByType<SpawnableObjects>();
            so.Spawn();
        }
    }



}
