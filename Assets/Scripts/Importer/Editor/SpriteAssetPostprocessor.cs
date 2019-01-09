using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class SpriteAssetPostprocessor : AssetPostprocessor
{
	void OnPreprocessTexture()
	{
		var importer = (TextureImporter)assetImporter;
		Object asset = AssetDatabase.LoadAssetAtPath(assetImporter.assetPath, typeof(Texture2D));

		if (!asset)
		{
			importer.textureType = SpriteAssetPostprocessorEditorWindow.textureType;
			importer.isReadable = SpriteAssetPostprocessorEditorWindow.isReadable;
			importer.filterMode = SpriteAssetPostprocessorEditorWindow.filterMode;
			importer.compressionQuality = (int)SpriteAssetPostprocessorEditorWindow.compressionQuality;
			importer.spritePixelsPerUnit = SpriteAssetPostprocessorEditorWindow.spritePixelsPerUnit;
		}
	}
}
