using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "EndlessBattle/GameEvent", order = 2)]
public class ScriptableGameEvent : ScriptableObject {
	
	[SerializeField] private List<GameEventListener> listeners = null;
	
	public void Call() {
		for (int i = 0; i < listeners.Count; i++)
		{
			listeners[i].OnEventCalled();
		}
	}

	public void AddListener(GameEventListener listener) {
		listeners.Add(listener);
	}

	public void RemoveListener(GameEventListener listener) {
		listeners.Remove(listener);
	}
}