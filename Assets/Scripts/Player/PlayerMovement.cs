using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;
    [SerializeField] float speed = 9.0f;

    [SerializeField] private Vector3 moveDirection = Vector3.zero;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterController.Move(moveDirection * speed * Time.deltaTime);
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        moveDirection = new Vector3(horizontal, 0.0f, vertical).normalized;

        if (moveDirection.magnitude >= 0.1f) 
           characterController.Move(moveDirection * speed * Time.deltaTime);
    }
}


