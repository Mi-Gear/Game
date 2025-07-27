using UnityEngine;
using Mirror;
using Steamworks;
using System.Collections.Generic;


public class CustomNetworkManager : NetworkManager
{
    NetworkManager netManager;
    public ulong lobbyID;
    public List<PlayerInstance> players;

    public int avID = 0;
    override public void Start()
    {
        netManager = GetComponent<NetworkManager>();
    }


    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        PlayerInstance p = new();
        p.playerID = (byte)avID;
        avID++;
        p.steamID = (ulong)SteamMatchmaking.GetLobbyMemberByIndex((CSteamID)lobbyID, p.playerID);
        players.Add(p);
    }




}
