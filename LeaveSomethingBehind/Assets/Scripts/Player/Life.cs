using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace RoundTableStudio.Player {
	public class Life : MonoBehaviour {
		public Image Bar;
		public float RegenerationCooldown = 60f;

		private PlayerManager _manager;
		private float _currentHp;
		private float _maxHp;

		private void Start() {
			_manager = GetComponent<PlayerManager>();
			_maxHp = _manager.Stats.MaxHp;
			
			_currentHp = _maxHp;
		}

		public bool LoseHp(float amount) {
			if (_currentHp - amount <= 0) return true;

			_currentHp -= amount;
			Bar.fillAmount = _currentHp / _maxHp;

			// if (_regen != null)
			// 	StopCoroutine(RegenerateLife());
			//
			// if(_manager.Stats.LifeRegeneration != 0)
			// 	StartCoroutine(RegenerateLife());
			
			return false;
		}

		public void RecoverHp(float amount) {
			_currentHp += amount;
		}
	}
}
