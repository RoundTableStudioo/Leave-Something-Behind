using System;
using UnityEngine.Audio;
using UnityEngine;

namespace RoundTableStudio.Sound {
	public enum AudioType {
		Music, 
		SoundEffect
	}
	public class SoundManager : MonoBehaviour {
		public static SoundManager Instance;

		public AudioMixerGroup GeneralMixerGroup;
		public AudioMixerGroup MusicMixerGroup;
		public AudioMixerGroup SoundEffectMixerGroup;

		public Sound[] Sounds;

		private void Awake() {
			if (Instance != null) return;

			Instance = this;

			DontDestroyOnLoad(gameObject);

			foreach (Sound s in Sounds) {
				s.Source = gameObject.AddComponent<AudioSource>();
				s.Source.clip = s.Audio;
				s.Source.volume = s.Volume;
				s.Source.pitch = s.Pitch;

				if (s.Type == AudioType.Music)
					s.Source.outputAudioMixerGroup = MusicMixerGroup;
				else if (s.Type == AudioType.SoundEffect)
					s.Source.outputAudioMixerGroup = SoundEffectMixerGroup;

				if (!s.PlayOnAwake) return;

				s.Source.Play();
			}
		}

		public void Play(string name) {
			Sound s = Array.Find(Sounds, sound => sound.Name == name);

			if (s == null) {
				Debug.LogError("Sound " + name + " not found or doesn't exist");
				return;
			}

			s.Source.Play();
		}

		public void Stop(string name) {
			Sound s = Array.Find(Sounds, sound => sound.Name == name);

			if (s == null) {
				Debug.LogError("Sound " + name + " not found or doesn't exist");
				return;
			}

			s.Source.Stop();
		}

		public void UpdateMixerVolume() {
			// TO DO
		}

	}
}
