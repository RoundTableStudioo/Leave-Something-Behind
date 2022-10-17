using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace  RoundTableStudio.Player {
    public class Mana : MonoBehaviour {
        public Slider Bar;
        [HideInInspector]
        public int CurrentMana;

        private PlayerManager _manager;
        private int _maxMana;
        private Coroutine _regen;

        private void Start() {
            _manager = GetComponent<PlayerManager>();
            _maxMana = _manager.Stats.Mana;
            
            CurrentMana = _maxMana;
            Bar.maxValue = _maxMana;
            Bar.value = _maxMana;
        }

        public void UseMana(int amount) {
            if (CurrentMana - amount >= 0) {
                CurrentMana -= amount;
                Bar.value = CurrentMana;

                if (_regen != null)
                    StopCoroutine(_regen);

                _regen = StartCoroutine(RegenMana());
            } else {
                Debug.Log("TO DO - Not enough mana message");
            }
        }

        private IEnumerator RegenMana() {
            yield return new WaitForSeconds(5);

            while (CurrentMana < _maxMana)
            {
                CurrentMana += _maxMana / 100;
                Bar.value = CurrentMana;
                yield return new WaitForSeconds(5);
            }

            if (CurrentMana > _maxMana)
                CurrentMana = _maxMana;
            
            _regen = null;
        }

        public void AddMana(int amount) {
            CurrentMana += amount;
            
            if (CurrentMana > _maxMana) {
                CurrentMana = _maxMana;
            }
        }
    }
}
