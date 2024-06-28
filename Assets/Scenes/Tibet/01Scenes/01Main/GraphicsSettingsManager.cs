using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class GraphicsSettingsManager : MonoBehaviour
{
    public Camera mainCamera;

    public Button resetButton;

    public Dropdown resolutionDropdown;
    public Toggle fullScreenToggle;
    public Toggle vsyncToggle;
    public Dropdown graphicsQualityDropdown;
    public Dropdown antiAliasingDropdown;
    public Dropdown textureQualityDropdown;
    public Dropdown refreshRateDropdown;
    public Slider shadowDistanceSlider;
    public Text shadowDistanceText;
    public Dropdown softShadowQualityDropdown;

    public Toggle bloomToggle;
    public Toggle motionBlurToggle;
    public Toggle depthOfFieldToggle;

    private void Start()
    {
        // Populate dropdowns with options
        //resolutionDropdown.AddOptions(GetAvailableResolutions());
        ////graphicsQualityDropdown.AddOptions(GetAvailableQualities());
        ////antiAliasingDropdown.AddOptions(GetAvailableAntiAliasingOptions());
        //textureQualityDropdown.AddOptions(GetAvailableTextureQualities());
        //refreshRateDropdown.AddOptions(GetAvailableRefreshRatesDistinct());
        //softShadowQualityDropdown.AddOptions(GetAvailableSoftShadowQualities());

        //// Set initial dropdown values to current settings
        //resolutionDropdown.value = GetCurrentResolutionIndex();
        //fullScreenToggle.isOn = Screen.fullScreen;
        //vsyncToggle.isOn = QualitySettings.vSyncCount != 0;
        //graphicsQualityDropdown.value = QualitySettings.GetQualityLevel();
        //refreshRateDropdown.value = GetCurrentRefreshRateIndex();

        //// Set up dropdown onChange events
        //resolutionDropdown.onValueChanged.AddListener(index => SetResolution(index));
        //fullScreenToggle.onValueChanged.AddListener(value => SetFullScreen(value));
        //vsyncToggle.onValueChanged.AddListener(value => SetVSync(value));
        //graphicsQualityDropdown.onValueChanged.AddListener(index => SetGraphicsQuality(index));
        //refreshRateDropdown.onValueChanged.AddListener(index => SetRefreshRate(index));
        //resetButton.onClick.AddListener(ResetToDefaults);


    }

    private int GetCurrentResolutionIndex()
    {
        string currentResolution = Screen.width + "x" + Screen.height;
        return resolutionDropdown.options.FindIndex(option => option.text == currentResolution);
    }

    private int GetCurrentRefreshRateIndex()
    {
        string currentRefreshRate = Screen.currentResolution.refreshRate.ToString() + "Hz";
        return refreshRateDropdown.options.FindIndex(option => option.text == currentRefreshRate);
    }

    private List<string> GetAvailableResolutions()
    {
        HashSet<string> uniqueResolutions = new HashSet<string>();
        foreach (var res in Screen.resolutions)
        {
            uniqueResolutions.Add(res.width + "x" + res.height);
        }
        return uniqueResolutions.ToList();
    }

    private List<string> GetAvailableQualities()
    {
        return new List<string>(QualitySettings.names);
    }

    private List<string> GetAvailableAntiAliasingOptions()
    {
        return new List<string>() { "Disabled", "FXAA", "SMAA", "TSAA" };
    }

    private List<string> GetAvailableTextureQualities()
    {
        return new List<string>() { "Low", "Medium", "High" };
    }

    private List<string> GetAvailableRefreshRatesDistinct()
    {
        HashSet<string> refreshRates = new HashSet<string>();
        foreach (var rate in Screen.resolutions)
        {
            string refreshRate = rate.refreshRate.ToString() + "Hz";
            refreshRates.Add(refreshRate);
        }
        return new List<string>(refreshRates);
    }

    private List<string> GetAvailableSoftShadowQualities()
    {
        return new List<string>() { "No Cascades", "Two Cascades", "Four Cascades" };
    }

  


    private void SetResolution(int index)
    {
        var parts = resolutionDropdown.options[index].text.Split('x');
        int width = int.Parse(parts[0]);
        int height = int.Parse(parts[1]);
        Screen.SetResolution(width, height, Screen.fullScreen);

        PlayerPrefs.SetInt("ResolutionWidth", width);
        PlayerPrefs.SetInt("ResolutionHeight", height);
    }

    private void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0);
    }

    private void SetVSync(bool isEnabled)
    {
        QualitySettings.vSyncCount = isEnabled ? 1 : 0;
        PlayerPrefs.SetInt("VSync", isEnabled ? 1 : 0);
    }

    private void SetGraphicsQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("GraphicsQuality", qualityIndex);
    }

   

    private void SetRefreshRate(int index)
    {
        string refreshRateString = refreshRateDropdown.options[index].text;
        int refreshRate = int.Parse(refreshRateString.Substring(0, refreshRateString.Length - 2));

        PlayerPrefs.SetInt("RefreshRate", refreshRate);
    }

    

   

   

   

   

    private void ResetToDefaults()
    {
        Screen.SetResolution(1920, 1080, true);
        QualitySettings.vSyncCount = 1;
        QualitySettings.SetQualityLevel(2);


        resolutionDropdown.value = GetCurrentResolutionIndex();
        fullScreenToggle.isOn = Screen.fullScreen;
        vsyncToggle.isOn = QualitySettings.vSyncCount != 0;
        graphicsQualityDropdown.value = QualitySettings.GetQualityLevel();

        PlayerPrefs.SetInt("ResolutionWidth", 1920);
        PlayerPrefs.SetInt("ResolutionHeight", 1080);
        PlayerPrefs.SetInt("FullScreen", 1);
        PlayerPrefs.SetInt("VSync", 1);
        PlayerPrefs.SetInt("GraphicsQuality", 2);
        PlayerPrefs.SetInt("TextureQuality", 1);
        PlayerPrefs.SetFloat("ShadowDistance", 150f);
        PlayerPrefs.SetInt("SoftShadowQuality", 2);
    }
}
