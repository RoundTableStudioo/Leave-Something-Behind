using RoundTableStudio.Sound;
using UnityEngine;
using UnityEngine.SceneManagement; //cambiar escenas

namespace RoundTableStudio.UI {
    
    public class Menu : MonoBehaviour
    {
        public GameObject Options;

        private void Start() {
            SoundManager.Instance.Play("MenuMusic");
        }

        public void PlayGame() //para comenzar el juego
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //se pasa a la escena siguiente en la cola, entre parentesis se puede poner la escena que queremos directamente
        }

        public void QuitGame() //salir del juego
        {
            Debug.Log("Quit"); //para asegurarse de que está funcionando
            Application.Quit(); 
        }

        public void OptionsOn() //menu de opciones 
        {
            Options.SetActive(true); //aparece
        }

        public void OptionsOff()
        {
            Options.SetActive(false); //desaparece
        }

        public void Return() //regresar
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); //cargar la escena anterior en la cola
        }
    }
}
