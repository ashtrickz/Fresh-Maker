using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private Camera camera;

    [SerializeField] private Transform[] cameraPosition;

    [SerializeField] private RoundPopUp popUp;
    
    private bool cameraSwitched = true;

    void Start()
    {
        DOTween.SetTweensCapacity(5000, 50);
        SwitchCamera();
    }
    
    public void SwitchCamera()
    {
        if (cameraSwitched)
        {
            camera.transform.DOMove(cameraPosition[0].position, 1).SetEase(Ease.InOutQuart);
            camera.transform.DORotate(new Vector3(5, -90, 0), 1);
            cameraSwitched = false;
            Debug.Log("Moving towards Customer Position");
            
        }
        else if (!cameraSwitched)
        {
            camera.transform.DOMove(cameraPosition[1].position, 1).SetEase(Ease.InOutQuart);
            camera.transform.DORotate(new Vector3(15, 0, 0), 1);
            cameraSwitched = true;
            Debug.Log("Moving towards Cooking Position");
        }
    }
    
}
