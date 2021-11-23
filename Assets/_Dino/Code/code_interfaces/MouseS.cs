using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseS : MonoBehaviour
{
    [Header("Slider")]
    [SerializeField] private Slider sliderMouse;
    [SerializeField] private FloatVariables speedMouse;
    [Header("Toggle")] 
    [SerializeField] private Toggle toggleUI;

    [SerializeField] private BoolVariables boolY;
    
    
    void Start()
    {
        sliderMouse.value = speedMouse.value;
        toggleUI.isOn = boolY.value;
    }
}
