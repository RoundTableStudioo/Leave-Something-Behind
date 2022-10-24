using UnityEngine;
using UnityEngine.SceneManagement;

namespace RoundTableStudio.Menu {
	public class Credits : MonoBehaviour {

		public void GoBackToMenu() {
			SceneManager.LoadScene(0);
		}
	}
}
