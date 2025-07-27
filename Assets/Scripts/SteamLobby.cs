using Mirror;
using Steamworks;
using UnityEngine;

public class SteamLobby : MonoBehaviour
{
    [SerializeField] private CustomNetworkManager netManager;
    private const string HostAddressKey = "HostAddressKey";
    public Callback<LobbyCreated_t> lobbyCreated;
    public Callback<GameLobbyJoinRequested_t> joinRequest;
    public Callback<LobbyEnter_t> enterLobby;
    void Start()
    {
        netManager = GetComponent<CustomNetworkManager>();
        if (!SteamManager.Initialized)  return; 

        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        joinRequest = Callback<GameLobbyJoinRequested_t>.Create(OnLobbyJoinRequested);
        enterLobby = Callback<LobbyEnter_t>.Create(OnEnterLobby);

    }
    public void HostLobby()
    {

        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, netManager.maxConnections);
    }
    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if (callback.m_eResult != EResult.k_EResultOK)  return;
        netManager.StartHost();
        
        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey, SteamUser.GetSteamID().ToString());
        NetworkManager.singleton.ServerChangeScene("Lobby");

    }
    private void OnLobbyJoinRequested(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    private void OnEnterLobby(LobbyEnter_t callback)
    {
        if (NetworkServer.active)
        {
            netManager.lobbyID = callback.m_ulSteamIDLobby;
            return;
        }
        string HostAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey);
        netManager.networkAddress = HostAddress;
        netManager.StartClient();
        NetworkClient.Ready();
    }


}
