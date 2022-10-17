using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace RoundTableStudio.Player {
    public class Stamina : MonoBehaviour {
        public Slider Bar;
        
        private int _maxStamina;
        private int _currentStamina;
        private PlayerManager _manager;
        private Coroutine _regen;

        private void Start() {
            _manager = GetComponent<PlayerManager>();
            _maxStamina = _manager.Stats.Stamina;
            
            _currentStamina = _maxStamina;
            Bar.maxValue = _maxStamina;
            Bar.value = _maxStamina;
        }

        public void UseStamina(int amount) {
            if (_currentStamina - amount >= 0) {
                _currentStamina -= amount;
                Bar.value = _currentStamina;

                if (_regen != null)
                    StopCoroutine(_regen);

                _regen = StartCoroutine(RegenStamina());
            }
            else {
                Debug.Log("TO DO - Not enough stamina message");
            }
        }

        private IEnumerator RegenStamina() {
            yield return new WaitForSeconds(2);

            while (_currentStamina < _maxStamina)
            {
                _currentStamina += _maxStamina / 100;
                Bar.value = _currentStamina;
            }
            
            _regen = null;
        }

    }
}
