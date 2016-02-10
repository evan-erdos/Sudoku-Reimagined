using UnityEngine;
using ui=UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(ui::Text))]
public class TileSet : MonoBehaviour {

	public int Size = 9;

	public IList<ISpace<Tiles>> TileList {get;set;}

	protected ui::Text uiText;

	public ui::Text scoreText;

	void Awake() {
		uiText = GetComponent<ui::Text>();
		//scoreText = GetComponentInChildren<ui::Text>();

		TileList = new List<ISpace<Tiles>>();
		//for (var i=1; i<Size; ++i)
		foreach (var tile in EnumUtil.GetValues<Tiles>())
			TileList.Add(new Space<Tiles>(tile));
	}

	public void Print(string s) {
		uiText.text = s;
	}

	public void PrintScore(int n) {
		scoreText.text = ("Score: "+n);
	}
}
