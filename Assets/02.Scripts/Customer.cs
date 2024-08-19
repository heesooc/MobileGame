using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    public enum CustomerState
    {
        ComeEntrance,
        WaitSalesBox,
        WaitPOS,
        GoOut,
        WaitTable,
    }

    private CustomerState _state = CustomerState.ComeEntrance;

    private Animator _animatior;
    public NavMeshAgent Agent;

    private PlayerInteractionAbility _playerInteractionAbility;

    private Vector3 _startPosition;
    public Transform handPosition;

    private void Start()
    {
        Agent.speed = 7f;
        _playerInteractionAbility = FindObjectOfType<PlayerInteractionAbility>();
        _startPosition = transform.position;

    }

    private void OnTriggerEnter(Collider other)
    {
        SalesBox salesBox = other.GetComponent<SalesBox>();
        if (salesBox != null && _playerInteractionAbility != null)
        {
            StartCoroutine(_playerInteractionAbility.StackBreadCoroutine(salesBox));
        }
    }

    private void Update()
    {
        switch (_state)
        {
            case CustomerState.ComeEntrance:
                {
                    ComeEntrance();
                    break;
                }

            case CustomerState.WaitSalesBox:
                {
                    WaitSalesBox();
                    break;
                }

            case CustomerState.WaitPOS:
                {
                    WaitPOS();
                    break;
                }

            case CustomerState.GoOut:
                {
                    GoOut();
                    break;
                }

            case CustomerState.WaitTable:
                {
                    WaitTable();
                    break;
                }
        }
    }
    private void ComeEntrance()
    {

    }

    private void WaitSalesBox()
    {

    }

    private void WaitPOS()
    {

    }

    private void GoOut()
    {

    }

    private void WaitTable()
    {

    }
}
