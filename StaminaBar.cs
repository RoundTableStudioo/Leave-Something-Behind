using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaBar;

    private int _maxStamina = 10;
    private int _currentStamina;

    private WaitForSeconds _regenTick = new WaitForSeconds(2f);
    private Coroutine _regen;

    public static StaminaBar instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _currentStamina = _maxStamina;
        staminaBar.maxValue = _maxStamina;
        staminaBar.value = _maxStamina;
    }

    public void UseStamina(int amount)
    {
        if (_currentStamina - amount >= 0)
        {
            _currentStamina -= amount;
            staminaBar.value = _currentStamina;

            if (_regen != null)
                StopCoroutine(_regen);

            _regen = StartCoroutine(RegenStamina());
        }
        else 
        {
            Debug.Log("No tienes suficiente estamina");
        }
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(2);

        while (_currentStamina < _maxStamina)
        {
            _currentStamina += _maxStamina / 100;
            staminaBar.value = _currentStamina;
            yield return _regenTick;
        }
        _regen = null;
    }

}
