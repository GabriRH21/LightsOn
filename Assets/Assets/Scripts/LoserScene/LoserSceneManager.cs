using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoserSceneManager : MonoBehaviour
{
    [SerializeField] private Button _tryAgainButton;
	[SerializeField] private Button _exit;

	private void Awake() {
		_tryAgainButton.onClick.AddListener(TryAgain);
		_exit.onClick.AddListener(Exit);
	}

	private void TryAgain() {
		SceneManager.LoadScene("LightsOnScene");
	}

	private void Exit() {
		Application.Quit();
	}
}
