using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    public enum CustomerState
    {
        WaitSalesBox,
        WaitPOS,
        GoOut,
        WaitTable,
    }

    private CustomerState _state = CustomerState.WaitSalesBox;

    private Animator _animatior;
    public NavMeshAgent Agent;

    private void Start()
    {
        
    }

    private void Update()
    {
        switch (_state)
        {
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

            
        }
    }

    private void WaitSalesBox()
    {

    }

    private void WaitPOS()
    {

    }
}
