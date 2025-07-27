using UnityEngine;
using Mirror;
using Steamworks;

public class LobbyManager : MonoBehaviour
{
    NetworkManager netManager;
    SteamLobby sl;
    void Start()
    {
        netManager = FindAnyObjectByType<NetworkManager>();
    }


}
