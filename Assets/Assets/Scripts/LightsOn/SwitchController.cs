using UnityEngine;

public class SwitchController : MonoBehaviour
{
	[SerializeField] private int _id = 0;

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
			transform.Rotate(0f, 0f, 180f);
			//Debug.Log("interact");
			LightsOnEvents.SwitchPressed?.Invoke(_id, isOn);
		}
	}
}
