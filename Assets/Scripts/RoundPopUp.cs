using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Color = System.Drawing.Color;

public class RoundPopUp : MonoBehaviour
{

    [SerializeField] private Image bgImage;
    [SerializeField] private Image levelImage;
    [SerializeField] private Text accuracyText;
    [SerializeField] private ColorComparator comparator;

    private int scalingFrames = 0;
    private float scaleStep = 0.1f;
    private void Start()
    {
        bgImage.rectTransform.localScale = new Vector3(0, 0, 0);
    }

    public void Update()
    {
        if(scalingFrames > 0)
        {
            Vector3 currentScale = bgImage.rectTransform.localScale;
            currentScale = Vector3.Lerp(
            currentScale, new Vector3(
                currentScale.x + scaleStep, currentScale.y + scaleStep, currentScale.z + scaleStep), 20 * Time.deltaTime);
            bgImage.rectTransform.localScale = currentScale;
            scalingFrames--;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
            PopIn();
    }
    
    public void PopIn()
    {
        scaleStep = 0.1f;
        scalingFrames = 60;
    }

    public void PopOut()
    {
        scaleStep = -0.1f;
        scalingFrames = 60;
    }
}
