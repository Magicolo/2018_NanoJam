using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class SpriteAssetPostprocessorEditorWindow : EditorWindow
{
	public static TextureImporterType textureType = TextureImporterType.Sprite;
	public static bool isReadable = true;
	public static FilterMode filterMode = FilterMode.Point;
	public static CompressionLevel compressionQuality = CompressionLevel.None;
	public static float spritePixelsPerUnit = 16;

	[MenuItem("Window/Sprite AssetPostprocessor")]
	static void Init()
	{
		GetInstance().Show();
	}

	public static SpriteAssetPostprocessorEditorWindow GetInstance(){
		return EditorWindow.GetWindow<SpriteAssetPostprocessorEditorWindow>();
	}

	void OnGUI()
	{
		GUILayout.Label("Default Settings", EditorStyles.boldLabel);
		EditorGUI.BeginChangeCheck();
		textureType = (TextureImporterType)EditorGUILayout.EnumPopup("Texture Type", textureType);
		isReadable = EditorGUILayout.Toggle("Is Readable",isReadable);
		filterMode = (FilterMode)EditorGUILayout.EnumPopup("Filter Mode", filterMode);
		compressionQuality = (CompressionLevel)EditorGUILayout.EnumPopup("Compression Quality", compressionQuality);
		spritePixelsPerUnit = EditorGUILayout.FloatField("Sprite PixelsPerUnit", spritePixelsPerUnit);

		EditorGUI.EndChangeCheck();
	}
}
