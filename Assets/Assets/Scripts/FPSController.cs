
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
	private float _hMouse, _vMouse;
	

	void Awake() {
		_characterController = this.GetComponent<CharacterController>();
	}

	void Update() {
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
		}

		move.y -= _gravity * Time.deltaTime;
		_characterController.Move(move * Time.deltaTime);
	}
}
