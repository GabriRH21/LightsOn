
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSController : MonoBehaviour
{
	private CharacterController _characterController;

	[Header("Movement Constants")]
	[SerializeField] private float _walkSpeed = 6.0f;
	[SerializeField] private float _runSpeed = 10.0f;
	[SerializeField] private float _gravity = 20.0f;

	private Vector3 move = new Vector3(0, 0, 0);

	[Space]
	[Header("Camera Constants")]
	[SerializeField] private Camera cam;
	[SerializeField] private float _mouseHorizontal = 3.0f;
	[SerializeField] private float _mouseVertical = 2.0f;
	[SerializeField] private float _minRotation = -65.0f;
	[SerializeField] private float _maxRotation = 60.0f;

	[Space]
	[SerializeField] private AudioClip[] _steps;
	[SerializeField] private AudioSource _audioSource;

	[Space]
	[Header("Others")]
	[SerializeField] private Rigidbody _rb;
	private float _hMouse, _vMouse;
	private bool _canMove = true;
	private float _stepTime = 0f;
	

	void Awake() {
		_characterController = this.GetComponent<CharacterController>();
	}

	void Update() {
		if (_canMove) {
			_hMouse = _mouseHorizontal * Input.GetAxis("Mouse X");
			_vMouse += _mouseVertical * Input.GetAxis("Mouse Y");

			_vMouse = Mathf.Clamp(_vMouse, _minRotation, _maxRotation);
			cam.transform.localEulerAngles = new UnityEngine.Vector3(-_vMouse, 0, 0);
			transform.Rotate(0, _hMouse, 0);

			if (_characterController.isGrounded)
			{
				move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
				if (Input.GetKeyDown(KeyCode.LeftShift)) {
					move = transform.TransformDirection(move) * _runSpeed;
				} else {
					move = transform.TransformDirection(move) * _walkSpeed;
				}

				if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
				{
					PlaySteps();
				}
				
			}

			move.y -= _gravity * Time.deltaTime;
			_characterController.Move(move * Time.deltaTime);
		}
	}

	public void PlaySteps() {
		_stepTime += Time.deltaTime;
				
		if (_stepTime >= 0.5) {
			_stepTime = 0;
			AudioClip newClip = _steps[UnityEngine.Random.Range(0, _steps.Length)];
			_audioSource.clip = newClip;
			_audioSource.Play();
		}
	}

	public void Death() {
		_rb.constraints = RigidbodyConstraints.None;
		this.enabled = false;
	}

	public void CanMove(bool condition) {
		_canMove = condition;
	}
}
