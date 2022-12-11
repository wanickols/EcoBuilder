using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassAI : MonoBehaviour
{
    private bool eatAllowed = true;
    public EntityProfile profile;
    protected FieldOfView fov;


    protected void Start() 
    {
        fov = GetComponent<FieldOfView>();
        Entity entity = GetComponent<Entity>();
        fov.OnMultiply += entity.Fov_OnMultiply;
    }

    // Start is called before the first frame update
    IEnumerator Die()
    {
        //Play eaten sounds
        yield return new WaitForSeconds(1);
        
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
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
