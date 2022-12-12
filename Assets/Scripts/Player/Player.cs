using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Player : NetworkBehaviour
{

    [SerializeField] private GameObject myPrefab;
    [SerializeField] private Signaler playerPlanted;

    //Spawn grass in a grid
    //Grass prefab
    //Spawn
    //Interact

    private void Update()
    {
        if (!IsOwner)
            return;

        if (Input.GetButtonDown("Interact")) 
        {
            SpawnServerRpc();
        }
    }

    [ServerRpc]
    private void SpawnServerRpc()
    {

        //Make grid 
     
        GameObject go = Instantiate(myPrefab, this.transform.position, Quaternion.identity);
        go.GetComponent<NetworkObject>().SpawnWithOwnership(OwnerClientId);

        playerPlanted.Raise();
    }

}
