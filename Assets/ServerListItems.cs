using UnityEngine;
using System.Collections.Generic;
using Mirror;

public class ServerListItems : MonoBehaviour
{
    [SerializeField] List<GameObject> items = new List<GameObject>();

    [Server]
    public void SyncServerItems(NetworkConnectionToClient conn = null)
    {
        foreach (var item in items)
        {
            NetworkServer.Spawn(item, conn);
        }
    }
}
