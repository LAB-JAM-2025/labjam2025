using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{
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

    public void SetBlueBackground()
    {
        RenderSettings.skybox = blueSky;
        RenderSettings.fog = true;
        RenderSettings.fogColor = blueFog;
        RenderSettings.fogDensity = fogDensity;
        DynamicGI.UpdateEnvironment(); 
    }

    public void SetRedBackground()
    {
        RenderSettings.skybox = redSky;
        RenderSettings.fog = true;
        RenderSettings.fogColor = redFog;
        RenderSettings.fogDensity = fogDensity;
        DynamicGI.UpdateEnvironment();
    }

    public void SetDarkBackground()
    {
        RenderSettings.skybox = darkSky;
        RenderSettings.fog = true;
        RenderSettings.fogColor = darkFog;
        RenderSettings.fogDensity = fogDensity;
        DynamicGI.UpdateEnvironment();
    }
}