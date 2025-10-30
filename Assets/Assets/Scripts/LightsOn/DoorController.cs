using Unity.VisualScripting;
using UnityEngine;

public class DoorController : MonoBehaviour
{
	[SerializeField] private Transform _door;
	private bool _openedBefore = false;

	private bool _canInteract = false;

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Player" && !_openedBefore) {
			_canInteract = true;
			LightsOnEvents.RaiseShowInteractMessage(true, "[E] Interact");
		} 
	}

	private void OnTriggerExit(Collider other) {
		if (other.tag == "Player" && !_openedBefore) {
			_canInteract = false;
			LightsOnEvents.RaiseShowInteractMessage(false);
		}
	}

	private void Update() {
		if (_canInteract && Input.GetKeyDown(KeyCode.E)) {
			_door.transform.Rotate(0f, 100f, 0f);
			LightsOnEvents.PrepareSolution?.Invoke();
			_openedBefore = true;
			_canInteract = false;
			LightsOnEvents.RaiseShowInteractMessage(false);
		}
	}

	public void CloseDoor() {
		_door.transform.Rotate(0f, -100f, 0f);;
	}
}
