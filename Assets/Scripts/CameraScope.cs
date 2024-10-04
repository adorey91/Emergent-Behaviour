using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScope : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Slider cameraSlider;

    private void Start()
    {
        cameraSlider.value = mainCamera.orthographicSize;    
    }

    public void ChangeCamera()
    {
        mainCamera.orthographicSize = cameraSlider.value;
    }
}
