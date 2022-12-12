using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class GrassAI : NetworkBehaviour
{
    private bool eatAllowed = true;
    public EntityProfile profile;
    [SerializeField] protected FieldOfView fov;
    [SerializeField] private Entity entity;


    protected void Start()
    {
        if (!IsOwner)
            return;

        if (!fov)
            fov = GetComponent<FieldOfView>();

        if (!entity)
            entity = GetComponent<Entity>();

        fov.OnMultiply += entity.Fov_OnMultiply;
    }

    // Start is called before the first frame update
    IEnumerator Die()
    {
        //Play eaten sounds
        yield return new WaitForSeconds(1);

        entity.onDestroyServerRpc();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!IsOwner)
            return;

        if (!eatAllowed)
            return;

            //Check for a match with the specific tag on any GameObject that collides with your GameObject
            if (collision.gameObject.tag == "Herbavore")
            {
                eatAllowed = false;
                collision.gameObject.SendMessage("Eat", profile.nutritionalValue);
                StartCoroutine(Die());
            }
    }

}
