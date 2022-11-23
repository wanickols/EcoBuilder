using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyAI : BasicAIMovement
{

    private new Rigidbody rigidbody;
  

    // Start is called before the first frame update
    new protected void Start()
    {
        base.Start();
        fov.targetMask = LayerMask.GetMask("Grass");
        rigidbody = GetComponent<Rigidbody>();  
    }

    // Update is called once per frame
    new protected void Update()
    {
        base.Update();
    }
}
