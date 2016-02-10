using UnityEngine;
using System.Collections;

public class IconSelector : MonoBehaviour {

	public GameObject[] tiles;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject CreateTile(Tiles tile){
		return (Object.Instantiate(tiles[(int) tile]) as GameObject);
	} 
}
