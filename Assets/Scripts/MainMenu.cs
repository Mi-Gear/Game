using Mirror;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private SteamLobby steamLobby;
    [SerializeField] private NetworkManager netManager;

    public void Quit()
    {
        Application.Quit();
    }
    public void CreateLobby()
    {
        steamLobby.HostLobby();
            
        
    }
}
