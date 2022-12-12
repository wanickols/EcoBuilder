using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyAI : BasicAIMovement
{
    private bool eatAllowed = true;
    private new Rigidbody rigidbody;
    [SerializeField] private string predatorTag;
    

    // Start is called before the first frame update
    new protected void Start()
    {
        base.Start();

        fov.OnMultiply += entity.Fov_OnMultiply;
        rigidbody = GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    new protected void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!IsOwner)
            return;

        if (!eatAllowed)
            return;

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == predatorTag)
        {
            eatAllowed = false;
            collision.gameObject.SendMessage("Eat", entity.holder.profile.nutritionalValue);
            entity.onDestroyServerRpc();
        }
    }

}
