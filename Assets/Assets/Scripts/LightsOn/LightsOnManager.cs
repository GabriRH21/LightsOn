using UnityEngine;

public class LightsOnManager : MonoBehaviour
{
	[Header("LightBulb Room")]
	[SerializeField] private Transform _smoke;
	[SerializeField] private Transform _light;

	private int _answerId = 0;
	private int[] _switchsIds = { 1 , 2, 3 };
	private float[] _switchsOnTime = { 0f, 0f, 0f };
	private bool[] _switchsOn = { false, false, false };

	private void Awake() {
		LightsOnEvents.SwitchPressed += SwitchPressed;
		LightsOnEvents.PrepareSolution += PrepareSolution;
		_answerId = _switchsIds[UnityEngine.Random.Range(0, _switchsIds.Length)];
		_light.gameObject.SetActive(false);
		_smoke.gameObject.SetActive(false);
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

	private void PrepareSolution() {
		if (SwitchesOn() == 1) {
            if (_switchsIds[0] == _answerId) {
				if (_switchsOn[0]) {
					_light.gameObject.SetActive(true);
				} else if (((_switchsOnTime[0] < 5 && _switchsOnTime[1] >= 5) || (_switchsOnTime[0] >= 5 && _switchsOnTime[1] < 5)) || 
							((_switchsOnTime[0] < 5 && _switchsOnTime[2] >= 5) || (_switchsOnTime[0] >= 5 && _switchsOnTime[2] < 5))) {
					_smoke.gameObject.SetActive(true);
				}
            } else if (_switchsIds[1] == _answerId) {
				if (_switchsOn[1]) {
					_light.gameObject.SetActive(true);
				} else if (((_switchsOnTime[1] < 5 && _switchsOnTime[0] >= 5) || (_switchsOnTime[1] >= 5 && _switchsOnTime[0] < 5)) || 
							((_switchsOnTime[1] < 5 && _switchsOnTime[2] >= 5) || (_switchsOnTime[1] >= 5 && _switchsOnTime[2] < 5))) {
					_smoke.gameObject.SetActive(true);
				}
			} else {
				if (_switchsOn[2]) {
					_light.gameObject.SetActive(true);
				} else if (((_switchsOnTime[2] < 5 && _switchsOnTime[0] >= 5) || (_switchsOnTime[2] >= 5 && _switchsOnTime[0] < 5)) || 
							((_switchsOnTime[2] < 5 && _switchsOnTime[1] >= 5) || (_switchsOnTime[2] >= 5 && _switchsOnTime[1] < 5))) {
					_smoke.gameObject.SetActive(true);
				}
			}
        }
	}
	
	private int SwitchesOn() {
		int result = 0;
		foreach (var toggle in _switchsOn) {
			if (toggle) {
				result++;
			}
		}
		return result;
	}

	private int GetUniqueToggleOn()  {
		int i = 0;
        foreach (var toggle in _switchsOn) {
			if (toggle) {
				return i;
			}
			i++;
		}
		return i;
    }
} 
