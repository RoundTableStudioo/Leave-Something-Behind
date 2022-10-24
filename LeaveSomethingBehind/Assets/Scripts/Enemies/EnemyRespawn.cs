using System.Collections.Generic;
using System.Collections;
using RoundTableStudio.Shared;
using UnityEngine;

namespace RoundTableStudio.Core {
	public class EnemyRespawn : MonoBehaviour {
		[Space(10)]
		[Header("Human enemies")]
		[Tooltip("Water Mage Prefab")]
		public GameObject WaterMage;
		[Tooltip("Fire Mage Prefab")] 
		public GameObject FireMage;
		[Tooltip("Wind Mage Prefab")] 
		public GameObject WindMage;
		[Tooltip("Ground Mage Prefab")] 
		public GameObject GroundMage;
		
		
		[Space(10)]
		[Header("Other enemies")]
		[Tooltip("Goblin enemy")] 
		public GameObject Goblin;
		[Tooltip("Enemies that are orcs")]
		public List<GameObject> Orcs;
		
		[Space(10)]
		[Header("Values")]
		[Tooltip("Total game time")]
		public int MinutesToEnd = 15;
		[Tooltip("Number of game phases")] 
		public int PhasesNumber = 3;
		[Tooltip("Max enemy number")] 
		public int MaxEnemyNumber = 25;
		[Space(10)]
		[Header("Unity fields")]
		[Tooltip("Timer game object")]
		public Timer Timer;
		[Tooltip("Map game object")]
		public MapGenerator Map;
		
		
		private int _currentPhase;
		private int _minutesPerWave;
		private int _minutesPerPhase;
		[HideInInspector]
		public int EnemyCount;
		private Vector3Int _playerPosition;
		private bool _respawning;

		private void Start() {
			_currentPhase = 1;
			_minutesPerPhase = MinutesToEnd / PhasesNumber;
			_minutesPerWave = _minutesPerPhase / 2;

			_respawning = false;
			EnemyCount = 0;
		}

		private void Update() {
			_playerPosition = Map.PlayerCellPosition;

			if (!_respawning && EnemyCount < MaxEnemyNumber) {
				_respawning = true;
				RespawnEnemies();
			}
		}

		private void RespawnEnemies() {
			if (Timer.MinutesCount == _minutesPerPhase * _currentPhase) {
				_currentPhase++;
			}
			
			switch (_currentPhase) {
				case 1:
					if (Timer.MinutesCount % (_minutesPerPhase * _currentPhase) <= 2) {
						//StartCoroutine(SpawnEnemy(3, 2, Goblin));
						//StartCoroutine(SpawnEnemy(4, 4, WindMage));
					}
					else {
						StartCoroutine(SpawnEnemy(2, 10, Orcs[0]));
						StartCoroutine(SpawnEnemy(2, 5, FireMage));
					}
					break;
				
				case 2:
					if (Timer.MinutesCount % (_minutesPerPhase * _currentPhase) <= 7) {
						StartCoroutine(SpawnEnemy(5, 3, Goblin));
						StartCoroutine(SpawnEnemy(2, 5, WaterMage));
					}
					else {
						StartCoroutine(SpawnEnemy(4, 15, Orcs[0]));
						StartCoroutine(SpawnEnemy(2, 5, GroundMage));
					}
					break;
				
				case 3:
					if (Timer.MinutesCount % (_minutesPerPhase * _currentPhase) <= 12) {
						StartCoroutine(SpawnEnemy(4, 3, Goblin));
						StartCoroutine(SpawnEnemy(2, 5, FireMage));
					}
					else {
						StartCoroutine(SpawnEnemy(3, 12, Orcs[0]));
						StartCoroutine(SpawnEnemy(2, 5, WindMage));
						StartCoroutine(SpawnEnemy(2, 5, WaterMage));
					}
					break;
			}
		}

		private IEnumerator SpawnEnemy(int number, float frequency, GameObject enemy) {
			for (int i = 0; i < number; i++) {
				int randomXPosition, randomYPosition;
				
				do {
					randomXPosition = Random.Range(-1, 2) * 6 + _playerPosition.x;
					randomYPosition = Random.Range(-1, 2) * 6 + _playerPosition.y;
				} while (randomXPosition == _playerPosition.x && randomYPosition == _playerPosition.y);

				Vector3 pos = new Vector3(randomXPosition, randomYPosition);

				Instantiate(enemy, pos, Quaternion.identity);
				EnemyCount++;

				if (EnemyCount >= MaxEnemyNumber) break;
				
				yield return new WaitForSeconds(frequency);
			}
			
			_respawning = false;
		}


	}
}
