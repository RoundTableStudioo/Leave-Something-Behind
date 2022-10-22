using System.Collections.Generic;
using System.Collections;
using RoundTableStudio.Shared;
using UnityEngine;

namespace RoundTableStudio.Core {
	public class EnemyRespawn : MonoBehaviour {
		[Space(10)]
		[Header("Enemies Lists")]
		[Tooltip("Enemies that are human")]
		public List<GameObject> HumanEnemies;
		[Tooltip("Enemies that are elves")]
		public List<GameObject> ElfEnemies;
		[Tooltip("Enemies that are goblins")] 
		public List<GameObject> GoblinEnemies;
		[Tooltip("Enemies that are orcs")]
		public List<GameObject> OrcEnemies;
		[Space(10)]
		[Header("Values")]
		[Tooltip("Total game time")]
		public int MinutesToEnd = 30;
		[Tooltip("Number of game phases")] 
		public int PhasesNumber = 3;
		[Tooltip("Max enemy number")] 
		public int MaxEnemyNumber = 25;
		
		private int _currentPhase;
		private int _minutesPerWave;
		private int _minutesPerPhase;
		private int _enemyCount;
		private Timer _timer;
		private GridGenerator _grid;
		private Vector3Int _playerPosition;
		private bool _respawning;

		private void Start() {
			_timer = GetComponent<Timer>();
			_grid = GetComponent<GridGenerator>();
			
			_currentPhase = 1;
			_minutesPerPhase = MinutesToEnd / PhasesNumber;
			_minutesPerWave = _minutesPerPhase / 2;

			_respawning = false;
			_enemyCount = 0;
		}

		private void Update() {
			if(!_respawning && _enemyCount < MaxEnemyNumber)
				RespawnEnemies();
		}

		private void RespawnEnemies() {
			if (_timer.MinutesCount == _minutesPerPhase * _currentPhase) {
				_currentPhase++;
			}
			
			switch (_currentPhase) {
				case 1:
					if (_timer.MinutesCount % (_minutesPerPhase * _currentPhase) <= 5) {
						_respawning = true;
						StartCoroutine(SpawnHuman(3, 15));
						StartCoroutine(SpawnGoblin(4, 10));
					}
					else {
						
					}
					break;
				case 2:
					if (_timer.MinutesCount % (_minutesPerPhase * _currentPhase) <= 15) {
						
					}
					else {
						
					}
					break;
				case 3:
					if (_timer.MinutesCount % (_minutesPerPhase * _currentPhase) <= 25) {
						
					}
					else {
						
					}
					break;
			}
		}

		private IEnumerator SpawnHuman(int number, float frequency) {
			int humanType = Random.Range(0, HumanEnemies.Count);

			for (int i = 0; i < number; i++) {
				int randomXPosition, randomYPosition;
				
				do {
					_playerPosition = _grid.PlayerCellPosition;
					randomXPosition = Random.Range(-1, 2) * 6 + _playerPosition.x;
					randomYPosition = Random.Range(-1, 2) * 6 + _playerPosition.y;
				} while (randomXPosition == 0 && randomYPosition == 0);

				Vector3 pos = new Vector3(randomXPosition, randomYPosition);

				Instantiate(HumanEnemies[humanType], pos, Quaternion.identity);
				_enemyCount++;
			}

			yield return new WaitForSeconds(frequency);
			_respawning = false;
		}
		
		private IEnumerator SpawnGoblin(int number, int frequency) {
			int goblinType = Random.Range(0, GoblinEnemies.Count);
			
			for (int i = 0; i < number; i++) {
				int randomXPosition, randomYPosition;
				
				do {
					_playerPosition = _grid.PlayerCellPosition;
					randomXPosition = Random.Range(-1, 2) * 6 + _playerPosition.x;
					randomYPosition = Random.Range(-1, 2) * 6 + _playerPosition.y;
				} while (randomXPosition == 0 && randomYPosition == 0);

				Vector3 pos = new Vector3(randomXPosition, randomYPosition);

				Instantiate(GoblinEnemies[goblinType], pos, Quaternion.identity);
				_enemyCount++;
			}

			yield return new WaitForSeconds(frequency);
			_respawning = false;
		}
		
		private IEnumerator SpawnElf(int number, int frequency) {
			yield return new WaitForSeconds(frequency);

			int elfType = Random.Range(0, ElfEnemies.Count);
		}
		
		private IEnumerator SpawnOrc(int number, int frequency) {
			yield return new WaitForSeconds(frequency);

			int orcType = Random.Range(0, OrcEnemies.Count);
		}
		
		
	}
}
