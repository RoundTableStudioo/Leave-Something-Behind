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
		private float _regenerationTick;

		private void Start() {
			_manager = GetComponent<PlayerManager>();
			_maxHp = _manager.Stats.MaxHp;
			
			_currentHp = _maxHp;
		}

		public void TickUpdate() {
			if(_currentHp <= _maxHp && _manager.Stats.LifeRegeneration != 0 && _regenerationTick <= 0)
				RegenerateLife();

			_regenerationTick -= Time.deltaTime;
		}

		public bool LoseHp(float amount) {
			if (_currentHp - amount <= 0) return true;

			_currentHp -= amount;
			Bar.fillAmount = _currentHp / _maxHp;

			return false;
		}

		private void RegenerateLife() {
			_currentHp += _manager.Stats.LifeRegeneration;
			Bar.fillAmount = _currentHp / _maxHp;

			_regenerationTick = RegenerationCooldown;
		}

		public void RecoverHp(float amount) {
			_currentHp += amount;
		}
	}
}
