using TMPro;
using UnityEngine;

public class LigthsOnCanvasManager : MonoBehaviour
{
	[Header("UI")] 
	[SerializeField] private TextMeshProUGUI _interactMessage;
	[SerializeField] private TextMeshProUGUI _Seconds;

	private float _realSeconds = 60;

	private void Awake() {
		LightsOnEvents.OnShowInteractMessage += ShowInteractMessage;
		_interactMessage.gameObject.SetActive(false);
	}

	private void Update() {
		if (_realSeconds >= 0) {
            _realSeconds -= Time.deltaTime;
			_Seconds.text = Mathf.Ceil(_realSeconds).ToString();
        } else {
			//Lose
			Debug.Log("Derrota");
		}
	}

	private void ShowInteractMessage(bool show, string message) {
		_interactMessage.gameObject.SetActive(show);
		if (!show) {
			return;
		}
		_interactMessage.text = message;
	}

} 
