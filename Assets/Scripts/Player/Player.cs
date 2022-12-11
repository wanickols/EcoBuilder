using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private GameObject myPrefab;
    [SerializeField] private Signaler playerPlanted;

    //Spawn grass in a grid
    //Grass prefab
    //Spawn
    //Interact

    private void Update()
    {
        if (Input.GetButtonDown("Interact")) 
        {
            spawn();
        }
    }

    private void spawn() 
    {

        //Make grid 
        Instantiate(myPrefab, this.transform.position, Quaternion.identity);
        playerPlanted.Raise();
    }

}
