using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider manaBar;

    private int _maxMana = 6;
    private int _currentMana;

    private WaitForSeconds _regenTick = new WaitForSeconds(5f);
    private Coroutine _regen;

    public static ManaBar instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _currentMana = _maxMana;
        manaBar.maxValue = _maxMana;
        manaBar.value = _maxMana;
    }

    public void UseMana(int amount)
    {
        if (_currentMana - amount >= 0)
        {
            _currentMana -= amount;
            manaBar.value = _currentMana;

            if (_regen != null)
                StopCoroutine(_regen);

            _regen = StartCoroutine(RegenMana());
        }
        else
        {
            Debug.Log("No tienes suficiente mana");
        }
    }

    private IEnumerator RegenMana()
    {
        yield return new WaitForSeconds(5);

        while (_currentMana < _maxMana)
        {
            _currentMana += _maxMana / 100;
            manaBar.value = _currentMana;
            yield return _regenTick;
        }
        _regen = null;
    }

    public void AddMana()
    {
        if (_currentMana < _maxMana)
        {
            _currentMana = _maxMana;
        }    
    }
}
