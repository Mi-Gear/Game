using Mirror;
using Steamworks;
using UnityEngine;

public class SteamLobby : MonoBehaviour
{
    [SerializeField] private NetworkManager netManager;
    private const string HostAddressKey = "HostAddressKey";
    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> joinRequest;
    protected Callback<LobbyEnter_t> enterLobby;
    void Start()
    {
        netManager = GetComponent<NetworkManager>();
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
        if (NetworkServer.active) return;
        string HostAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey);
        netManager.networkAddress = HostAddress;
        netManager.StartClient();
        NetworkClient.Ready();
    }


}
