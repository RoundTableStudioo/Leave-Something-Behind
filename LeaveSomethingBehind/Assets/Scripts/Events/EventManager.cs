using System;
using UnityEngine;
using RoundTableStudio.Shared;

namespace RoundTableStudio.Events {
	public class EventManager : MonoBehaviour {
		public static EventManager Instance;
		
		private void Awake() {
			if (Instance != null) return;

			Instance = this;
		}

		public event Action<Damage> OnTakeDamage;
	}
}
