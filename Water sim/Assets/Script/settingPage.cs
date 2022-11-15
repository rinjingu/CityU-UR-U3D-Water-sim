using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class settingPage : MonoBehaviour
{
    public profileIO pio;
    public Button applyButton;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public Toggle fpsToggle;
    public Slider fpsSlider;

    // Start is called before the first frame update
    void Start()
    {
        pio = new profileIO();
        //obtain setting file or create one with default setting 
        pio.findOrBuild("setting.ini");

        //load settings from the file to memory and apply them to the game
        resolutionDropdown.value = pio.getFromCFG<int>("Setting", "ResolutionValue");
        fullscreenToggle.isOn = pio.getFromCFG<bool>("Setting","Fullscreen");
        fpsToggle.isOn = pio.getFromCFG<bool>("Setting", "CustomFPS");
        fpsSlider.value = pio.getFromCFG<int>("Setting", "FPSValue");
    }

    public void applySetting()
    {


        int fps;

        if (fpsToggle.isOn)
        {
            fps = (int)fpsSlider.value;
        }
        else
        {
            fps = 0;
        }

        switch (resolutionDropdown.value)
        {
            case 0:
                Screen.SetResolution(1920, 1080, fullscreenToggle.isOn, fps);
                break;
            case 1:
                Screen.SetResolution(2560, 1440, fullscreenToggle.isOn, fps);
                break;
            default:
                Debug.LogError("Error: Unknown resolution value");
                break;
        }

        // load settings from game to memory 
        pio.setToCFG<int>("Setting", "ResolutionValue", resolutionDropdown.value);
        pio.setToCFG<bool>("Setting", "Fullscreen", fullscreenToggle.isOn);
        pio.setToCFG<bool>("Setting","CustomFPS", fpsToggle.isOn);
        pio.setToCFG<int>("Setting", "FPSValue", fps);
        // save current setting in memory to setting.ini file
        pio.saveCFG("setting.ini");
    }
}
