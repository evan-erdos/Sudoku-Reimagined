using UnityEngine;
using System.Collections;

public class ExitGame : MonoBehaviour {

	public void Exit() {
		Debug.Log ("Exit!");
		Application.Quit ();
	}
}

