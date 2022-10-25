using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class settingPage : MonoBehaviour
{
    public Button applyButton;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public Toggle fpsToggle;
    public Slider fpsSlider;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

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
                Debug.Log("Error: Unknown resolution value");
                break;
        }
    }
}
