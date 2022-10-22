using System;
using System.Collections.Generic;
using RoundTableStudio.Sound;
using UnityEngine;
using TMPro;

namespace RoundTableStudio.Menu {
	public class Settings : MonoBehaviour {
		public TMP_Dropdown ResolutionDropdown;

		private Animator _animator;
		private SoundManager _soundManager;
		private Resolution[] _resolutions;
		private bool _isFullScreen = true;

		private void Start() {
			_animator = GetComponentInParent<Animator>();
			_soundManager = SoundManager.Instance;

			_resolutions = Screen.resolutions;
			ResolutionDropdown.ClearOptions();

			List<string> options = new List<string>();
			int currentResolutionIndex = 0;

			for (int i = 0; i < _resolutions.Length; i++) {
				string option = _resolutions[i].width + "x" + _resolutions[i].height;
				options.Add(option);

				if (_resolutions[i].width == Screen.currentResolution.width &&
				    _resolutions[i].height == Screen.currentResolution.height)
					currentResolutionIndex = i;
			}
			
			ResolutionDropdown.AddOptions(options);
			ResolutionDropdown.value = currentResolutionIndex;
			ResolutionDropdown.RefreshShownValue();
		}

		public void SetResolution(int resolutionIndex) {
			Resolution resolution = _resolutions[resolutionIndex];
			
			Screen.SetResolution(resolution.width, resolution.height, _isFullScreen);
		}

		public void OnGeneralVolumeChange(float volume) {
			_soundManager.GeneralVolume = volume;

			_soundManager.UpdateMixerVolume();
		}
		
		public void OnMusicVolumeChange(float volume) {
			_soundManager.MusicVolume = volume;

			_soundManager.UpdateMixerVolume();
		}
		
		public void OnSoundEffectVolumeChange(float volume) {
			_soundManager.SoundEffectsVolume = volume;

			_soundManager.UpdateMixerVolume();
		}

		public void OnBackButton() {
			_soundManager.Play("Button");
			_animator.SetBool(Animator.StringToHash("Options"), false);
		}

		public void ChangeFullScreen(bool fullScreen) {
			_isFullScreen = fullScreen;
		}
	}
}
