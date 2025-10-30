using UnityEngine;

public class LightsOnManager : MonoBehaviour
{
	private int _answerId = 0;
	private int[] _switchsIds = { 1 , 2, 3 };
	private float[] _switchsOnTime = { 0f, 0f, 0f };
	private bool[] _switchsOn = { false, false, false };

	private void Awake() {
		LightsOnEvents.SwitchPressed += SwitchPressed;
		_answerId = _switchsIds[UnityEngine.Random.Range(0, _switchsIds.Length)];
	}

	private void Update() {
		CheckSwitches();
	}

	public void SwitchPressed(int Id, bool isOn) {
		try {
			_switchsOn[Id - 1] = isOn;
		} catch (System.Exception e) {
			Debug.LogError(e);
		}
	}

	private void CheckSwitches() {
		for (int i = 0; i < _switchsOn.Length; i++) {
			if (_switchsOn[i] && _switchsOnTime[i] < 5) {
            	_switchsOnTime[i] += Time.deltaTime;
        	} 
			if (!_switchsOn[i] && _switchsOnTime[i] < 5 && _switchsOnTime[i] > 0) {
                _switchsOnTime[i] -= Time.deltaTime;
            }
		}
	}
} 
