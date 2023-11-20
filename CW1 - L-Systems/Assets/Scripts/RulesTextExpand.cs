using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RulesTextExpand : MonoBehaviour
{
    [SerializeField] private TMP_InputField ruleInput;
    [SerializeField] private TextMeshProUGUI expandedRules;

    // Update is called once per frame
    void Update()
    {
        expandedRules.text = ruleInput.text;
    }
}
