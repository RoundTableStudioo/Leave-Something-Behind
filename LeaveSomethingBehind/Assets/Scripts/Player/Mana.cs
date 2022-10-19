using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace  RoundTableStudio.Player {
    public class Mana : MonoBehaviour {
        public Image Bar;
        [HideInInspector]
        public float CurrentMana;

        private PlayerManager _manager;
        private float _maxMana;
        private Coroutine _regen;

        private void Start() {
            _manager = GetComponent<PlayerManager>();
            _maxMana = _manager.Stats.Mana;
            
            CurrentMana = _maxMana;
            Bar.fillAmount = 1f;
        }

        public void UseMana(int amount) {
            if (CurrentMana - amount >= 0) {
                CurrentMana -= amount;
                Bar.fillAmount = CurrentMana / _maxMana;

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
                Bar.fillAmount = CurrentMana / _maxMana;
                yield return new WaitForSeconds(5);
            }

            if (CurrentMana > _maxMana)
                CurrentMana = _maxMana;
            
            _regen = null;
        }

        public void AddMana(){
            if(CurrentMana < _maxMana)
                CurrentMana = _maxMana;
        }
    }
}
