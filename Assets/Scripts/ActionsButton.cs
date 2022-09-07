using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ActionsButton : MonoBehaviour
{

    [SerializeField] private Button toggleButton;
    [SerializeField] private Button[] otherButtons;
    [Space]
    [SerializeField] private RectTransform[] positions;
    [Space]
    [SerializeField] private Image tutorialImage;
    
    public bool isClosed = true;
    public bool tutorialOn = false;

    public void OpenActions()
    {
        if (isClosed)
        {
            foreach (var button in otherButtons)
            {
                button.image.rectTransform.DOMove(new Vector2(
                    positions[1].position.x,
                    button.image.rectTransform.position.y), Random.Range(0.5f, 1f)).SetEase(Ease.OutFlash);
            }
            isClosed = false;
        }
        else
        {
            foreach (var button in otherButtons)
            {
                button.image.rectTransform.DOMove(new Vector2(
                    positions[0].position.x,
                    button.image.rectTransform.position.y), Random.Range(0.5f, 1f)).SetEase(Ease.InFlash);
            }
            isClosed = true;
        }
    }

    public void Tutorial()
    {
        if (!tutorialOn)
        {
            tutorialImage.DOFade(0, 0);
            tutorialImage.gameObject.SetActive(true);
            tutorialImage.DOFade(1, 0.5f);
            tutorialOn = true;
        }
        else if (tutorialOn)
        {
            tutorialOn = false;
            tutorialImage.DOFade(0, 0.5f).OnComplete(
                () => tutorialImage.gameObject.SetActive(false));
        }
    }

    public void Restart() => SceneManager.LoadScene("GameScene");

}
