using UnityEngine;
using System.Collections.Generic;

public class IntTileSet : MonoBehaviour {

	public int Size = 9;

	public IList<ISpace<int>> Tiles {get;set;}

	void Awake() {
		Tiles = new List<ISpace<int>>();
		for (var i=1; i<Size; ++i)
			Tiles.Add(new Space<int>(i));
	}
}
