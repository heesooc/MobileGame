using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAbility : MonoBehaviour
{
    public float moveSpeed = 5f;
    public VariableJoystick variableJoystick;
    private Rigidbody rb;
    Animator _animator;

    private float lastMoveX = 0f;
    private float lastMoveZ = 0f;
    private float inputThreshold = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float moveX = variableJoystick.Horizontal;
        float moveZ = variableJoystick.Vertical;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        if (Mathf.Abs(moveX) > inputThreshold || Mathf.Abs(moveZ) > inputThreshold)
        {
            lastMoveX = moveX;
            lastMoveZ = moveZ;
        }


        float speedValue = (Mathf.Abs(moveX) > inputThreshold || Mathf.Abs(moveZ) > inputThreshold) ? 1f : 0f;

        _animator.SetFloat("Horizontal", Mathf.Lerp(_animator.GetFloat("Horizontal"), speedValue > 0 ? moveX : lastMoveX, Time.deltaTime * 8));
        _animator.SetFloat("Vertical", Mathf.Lerp(_animator.GetFloat("Vertical"), speedValue > 0 ? moveZ : lastMoveZ, Time.deltaTime * 8));
        _animator.SetFloat("Speed", Mathf.Lerp(_animator.GetFloat("Speed"), speedValue, Time.deltaTime * 8));

        rb.MovePosition(rb.position + move * moveSpeed * Time.deltaTime);
    }
}