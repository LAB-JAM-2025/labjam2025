using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{
    public Material sphereMaterial;

    [Header("Skyboxes")]
    public Material blueSky;
    public Material redSky;
    public Material darkSky;

    [Header("Fog Colors")]
    public Color blueFog = Color.cyan;
    public Color redFog = Color.red;
    public Color darkFog = Color.black;

    [Header("Fog Density")]
    public float fogDensity = 0.01f;

    void Start()
    {
        // Set a default background
        SetBlueBackground();
    }

    public void SetBlueBackground()
    {
        RenderSettings.skybox = blueSky;
        RenderSettings.fog = true;
        RenderSettings.fogColor = blueFog;
        RenderSettings.fogDensity = fogDensity;
        sphereMaterial.SetColor("_BgColor", blueFog);
        DynamicGI.UpdateEnvironment(); 
    }

    public void SetRedBackground()
    {
        RenderSettings.skybox = redSky;
        RenderSettings.fog = true;
        RenderSettings.fogColor = redFog;
        RenderSettings.fogDensity = fogDensity;
        sphereMaterial.SetColor("_BgColor", redFog);

        DynamicGI.UpdateEnvironment();
    }

    public void SetDarkBackground()
    {
        RenderSettings.skybox = darkSky;
        RenderSettings.fog = true;
        RenderSettings.fogColor = darkFog;
        RenderSettings.fogDensity = fogDensity;
        sphereMaterial.SetColor("_BgColor", darkFog);
        DynamicGI.UpdateEnvironment();
    }
}