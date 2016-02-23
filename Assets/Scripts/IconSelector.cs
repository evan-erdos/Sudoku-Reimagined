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
			SetTileDefault ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			SetTileLevel ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			SetTileLower ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			SetTileRaise ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha5)) {
			SetTileSpout ();
		}
	}

	public void SetTileDefault() { SetTile(Tiles.Default); }
	public void SetTileRaise() { SetTile(Tiles.Raise); }
	public void SetTileLower() { SetTile(Tiles.Lower); }
	public void SetTileLevel() { SetTile(Tiles.Level); }
	public void SetTileSpout() { SetTile(Tiles.Spout); }
	public void SetTileEmpty() { SetTile(Tiles.Empty); }

	public void SetTile(Tiles tile) {
		Current = tile;
		// update tranform of selected thing 	
	}

	public static GameObject CreateTile(Tiles tile) {
		var space = Object.Instantiate(globalTiles[(int) tile]) as GameObject;
		space.GetComponent<ISpace<Tiles>>().Value = tile;
		return space;
	}
}
