using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace RoundTableStudio.Player {
    public class Stamina : MonoBehaviour {
        public Image Bar;
        public float RegenerationCooldown;
        
        private float _maxStamina;
        private float _currentStamina;
        private PlayerManager _manager;
        private Coroutine _regen;

        private void Start() {
            _manager = GetComponent<PlayerManager>();
            _maxStamina = _manager.Stats.Stamina;
            
            _currentStamina = _maxStamina;
            Bar.fillAmount = 1f;
        }

        public bool UseStamina(int amount) {
            if (!(_currentStamina - amount >= 0)) return false;
            
            _currentStamina -= amount;
            Bar.fillAmount = _currentStamina / _maxStamina;

            if (_regen != null)
                StopCoroutine(_regen);

            _regen = StartCoroutine(RegenStamina());

            return true;
        }

        private IEnumerator RegenStamina() {
            while (_currentStamina < _maxStamina) {
                _currentStamina += _manager.Stats.StaminaRegeneration;
                Bar.fillAmount = _currentStamina / _maxStamina;
                yield return new WaitForSeconds(RegenerationCooldown);
            }
            
            if (_currentStamina > _maxStamina)
                _currentStamina = _maxStamina;
            
            _regen = null;
        }

    }
}
