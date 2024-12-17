using UnityEditor;

[CustomEditor(typeof(CircleGraphic)), CanEditMultipleObjects]
public class CircleEditor : Editor
{
    private SerializedProperty colorProp;

    private void OnEnable()
    {
        colorProp = serializedObject.FindProperty("m_Color");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(colorProp);
        DrawPropertiesExcluding(serializedObject, "m_Script", "m_Color", "m_OnCullStateChanged");

        serializedObject.ApplyModifiedProperties();
    }
}
