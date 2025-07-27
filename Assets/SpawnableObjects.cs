using UnityEngine;
using Mirror;
using System.Collections.Generic;

public class SpawnableObjects : NetworkBehaviour
{
    [SerializeField] private List<GameObject> list;
    [SerializeField] private List<GameObject> list1;

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
        GameObject go1 = list1[0];
        go1 = Instantiate(go1,new Vector3(0,0,0),Quaternion.identity);
        NetworkServer.Spawn(go1);
    }

    

}
