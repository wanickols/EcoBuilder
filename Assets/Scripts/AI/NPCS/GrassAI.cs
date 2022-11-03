using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassAI : MonoBehaviour
{

    public EntityProfile profile;
    // Start is called before the first frame update
    IEnumerator Die()
    {
        //Play eaten sounds
        yield return new WaitForSeconds(1);
        
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
            //Check for a match with the specific tag on any GameObject that collides with your GameObject
            if (collision.gameObject.tag == "Herbavore")
            {
            collision.gameObject.SendMessage("Eat", profile.nutritionalValue);
                StartCoroutine(Die());
            }
    }

}
