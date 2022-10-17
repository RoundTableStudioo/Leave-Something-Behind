using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider Bar;

    private int _maxStamina = 10;
    private int _currentStamina;

    private WaitForSeconds _regenTick = new WaitForSeconds(2f);
    private Coroutine _regen;

    void Start()
    {
        _currentStamina = _maxStamina;
        Bar.maxValue = _maxStamina;
        Bar.value = _maxStamina;
    }

    public void UseStamina(int amount)
    {
        if (_currentStamina - amount >= 0)
        {
            _currentStamina -= amount;
            Bar.value = _currentStamina;

            if (_regen != null)
                StopCoroutine(_regen);

            _regen = StartCoroutine(RegenStamina());
        }
        else 
        {
            Debug.Log("TO DO - Not enough stamina message");
        }
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(2);

        while (_currentStamina < _maxStamina)
        {
            _currentStamina += _maxStamina / 100;
            Bar.value = _currentStamina;
        }
        
        _regen = null;
    }

}
