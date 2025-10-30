using Unity.VisualScripting;
using UnityEngine;

public class CloseDoorCollider : MonoBehaviour
{   
    [SerializeField] DoorController _door;
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            _door.CloseDoor();
            Destroy(this.gameObject);
        }
    }
}
