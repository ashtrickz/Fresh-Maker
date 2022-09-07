using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RoundPopUp : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Image bgImage;
    [SerializeField] private Image levelImage;
    [Header("Accuracy")] 
    [SerializeField] private Image textBackground;
    [SerializeField] private TMP_Text accuracyText;
    [Space]
    [SerializeField] private Color[] roundColors;
    [Space]
    [SerializeField] private ColorComparator comparator;

    private int scalingFrames = 0;
    private float scaleStep = 0.1f;
    private int speed = 40;

    private void Start()
    {
        textBackground.DOFade(0, 0);
        accuracyText.DOFade(0, 0);
    }
    
    public void Update()
    {
        if (scalingFrames > 0 &&
            bgImage.rectTransform.localScale.x + scaleStep < 1 &&
            bgImage.rectTransform.localScale.x + scaleStep > -0.1f)
        {
            Vector3 currentScale = bgImage.rectTransform.localScale;
            currentScale = Vector3.Lerp(
                currentScale, new Vector3(
                    currentScale.x + scaleStep, currentScale.y + scaleStep, currentScale.z + scaleStep),
                speed * Time.deltaTime);
            bgImage.rectTransform.localScale = currentScale;
            scalingFrames--;
        }
    }

    public void PopIn() => StartCoroutine(PopUpRotation());

    public void ShowAccuracy(int accuracy) => StartCoroutine(AccuracyRotation(accuracy));

    private IEnumerator PopUpRotation()
    {
        levelImage.color = new Color(
            roundColors[comparator.level - 1].r,
            roundColors[comparator.level - 1].g,
            roundColors[comparator.level - 1].b);
        if (bgImage.rectTransform.localScale == new Vector3(0, 0, 0))
        {
            bgImage.rectTransform.localScale = new Vector3(0, 0, 0);
            speed = 40;
            scaleStep = 0.1f;
            scalingFrames = 50;
            yield return new WaitForSeconds(3f);
            speed = 55;
            scaleStep = -0.1f;
            scalingFrames = 50;
            yield return new WaitForSeconds(0.3f);
            bgImage.rectTransform.localScale = new Vector3(0, 0, 0);
        }
    }

    private IEnumerator AccuracyRotation(int accuracy)
    {
        accuracyText.text = "Accuracy:\n" + accuracy + "%";
        textBackground.DOFade(1, .5f);
        accuracyText.DOFade(1, 1f);
        yield return new WaitForSeconds(3f);
        textBackground.DOFade(0, 1f);
        accuracyText.DOFade(0, .5f);
    }
}
