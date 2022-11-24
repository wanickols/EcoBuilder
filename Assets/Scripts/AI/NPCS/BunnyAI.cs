using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyAI : BasicAIMovement
{

    private new Rigidbody rigidbody;
    Entity entity;
  

    // Start is called before the first frame update
    new protected void Start()
    {
        base.Start();
        fov.targetMask = LayerMask.GetMask("Grass");
        entity = GetComponent<Entity>();
        fov.OnMultiply += entity.Fov_OnMultiply;

        rigidbody = GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    new protected void Update()
    {
        base.Update();
    }
}
