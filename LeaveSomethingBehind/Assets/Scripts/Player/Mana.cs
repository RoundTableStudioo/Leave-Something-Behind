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

        public bool UseMana(int amount) {
            if (!(CurrentMana - amount >= 0)) return false;
            
            CurrentMana -= amount;
            Bar.fillAmount = CurrentMana / _maxMana;

            if (_regen != null)
                StopCoroutine(_regen);

            _regen = StartCoroutine(RegenMana());

            return true;
        }

        private IEnumerator RegenMana() {
            while (CurrentMana < _maxMana)
            {
                CurrentMana += _manager.Stats.ManaRegeneration;
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
