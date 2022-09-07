using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ActionsButton : MonoBehaviour
{

    [SerializeField] private Button toggleButton;
    [SerializeField] private Button[] otherButtons;
    [Space]
    [SerializeField] private RectTransform[] positions;

    public bool isClosed = true;

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

}
