using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Ingridient : MonoBehaviour
{
    [SerializeField] private Transform blenderDropPosition;
    
    public string ingridientName;
    
    public Color ingridientColor;

    private Vector3 startingScale;

    void OnEnable()
    {
        if (startingScale == Vector3.zero)
            startingScale = gameObject.transform.localScale;
        gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        gameObject.transform.DOScale(startingScale,  0.5f).SetEase(Ease.InBounce);
    }

    public void Disable()
    {
        if (!transform.GetComponent<Rigidbody>())
        {
            transform.DOScale(new Vector3(.1f, .1f, .1f), 0.5f).SetEase(Ease.OutBounce).OnComplete(
                () => this.gameObject.SetActive(false));
            transform.rotation = Quaternion.identity;
            if (ingridientName == "Cherry")
                transform.DORotate(new Vector3(0f, 90f, 0f), 0);
            if (ingridientName == "Banana")
                transform.DORotate(new Vector3(0f, 20f, 0f), 0);
        }
        else StartCoroutine(DisableLater());
    }

    public void JumpIntoBlender()
    {
        if(ingridientName == "Banana")
            transform.DORotate(new Vector3(90f, 0f, 0f), 0.5f);
        transform.DOJump(new Vector3(-0.6f, 1.6f, 6.05f), 0.25f, 1, 0.5f).OnComplete(
            () => transform.AddComponent<Rigidbody>());
    }

    IEnumerator DisableLater()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject.GetComponent<Rigidbody>());
        Disable();
    }
    
}
