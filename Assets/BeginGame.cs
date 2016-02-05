using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class BeginGame : MonoBehaviour {

	bool wait;

	MovieTexture movie;

    void Start() {
        movie = (MovieTexture) GetComponent<Renderer>().material.mainTexture;
        movie.loop = true;
        movie.Play();
        //CameraFade.StartAlphaFade(
        //    new Color(0,0,0),true,2f,0f);
        StartCoroutine(StartGame(12f));
    }

#if CLICK
	void Update() {
		if (Input.anyKey)
        	StartCoroutine(StartGame(8f));
	}
#endif

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
                SceneManager.LoadScene("Sudoku");});
		yield return new WaitForSeconds(delay);
	}
}
