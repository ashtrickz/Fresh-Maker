using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ClientMovement : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject nextClientPrefab;

    private RoundPopUp popUp;

    private bool isComplete;

    void Start()
    {
        isComplete = false;
        popUp = FindObjectOfType<RoundPopUp>();
        Debug.Log(popUp);
        animator.SetBool("isWalking", true);
        transform.position = new Vector3(-3.35f, 0f, 3.75f);
        StartWalking();
    }

    void Update()
    {

    }

    private void StartWalking()
    {
        
    }

    public void WalkAway()
    {
        isComplete = true;
    }
}
