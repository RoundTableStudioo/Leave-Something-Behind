using System.Collections;
using RoundTableStudio.Sound;
using UnityEngine;
using UnityEngine.SceneManagement; //cambiar escenas

namespace RoundTableStudio.Menu {
    
    public class Menu : MonoBehaviour
    {
        private Animator _animator;
        private SoundManager _soundManager;

        private void Start() {
            _animator = GetComponent<Animator>();
            _soundManager = SoundManager.Instance;
        }

        public void PlayGame() //para comenzar el juego
        {
            _soundManager.Play("Button");
            _animator.SetTrigger(Animator.StringToHash("Play"));
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

        public IEnumerator OnAnimationFinish() {
            Sound.Sound sound = _soundManager.GetSound("MenuMusic");

            while (sound.Source.volume > 0) {
                sound.Source.volume -= 0.01f;
                yield return new WaitForSeconds(0.1f);
            }

            _soundManager.Stop("MenuMusic");
            
            //se pasa a la escena siguiente en la cola, entre parentesis se puede poner la escena que queremos directamente
            _soundManager.Play("IntroductionMusic");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void OnCreditsButton() {
            SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
        }
    }
}
