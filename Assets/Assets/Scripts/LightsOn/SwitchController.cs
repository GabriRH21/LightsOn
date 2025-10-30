using UnityEngine;

public class SwitchController : MonoBehaviour
{
	[SerializeField] private int id = 0;

	private bool isOn = false;
  	private bool canInteract = false;
	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			canInteract = true;
			LightsOnEvents.RaiseShowInteractMessage(true, "[E] Interact");
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			canInteract = false;
			LightsOnEvents.RaiseShowInteractMessage(false);
		}
	}

	private void Update() {
		if (canInteract && Input.GetKeyDown(KeyCode.E)) {
			isOn = !isOn;
			transform.rotation = Quaternion.Euler(isOn ? new Vector3(180, 0, 180) : new Vector3(180, 0, 0));
			Debug.Log("interact");
		}
	}


}
