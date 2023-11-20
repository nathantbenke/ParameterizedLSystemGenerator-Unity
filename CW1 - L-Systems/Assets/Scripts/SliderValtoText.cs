using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderValtoText : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI sliderVal;
    private float simplifiedValue;
    public float decimalPoint;

    // Update is called once per frame
    void Update()
    {
        //Rounds float value to single decimal point. (i.e. 10.1253 -> 10.1)
        simplifiedValue = (float)Mathf.Round(slider.value * Mathf.Pow(10f, decimalPoint)) / Mathf.Pow(10f, decimalPoint);
        sliderVal.text = simplifiedValue.ToString();
    }
}
