using System;
using RoundTableStudio.Sound;
using UnityEngine;
using UnityEngine.SceneManagement; //cambiar escenas

namespace RoundTableStudio.Menu {
    
    public class Menu : MonoBehaviour
    {
        private Animator _animator;
        private SoundManager _soundManager;

        private void Start() {
            _animator = GetComponentInParent<Animator>();
            _soundManager = SoundManager.Instance;
        }

        public void PlayGame() //para comenzar el juego
        {
            _soundManager.Play("Button");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //se pasa a la escena siguiente en la cola, entre parentesis se puede poner la escena que queremos directamente
        }

        public void QuitGame() //salir del juego
        {
            _soundManager.Play("Button");
            Debug.Log("Quit"); //para asegurarse de que esta funcionando
            Application.Quit(); 
        }

        public void OnOptionsButton() //menu de opciones 
        { 
            _soundManager.Play("Button");
            _animator.SetBool(Animator.StringToHash("Options"), true);
        }
    }
}
