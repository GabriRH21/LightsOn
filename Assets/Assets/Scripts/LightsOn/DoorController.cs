using Unity.VisualScripting;
using UnityEngine;

public class DoorController : MonoBehaviour
{
	[SerializeField] private Transform _door;

	private bool _canInteract = false;

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			_canInteract = true;
			LightsOnEvents.RaiseShowInteractMessage(true, "[E] Interact");
		} 
	}

	private void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			_canInteract = false;
			LightsOnEvents.RaiseShowInteractMessage(false);
		}
	}

	private void Update() {
		if (_canInteract && Input.GetKeyDown(KeyCode.E)) {
			_door.transform.rotation = Quaternion.Euler( new Vector3(0, 100, 0) );
			LightsOnEvents.PrepareSolution?.Invoke();
		}
	}
}
