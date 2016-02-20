using UnityEngine;
using System.Collections;

public class InstructionPopups : MonoBehaviour {

	int currentInstrScreen = 1;

	// Use this for initialization
	void Start () {
		transform.Find ("Instr1").gameObject.SetActive (true);
	}

	public void NextInstructionScren() {
		string currInstrName = "Instr" + currentInstrScreen.ToString ();
		this.currentInstrScreen++;
		string nextInstrName = "Instr" + currentInstrScreen.ToString ();

		Transform nextInstr = transform.Find (nextInstrName);
			if (nextInstr)
				nextInstr.gameObject.SetActive (true);
		transform.Find (currInstrName).gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
