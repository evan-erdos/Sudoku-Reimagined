using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadOnClick : MonoBehaviour {



	public void LoadScene(string scene) {
		SceneManager.LoadScene (scene);
	}
}
