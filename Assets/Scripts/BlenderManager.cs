using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class BlenderManager : MonoBehaviour
{
    //Небольшой godobject образовался, раскидать по полочкам времени не хватило :(
    [SerializeField] private List<Ingridient> ingridientList;
    [SerializeField] private IngridientsPool pool;
    [SerializeField] private ColorComparator comparator;
    
    [SerializeField] private GameObject blenderCap;
    [SerializeField] private Transform[] capPosition;
    [Space]
    [SerializeField] private GameObject mixer;
    [Space]
    [SerializeField] private Renderer liquid;
    [SerializeField] private Renderer[] cupLiquid;

    [HideInInspector] public bool isReady = false;
    [HideInInspector] public bool isMixing = false;
    
    private float targetFillValue = 0.2f;
    private float currentFillValue = 0;
    
    private float cupTargetFillValue = 0.2f;
    private float cupCurrentFillValue = 0;
    
    private Color updatedColor = Color.grey;
    
    public void AddIngridient(GameObject ingridient) =>
        ingridientList.Add(ingridient.GetComponent<Ingridient>());

        private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                //Adding an ingridients
                if (hit.transform.gameObject.CompareTag("Ingridient"))
                {
                    Ingridient obj = hit.transform.gameObject.GetComponent<Ingridient>();
                    if (ingridientList.Count < 3)
                    {
                        obj.JumpIntoBlender();
                        AddIngridient(obj.gameObject);
                    }
                    else throw new Exception($"The list of Ingridients is full! {ingridientList.Count}");
                }
                //Completing fresh
                if (hit.transform.gameObject.CompareTag("Blender"))
                {
                    if(ingridientList.Count < 1)
                        throw new Exception("There is no ingridients in blender!");
                    else
                        StartCoroutine(MakingFresh());
                }
                //Ending the round
                if (hit.transform.gameObject.CompareTag("CustomerCup"))
                    if (isReady)
                        RoundEnd();
            }
        }
        
        //Liquid Fill Manager
        if (isMixing || isReady)
        {
            pool.DisablePool();
            currentFillValue = Mathf.Lerp(currentFillValue, targetFillValue, 2f * Time.deltaTime);
            liquid.material.SetFloat("_Fill", currentFillValue);
        }

        if (isReady)
        {
            targetFillValue = -0.1f;
            cupTargetFillValue = 0.2f;
            cupCurrentFillValue = Mathf.Lerp(cupCurrentFillValue, cupTargetFillValue, 2f * Time.deltaTime);
            foreach (var cup in cupLiquid)
                cup.material.SetFloat("_Fill", cupCurrentFillValue);
        }
        else if (cupCurrentFillValue > 0)
        {
            cupCurrentFillValue = Mathf.Lerp(cupCurrentFillValue, cupTargetFillValue, 2f * Time.deltaTime);
            foreach (var cup in cupLiquid)
                cup.material.SetFloat("_Fill", cupCurrentFillValue);
        }
    }

    private void ResetList() => ingridientList.Clear();

    private void UpdateColor()
    {
        updatedColor = new Color(0, 0, 0, 0);
        foreach (var c in ingridientList)
            updatedColor += c.ingridientColor;
        updatedColor /= ingridientList.Count;
        Debug.Log(updatedColor);
        ChangeColor();
    }

    private void ChangeColor()
    {
        targetFillValue = 0.2f;
        liquid.material.SetColor("_LiquidColor", updatedColor);
        liquid.material.SetColor("_SurfaceColor", updatedColor);
        liquid.material.SetColor("_FresnelColor", updatedColor);
        
        foreach (var cup in cupLiquid)
        {
            cup.material.SetColor("_LiquidColor", updatedColor);
            cup.material.SetColor("_SurfaceColor", updatedColor);
            cup.material.SetColor("_FresnelColor", updatedColor);
        }
    }

    private void OpenCap() =>
        blenderCap.transform.DOJump(capPosition[0].position, 0.5f, 1, 0.5f).OnComplete(
            () => isMixing = false);

    private void CloseCap() =>
        blenderCap.transform.DOJump(capPosition[1].position, 0.5f, 1, 0.5f).OnComplete(
            () => isMixing=true);

    IEnumerator MakingFresh()
    {
        pool.DisablePool();
        CloseCap();
        UpdateColor();
        yield return new WaitForSeconds(0.5f);
        mixer.transform.DOShakeRotation(3f, 1, 10, 35f, true);
        yield return new WaitForSeconds(3f);
        OpenCap();
        ResetList();
        isReady = true;
    }
    
    private void RoundEnd()
    {
        comparator.CompareColors(updatedColor);
        isReady = false;
        cupTargetFillValue = -0.1f;
    }
    
}
