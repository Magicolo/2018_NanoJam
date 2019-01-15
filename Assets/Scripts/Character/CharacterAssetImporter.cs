using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CharacterAssetImporter : ImporterBase<CharacterAssets>
{

	protected override string AssetTypeName => "Character";

	protected override string Path => "Characters";

	protected override KeyCode ForceReloadKey => Cheat.ReloadCharacters;

	protected override List<CharacterAssets> CurrentAssets => CharacterAssetManager.Instance.CharacterAsset;

	protected override void LoadAsset(CharacterAssets assetObject, DirectoryInfo assetFolder)
	{
		throw new System.NotImplementedException();
	}
}
