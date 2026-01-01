using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class SceneReference
{
    public string scenePath;

#if UNITY_EDITOR
    public SceneAsset sceneAsset;
#endif

    public string SceneName
    {
        get
        {
#if UNITY_EDITOR
            if (sceneAsset != null)
                return sceneAsset.name;
#endif
            // obtiene solo el nombre sin extensión
            return System.IO.Path.GetFileNameWithoutExtension(scenePath);
        }
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(SceneReference))]
public class SceneReferenceDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var sceneAssetProp = property.FindPropertyRelative("sceneAsset");
        var scenePathProp = property.FindPropertyRelative("scenePath");

        EditorGUI.BeginProperty(position, label, property);

        var sceneAsset = (SceneAsset)sceneAssetProp.objectReferenceValue;
        var newAsset = EditorGUI.ObjectField(position, label, sceneAsset, typeof(SceneAsset), false);

        if (newAsset != sceneAssetProp.objectReferenceValue)
        {
            sceneAssetProp.objectReferenceValue = newAsset;
            scenePathProp.stringValue = (newAsset != null) ? AssetDatabase.GetAssetPath(newAsset) : "";
        }

        EditorGUI.EndProperty();
    }
}
#endif