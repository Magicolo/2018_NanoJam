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

	public static Sprite ImportSprite(Texture2D texture)
	{
		//texture.isReadable = true;

		texture.filterMode = SpriteAssetPostprocessorEditorWindow.filterMode;

		var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), SpriteAssetPostprocessorEditorWindow.spritePixelsPerUnit);

		return sprite;
		//importer.isReadable = SpriteAssetPostprocessorEditorWindow.isReadable;
		//texture.compressionQuality = (int)SpriteAssetPostprocessorEditorWindow.compressionQuality;

		//texture.spritePixelsPerUnit = SpriteAssetPostprocessorEditorWindow.spritePixelsPerUnit;
	}
}
