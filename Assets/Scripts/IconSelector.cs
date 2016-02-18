using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class IconSelector : MonoBehaviour {

	public GameObject[] tiles;

	static GameObject[] globalTiles;

	public static Tiles Current {get;set;}

	public void Awake() {
		Debug.Log ("Icon Selector awakened!");

		IconSelector.globalTiles = tiles;
	}

	public void Update() {
		//Debug.Log ("ICON SELECTOR UDPDAAATE");
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			SetTile(Tiles.Default);
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			SetTile(Tiles.Level);
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			SetTile(Tiles.Lower);
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			SetTile (Tiles.Raise);
		}
		if (Input.GetKeyDown (KeyCode.Alpha5)) {
			SetTile(Tiles.Spout);
		}
	}

	public void SetTileDefault() { SetTile(Tiles.Default); }
	public void SetTileRaise() { SetTile(Tiles.Raise); }
	public void SetTileLower() { SetTile(Tiles.Lower); }
	public void SetTileLevel() { SetTile(Tiles.Level); }
	public void SetTileSpout() { SetTile(Tiles.Spout); }

	public void SetTile(Tiles tile) {
		Current = tile;
	}

	public static GameObject CreateTile(Tiles tile) {
		var space = Object.Instantiate(globalTiles[(int) tile]) as GameObject;
		space.GetComponent<ISpace<Tiles>>().Value = tile;
		return space;
	}
}
