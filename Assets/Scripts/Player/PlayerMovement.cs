using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class PlayerMovement : NetworkBehaviour
{

    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private float rotationSpeed = 400f;
    [SerializeField] private Vector3 movement;

    private void Start()
    {
        movement = new Vector3(0f,0f,0f);
    }


    private void Update()
    {
        if (!IsOwner) return;

        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        movement.Set(horizontal, 0f, vertical);

        transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);

        if (movement != Vector3.zero) 
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }



}


