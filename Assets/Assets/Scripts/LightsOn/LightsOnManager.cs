using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LightsOnManager : MonoBehaviour
{
	[Header("LightBulb Room")]
	[SerializeField] private Transform _smoke;
	[SerializeField] private Transform _light;
	
	[Space]
	[Header("Explosions")]
	[SerializeField] private TextMeshPro[] _tntTimer;
	[SerializeField] private Transform[] _explosionEffects;
	
	[Space]
	[Header("Settings/General")]
	[SerializeField] private Volume _cameraVolume;
	[SerializeField] private FPSController _characterController;
	[SerializeField] private LigthsOnCanvasManager _canvasScript;

	private int _answerId = 0;
	private int[] _toggleIds = { 1 , 2, 3 };
	private float[] _toggleOnTime = { 0f, 0f, 0f };
	private bool[] _toggleOn = { false, false, false };
	private bool _cheatSolution = false;
	private bool _FinalQuest = false;
	private bool _endGame = false;
	private float _timer = 60;

	private void Awake() {
		LightsOnEvents.SwitchPressed += SwitchPressed;
		LightsOnEvents.PrepareSolution += PrepareSolution;
		LightsOnEvents.FinalQuest += ActivateFinalSelection;
		_answerId = _toggleIds[UnityEngine.Random.Range(0, _toggleIds.Length)];

		_light.gameObject.SetActive(false);
		_smoke.gameObject.SetActive(false);
		foreach(var exp in _explosionEffects) {
			exp.gameObject.SetActive(false);
		}
		_cameraVolume.gameObject.SetActive(false);
	}

	private void FixedUpdate() {
		CheckSwitches();
		if (_timer >= 0) {
			_timer -= Time.deltaTime;
			foreach (var timerText in _tntTimer) {
				timerText.text = System.String.Format("00:{0}",Mathf.Ceil(_timer).ToString());
			}
		} else if (!_endGame){
			_endGame = true;
			Death();
		}
	}

	public void SwitchPressed(int Id, bool isOn) {
		if (!_FinalQuest) {
			try {
				_toggleOn[Id - 1] = isOn;
			} catch (System.Exception e) {
				Debug.LogError(e);
			}
		} else {
			if (Id == _answerId && !_cheatSolution) {
				Debug.Log("Victoria");
			} else {
				Death();
			}
		}
	}

	private void CheckSwitches() {
		for (int i = 0; i < _toggleOn.Length; i++) {
			if (_toggleOn[i] && _toggleOnTime[i] < 5) {
				_toggleOnTime[i] += Time.deltaTime;
			} 
			if (!_toggleOn[i] && _toggleOnTime[i] < 5 && _toggleOnTime[i] > 0) {
				_toggleOnTime[i] -= Time.deltaTime;
			}
		}
	}

	private void PrepareSolution() {
		switch (SwitchesOn()) {
			case 0:
				CheckIfSomeToggleWasOn();
				_cheatSolution = true;
				break;
			case 1:
				_cheatSolution = !CouldBeCorrect();
				break;
			case 2:
				_light.gameObject.SetActive(true);
				_cheatSolution = true;
				break;
			default: 
				_light.gameObject.SetActive(true);
				_cheatSolution = true;
				break;
		}
	}

	private bool CouldBeCorrect() {
		if (WasOtherTogglePressed()) {
			if (_toggleIds[0] == _answerId) {
				if (_toggleOn[0]) {
					_light.gameObject.SetActive(true);
					return true;
				} else if (((_toggleOnTime[0] < 5 && _toggleOnTime[1] >= 5) || (_toggleOnTime[0] >= 5 && _toggleOnTime[1] < 5)) || 
							((_toggleOnTime[0] < 5 && _toggleOnTime[2] >= 5) || (_toggleOnTime[0] >= 5 && _toggleOnTime[2] < 5))) {
					_smoke.gameObject.SetActive(true);
					return true;
				}
			} else if (_toggleIds[1] == _answerId) {
				if (_toggleOn[1]) {
					_light.gameObject.SetActive(true);
					return true;
				} else if (((_toggleOnTime[1] < 5 && _toggleOnTime[0] >= 5) || (_toggleOnTime[1] >= 5 && _toggleOnTime[0] < 5)) || 
							((_toggleOnTime[1] < 5 && _toggleOnTime[2] >= 5) || (_toggleOnTime[1] >= 5 && _toggleOnTime[2] < 5))) {
					_smoke.gameObject.SetActive(true);
					return true;
				}
			} else {
				if (_toggleOn[2]) {
					_light.gameObject.SetActive(true);
					return true;
				} else if (((_toggleOnTime[2] < 5 && _toggleOnTime[0] >= 5) || (_toggleOnTime[2] >= 5 && _toggleOnTime[0] < 5)) || 
							((_toggleOnTime[2] < 5 && _toggleOnTime[1] >= 5) || (_toggleOnTime[2] >= 5 && _toggleOnTime[1] < 5))) {
					_smoke.gameObject.SetActive(true);
					return true;
				}
			}
		}
		return false;
	}

	private void CheckIfSomeToggleWasOn() {
		int wasOn = 0;
		foreach (var toggleTimer in _toggleOnTime) {
			if (toggleTimer >= 5) {
				wasOn++;
			}
		}
		if (wasOn == 2 || wasOn == 3) {
			_smoke.gameObject.SetActive(true);
		}
	}
	
	private int SwitchesOn() {
		int result = 0;
		foreach (var toggle in _toggleOn) {
			if (toggle) {
				result++;
			}
		}
		return result;
	}

	private bool WasOtherTogglePressed() {
		int toggleOn = GetUniqueToggleOn();
		for (int i = 0; i < _toggleOnTime.Length; i++) {
			if (i != toggleOn) {
				if (_toggleOnTime[i] >= 5) {
					return true;
				}
			}
		}
		return false;
	}

	private int GetUniqueToggleOn()  {
		int i = 0;
		foreach (var toggle in _toggleOn) {
			if (toggle) {
				return i;
			}
			i++;
		}
		return i;
	}

	private void ActivateFinalSelection() {
		_FinalQuest = true;
	}

#region Death

	private void Death() {
		_characterController.Death();
		_canvasScript.Death();
		StartCoroutine(Explosion());
		StartCoroutine(CameraEffects());
	}
	private IEnumerator Explosion() {
		foreach(var exp in _explosionEffects) {
			exp.gameObject.SetActive(true);
			yield return new WaitForSeconds(0.1f);
		}
	}

	private IEnumerator CameraEffects() {
		_cameraVolume.gameObject.SetActive(true);
		
		DepthOfField dof;
		ColorAdjustments colorAdj;

		_cameraVolume.profile.TryGet(out dof);
		_cameraVolume.profile.TryGet(out colorAdj);

		float totalDuration = 2f;
		float halfDuration = totalDuration * 0.5f;

		float focalStart   = 50f;
		float focalMid     = 125f;
		float focalEnd     = 200f;

		Color colorStart = Color.white;
		Color colorMid   = new Color(0.6f, 0.05f, 0.05f, 1f);
		Color colorEnd   = Color.black;

		float elapsed = 0f;
		while (elapsed < totalDuration)
		{
			elapsed += Time.deltaTime;
			if (elapsed > totalDuration) {
				elapsed = totalDuration;
			} 
			if (elapsed <= halfDuration) {
				float t1 = elapsed / halfDuration;

				dof.focalLength.value = Mathf.Lerp(focalStart, focalMid, t1);

				colorAdj.colorFilter.value = Color.Lerp(colorStart, colorMid, t1);
			} else {
				float t2 = (elapsed - halfDuration) / halfDuration;

				dof.focalLength.value = Mathf.Lerp(focalMid, focalEnd, t2);

				colorAdj.colorFilter.value = Color.Lerp(colorMid, colorEnd, t2);
			}

			yield return null;
		}
		dof.focalLength.value = focalEnd;
		colorAdj.colorFilter.value = colorEnd;
		yield return new WaitForSeconds(1.5f);
		// ToDo: Change Scene to loser one
	}
#endregion
} 
