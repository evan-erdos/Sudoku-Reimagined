using UnityEngine;
using System.Collections;

public class LockCameraRotation : MonoBehaviour {

	public Transform target;

	void Awake() {
		if (!target)
			throw new System.Exception("No target");

	}

	void FixedUpdate() {
		transform.eulerAngles = new Vector3(
			90f,
			target.eulerAngles.y+30f,
			target.eulerAngles.z);

	}
}
