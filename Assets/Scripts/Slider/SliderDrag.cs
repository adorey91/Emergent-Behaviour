using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SliderDrag : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private Flock flock;
    [SerializeField] private string spawnType;

    public void OnPointerUp(PointerEventData eventData)
    {
        int value = (int)gameObject.GetComponent<Slider>().value;
        flock.UpdateFishCount(value);
    }

    public void UpdateText(TMP_Text spawnAmt)
    {
        spawnAmt.text = spawnType + " (" + (int)gameObject.GetComponent<Slider>().value + ")";
    }

}