using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace  RoundTableStudio.Player {
    public class Mana : MonoBehaviour {
        public Image Bar;
        public float RegenerationCooldown;
        
        private float _currentMana;
        private PlayerManager _manager;
        private float _maxMana;
        private Coroutine _regen;

        private void Start() {
            _manager = GetComponent<PlayerManager>();
            _maxMana = _manager.Stats.Mana;
            
            _currentMana = _maxMana;
            Bar.fillAmount = 1f;
        }

        public bool UseMana(int amount) {
            if (!(_currentMana - amount >= 0)) return false;
            
            _currentMana -= amount;
            Bar.fillAmount = _currentMana / _maxMana;

            if (_regen != null)
                StopCoroutine(_regen);

            _regen = StartCoroutine(RegenMana());

            return true;
        }

        private IEnumerator RegenMana() {
            while (_currentMana < _maxMana) {
                _currentMana += _manager.Stats.ManaRegeneration;
                Bar.fillAmount = _currentMana / _maxMana;
                yield return new WaitForSeconds(RegenerationCooldown);
            }

            if (_currentMana > _maxMana)
                _currentMana = _maxMana;
            
            _regen = null;
        }

        public void AddMana(){
            if(_currentMana < _maxMana)
                _currentMana = _maxMana;
        }
    }
}
