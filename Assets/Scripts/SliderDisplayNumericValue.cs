using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderDisplayNumericValue : MonoBehaviour {
    public Text target;

    public void onValueChanged(float newValue)
    {
        target.text = newValue.ToString("N1");
    }
}
