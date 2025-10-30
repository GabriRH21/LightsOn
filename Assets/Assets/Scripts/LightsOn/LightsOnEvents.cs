using UnityEngine;

public static class LightsOnEvents
{
	public static System.Action<bool, string> OnShowInteractMessage;
	
	public static void RaiseShowInteractMessage(bool show, string message = null) {
		OnShowInteractMessage?.Invoke(show, message);
	}
}
