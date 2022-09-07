using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class ClientMovement : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject nextClientPrefab;

    private RoundPopUp popUp;

    private Vector3 targetPossition;
    private Vector3 targetRotation;
    
    void Start()
    {
        popUp = FindObjectOfType<RoundPopUp>();
        animator.SetBool("isWalking", true);
        transform.position = new Vector3(-3.35f, 0f, 3.75f);
        targetPossition = new Vector3(-3.35f, 0, 5.4f);
        targetRotation = new Vector3(0, 90, 0);
    }

    private void FixedUpdate()
    {
        if (transform.position != targetPossition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPossition, 1f * Time.deltaTime);
            animator.SetBool("isWalking", true);
        }
        else if (transform.position == targetPossition)
        {
            animator.SetBool("isWalking", false);
            transform.rotation = Quaternion.Lerp(transform.rotation,
                    Quaternion.Euler(new Vector3(transform.rotation.x, targetRotation.y, transform.rotation.z)),
                    5f * Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.transform.gameObject.CompareTag("Client"))
                {
                    popUp.PopIn();
                }
            }
        }
    }

    public void CustomerServed() => StartCoroutine(ChangeCustomer());
    private IEnumerator ChangeCustomer()
    {
        targetRotation = new Vector3(0, 180, 0);
        yield return new WaitForSeconds(0.5f);
        targetPossition = new Vector3(-3.35f, 0, 3.4f);
        yield return new WaitForSeconds(2f);
        if(nextClientPrefab != null)
            Instantiate(nextClientPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
