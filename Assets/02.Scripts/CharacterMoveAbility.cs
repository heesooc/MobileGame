using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveAbility : MonoBehaviour
{
    public float moveSpeed = 5f;
    public VariableJoystick variableJoystick;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveX = variableJoystick.Horizontal;
        float moveZ = variableJoystick.Vertical;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        rb.MovePosition(transform.position + move * moveSpeed * Time.deltaTime);
    }
}