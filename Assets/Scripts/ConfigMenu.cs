using System;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class ConfigMenu : MonoBehaviour {
    public GameObject ConfigMenuPanel;
    public Dropdown cboDifficulty;
    public Dropdown cboInputMode;
    public Slider sldAccelerationFactor;

    public void Start()
    {
        ShowConfigMenu();
    }

    public void ShowConfigMenu()
    {
        //set values of controls in ConfigMenuPanel to those in ConfigGlobal
        sldAccelerationFactor.value = (float)ConfigGlobal.AccelDiplacementFactor;
        cboDifficulty.value = (int)ConfigGlobal.DifficultyLevel;
        cboInputMode.value = (int)ConfigGlobal.InputMode;

        //show ConfigMenuPanel
        ConfigMenuPanel.SetActive(true);
    }

    public void HideConfigMenu()
    {
        ConfigMenuPanel.SetActive(false);
    }

    public void SaveAndClose()
    {
        //Save values
        ConfigGlobal.AccelDiplacementFactor = sldAccelerationFactor.value;
        ConfigGlobal.DifficultyLevel = (DifficultyLevel)cboDifficulty.value;
        ConfigGlobal.InputMode = (InputMode)cboInputMode.value;

        HideConfigMenu();
    }
    
}
