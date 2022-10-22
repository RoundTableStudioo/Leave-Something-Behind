using UnityEngine;
using UnityEngine.Audio;

namespace RoundTableStudio.Sound {
	[System.Serializable]
	public class Sound {
		[Tooltip("Name of the sound")]
		public string Name;
		[Tooltip("Type of the audio")]
		public AudioType Type;
		[Tooltip("Volume of the audio")]
		[Range(0f, 1f)]
		public float Volume;
		[Tooltip("Pitch of the audio")] 
		[Range(0.3f, 1f)]
		public float Pitch;
		[Tooltip("Will the audio loop?")] 
		public bool Loop;
		[Tooltip("Will the audio play when the game starts?")]
		public bool PlayOnAwake;
		[Tooltip("Audio to be played")] 
		public AudioClip Audio;

		[HideInInspector] 
		public AudioSource Source;

	}
}
