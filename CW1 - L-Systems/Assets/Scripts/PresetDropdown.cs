using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PresetDropdown : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private Slider angleSlider;
    [SerializeField] private Slider iterationSlider;
    [SerializeField] private TMP_InputField axiom;
    [SerializeField] private TMP_InputField rules;
    private float dragOffset = 2500;

    //[SerializeField] private char axiom;

    public int GetDropdownValue()
    {
        int currentPresetIndex = dropdown.value;
        return currentPresetIndex;
    }

    // Update is called once per frame
    void Update()
    {

        Camera.main.GetComponent<CameraDrag>().dragOffset = dragOffset;

        //A
        //{ 'F', "F[+F]F[-F]F" },

        //B
        //{ 'F', "F[+F]F[-F]F[F]" }, //20 DEG, N = 5

        //C
        //{ 'F', "FF-[-F+F+F]+[+F-F-F]" }, //22.5, N=4

        //D
        //{ 'X', "F[+X]F[-X]+X"}, //20, N=7
        //{ 'F', "FF" },

        //E
        //{ 'X', "F[+X][-X]FX" }, //25.7, N=7
        //{ 'F', "FF" },

        //F
        //{ 'X', "F-[[X]+X]+F[+FX]-X"}, //22.5, N=5
        //{ 'F', "FF" },


       // loadPreset();

    }


    public void loadPreset()
    {
        switch (GetDropdownValue())
        {

            //A
            //{ 'F', "F[+F]F[-F]F" }, //25.7 DEG, N=5
            case 0:
                iterationSlider.maxValue = 7;
                iterationSlider.value = 5;
                angleSlider.value = 25.7f;
                axiom.text = "F";
                rules.text = "{ 'F', \"F[+F]F[-F]F\" },";
                dragOffset = 9500;
                break;

            //B
            //{ 'F', "F[+F]F[-F]F[F]" }, //20 DEG, N = 5
            case 1:
                iterationSlider.maxValue = 7;
                iterationSlider.value = 5;
                angleSlider.value = 20;
                axiom.text = "F";
                rules.text = "{ 'F', \"F[+F]F[-F][F]\" },";
                dragOffset = 1300;

                break;

            //C
            //{ 'F', "FF-[-F+F+F]+[+F-F-F]" }, //22.5, N=4
            case 2:
                iterationSlider.maxValue = 5;
                iterationSlider.value = 4;
                angleSlider.value = 22.5f;
                axiom.text = "F";
                rules.text = "{ 'F', \"FF-[-F+F+F]+[+F-F-F]\" },";
                dragOffset = 650;
                break;


            //D
            //{ 'X', "F[+X]F[-X]+X"}, //20, N=7
            //{ 'F', "FF" },
            case 3:
                iterationSlider.maxValue = 9;
                iterationSlider.value = 7;
                angleSlider.value = 20;
                axiom.text = "X";
                rules.text = "{ 'X', \"F[+X]F[-X]+X\" }, { 'F', \"FF\" },";
                dragOffset = 5000;
                break;


            //E
            //{ 'X', "F[+X][-X]FX" }, //25.7, N=7
            //{ 'F', "FF" },
            case 4:
                iterationSlider.maxValue = 9;
                iterationSlider.value = 7;
                angleSlider.value = 25.7f;
                axiom.text = "X";
                rules.text = "{ 'X', \"F[+X][-X]FX\" }, { 'F', \"FF\" },";
                dragOffset = 5000;
                break;
            //F
            //{ 'X', "F-[[X]+X]+F[+FX]-X"}, //22.5, N=5
            //{ 'F', "FF" },
            case 5:
                iterationSlider.maxValue = 7;
                iterationSlider.value = 5;
                angleSlider.value = 22.5f;
                axiom.text = "X";
                rules.text = "{ 'X', \"F-[[X]+X]+F[+FX]-X\" }, { 'F', \"FF\" },";
                dragOffset = 3000;
                break;

            //Custom Tree 1: Dragon Curve
            // { 'X', "X+YF+" }, { 'Y', "-FX-Y" } 
            case 6:
                iterationSlider.maxValue = 14;
                iterationSlider.value = 11;
                angleSlider.value = 90f;
                axiom.text = "FX";
                rules.text = "{ 'X', \"X+YF+\" }, { 'Y', \"-FX-Y\" }";
                dragOffset = 1500;
                break;


            //Custom Tree 2: 
            // { 'F', "F[+FF][-FF]F[-F][+F]F" }
            case 7:
                iterationSlider.maxValue = 5;
                iterationSlider.value = 5;
                angleSlider.value = 35;
                axiom.text = "F";
                rules.text = "{ 'F', \"F[+FF][-FF]F[-F][+F]F\" }";
                dragOffset = 2500;
                break;

            //Fully Custom Tree
            case 8:
                iterationSlider.maxValue = 10;
                iterationSlider.value = 1;
                angleSlider.value = 45;
                axiom.text = "";
                rules.text = "";
                dragOffset = 10000;
                break;

            default:
                break;
        };

    }
}
