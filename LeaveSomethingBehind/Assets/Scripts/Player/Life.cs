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
		private Coroutine _regen;

		private void Start() {
			_manager = GetComponent<PlayerManager>();
			_maxHp = _manager.Stats.MaxHp;
			
			_currentHp = _maxHp;
		}

		public bool LoseHp(float amount) {
			if (_currentHp - amount <= 0) return true;

			_currentHp -= amount;
			Bar.fillAmount = _currentHp / _maxHp;

			if (_regen != null && _manager.Stats.LifeRegeneration != 0)
				StopCoroutine(RegenerateLife());

			StartCoroutine(RegenerateLife());
			
			return false;
		}

		private IEnumerator RegenerateLife() {
			while (_currentHp < _maxHp) {
				_currentHp += _manager.Stats.LifeRegeneration;
				Bar.fillAmount = _currentHp / _maxHp;
				yield return new WaitForSeconds(RegenerationCooldown);
			}

			_regen = null;
		}
	}
}
