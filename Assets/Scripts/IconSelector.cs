using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IconSelector : MonoBehaviour {

	public GameObject[] tiles;

	static GameObject[] globalTiles;

	public static Tiles Current {get;set;}

	public void Awake() {
		IconSelector.globalTiles = tiles;
	}

	public void SetTileDefault() { SetTile(Tiles.Default); }
	public void SetTileRaise() { SetTile(Tiles.Raise); }
	public void SetTileLower() { SetTile(Tiles.Lower); }
	public void SetTileLevel() { SetTile(Tiles.Level); }
	public void SetTileSpout() { SetTile(Tiles.Spout); }

	public void SetTile(Tiles tile) { Current = tile; }

	public static GameObject CreateTile(Tiles tile) {
		return (Object.Instantiate(globalTiles[(int) tile]) as GameObject);
	}
}
