using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IconSelector : MonoBehaviour {

	public GameObject[] tiles;

	static GameObject[] globalTiles;

	public static Dir CurrentSelectDir {get;set;}

	public static Tiles Current {get;set;}

	public void Awake() {
		IconSelector.globalTiles = tiles;
	}


	public void SetTileDefault() { SetTile(Tiles.Default); }
	public void SetTileRaise() { SetTile(Tiles.Raise); }
	public void SetTileLower() { SetTile(Tiles.Lower); }
	public void SetTileLevel() { SetTile(Tiles.Level); }
	public void SetTileSpout() { SetTile(Tiles.Spout); }

	public void SetTile(Tiles tile) { 
		if (tile == Current)
			CurrentSelectDir = (Dir)(((int)CurrentSelectDir + 1) % 4);
		else
			CurrentSelectDir = 0;

//		Debug.Log ("Current icon select dir:");
//		Debug.Log (CurrentSelectDir);

		Current = tile;
	}

	public static GameObject CreateTile(Tiles tile) {
		var space = Object.Instantiate(globalTiles[(int) tile]) as GameObject;
		space.GetComponent<ISpace<Tiles>>().Value = tile;
		return space;
	}
}
