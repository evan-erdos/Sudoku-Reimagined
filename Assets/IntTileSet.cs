using UnityEngine;
using ui=UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(ui::Text))]
public class IntTileSet : TileSet<int> {

	public int Size = 9;

	public IList<ISpace<int>> Tiles {get;set;}

	protected ui::Text uiText;

	void Awake() {
		uiText = GetComponent<ui::Text>();

		Tiles = new List<ISpace<int>>();
		for (var i=1; i<Size; ++i)
			Tiles.Add(new Space<int>(i));
	}

	public void Print(string s) {
		uiText.text = s;
	}
}
