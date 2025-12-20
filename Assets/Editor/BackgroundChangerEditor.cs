using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BackgroundChanger))]
public class BackgroundChangerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Show all default fields

        BackgroundChanger changer = (BackgroundChanger)target;

        GUILayout.Space(10);
        GUILayout.Label("Quick Background Buttons", EditorStyles.boldLabel);

        if (GUILayout.Button("Blue Background"))
        {
            changer.SetBlueBackground();
        }

        if (GUILayout.Button("Red Background"))
        {
            changer.SetRedBackground();
        }

        if (GUILayout.Button("Dark Background"))
        {
            changer.SetDarkBackground();
        }
    }
}