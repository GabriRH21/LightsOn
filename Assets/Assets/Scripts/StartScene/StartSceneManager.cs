using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
	[SerializeField] Button _startButton;
	[SerializeField] Button _exitButton;

    private void Awake() {
        _startButton.onClick.AddListener(OnStartButtonPressed);
		_exitButton.onClick.AddListener(OnExitButtonPressed);
    }

	private void OnStartButtonPressed() {
		SceneManager.LoadScene("LightsOnScene");
	}

	private void OnExitButtonPressed() {
		Application.Quit();
	}
}
