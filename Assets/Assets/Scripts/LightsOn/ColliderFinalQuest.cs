using UnityEngine;

public class ColliderFinalQuest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			LightsOnEvents.FinalQuest?.Invoke();
			Destroy(this.gameObject);
		}
	}
}
