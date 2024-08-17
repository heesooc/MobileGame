using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveAbility : MonoBehaviour
{
    public float moveSpeed = 5f;
    public VariableJoystick variableJoystick;
    private Rigidbody rb;
    Animator _animator;

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

        float speedValue = move.magnitude > 0 ? 1f : 0f;

        _animator.SetFloat("Horizontal", Mathf.Lerp(_animator.GetFloat("Horizontal"), moveX, Time.deltaTime * 8));
        _animator.SetFloat("Vertical", Mathf.Lerp(_animator.GetFloat("Vertical"), moveZ, Time.deltaTime * 8));
        _animator.SetFloat("Speed", Mathf.Lerp(_animator.GetFloat("Speed"), speedValue, Time.deltaTime * 8));

        rb.MovePosition(transform.position + move * moveSpeed * Time.deltaTime);
    }
}