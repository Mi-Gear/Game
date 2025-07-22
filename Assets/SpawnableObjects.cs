using UnityEngine;
using Mirror;
using System.Collections.Generic;

public class SpawnableObjects : NetworkBehaviour
{
    [SerializeField] private List<GameObject> list;

    void Start()
    {
    }

    public void Spawn()
    {
        if (!NetworkServer.active) { return; }
        for (int i = 0; i < list.Count; i++)
        {
            GameObject go = list[i];
            go = Instantiate(go, FindAnyObjectByType<Canvas>().transform);
            NetworkServer.Spawn(go);
        }
    }

    

}
