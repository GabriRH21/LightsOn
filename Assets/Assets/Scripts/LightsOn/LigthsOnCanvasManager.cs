using TMPro;
using UnityEngine;

public class LigthsOnCanvasManager : MonoBehaviour
{
	[Header("UI")] 
	[SerializeField] private TextMeshProUGUI _interactMessage;

	private void Awake() {
		LightsOnEvents.OnShowInteractMessage += ShowInteractMessage;
		_interactMessage.gameObject.SetActive(false);
	}

	private void ShowInteractMessage(bool show, string message) {
		_interactMessage.gameObject.SetActive(show);
		if (!show) {
			return;
		}
		_interactMessage.text = message;
	}

} 
