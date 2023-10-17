using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour {
	[SerializeField] private ScriptableGameEvent gameEvent = null;
	[SerializeField] private UnityEvent response = null;

	private void OnEnable () {
		gameEvent.AddListener (this);
	}

	private void OnDisable () {
		gameEvent.RemoveListener (this);
	}

	public void OnEventCalled () {
		response.Invoke ();
	}
}