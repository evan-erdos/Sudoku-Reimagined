using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LoadSceneOnClick : MonoBehaviour {

	bool wait;

    public string scene = "Zendoku-intro";

	void Update() {
		if (Input.anyKey)
        	StartCoroutine(StartGame(8f));
	}

	IEnumerator StartGame(float delay) {
		if (wait) yield break;
		wait = true;
		CameraFade.StartAlphaFade(
            new Color(0,0,0),false,1f,delay,
            ()=> {
                if (!Camera.main) return;
                Camera.main.cullingMask = 0;
                Camera.main.clearFlags =
                    CameraClearFlags.SolidColor;
                Camera.main.backgroundColor =
                    new Color(0,0,0);
                SceneManager.LoadScene(scene);});
		yield return new WaitForSeconds(delay);
	}
}
