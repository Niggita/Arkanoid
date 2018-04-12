using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsHandler : MonoBehaviour {

	[SerializeField]
	private AudioSource EfxSource;
	[SerializeField]
	private AudioSource MusicSource;

	[SerializeField]
	private AudioClip MenuMusic;
	[SerializeField]
	private AudioClip GameMusic;

	[SerializeField]
	private AudioClip buttonSound;
	[SerializeField]
	private AudioClip enemySound;
	[SerializeField]
	private AudioClip platformSound;

	public static SoundsHandler instance = null;

	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
	}

	public void PlaySingle(AudioClip clip){
		EfxSource.clip = clip;
		EfxSource.Play ();
	}

	public void PlayMusic(AudioClip clip){
		MusicSource.clip = clip;
		MusicSource.Play ();
	}

	public void PlayMenuMusic(){
		PlayMusic (MenuMusic);
	}

	public void PlayGameMusic(){
		PlayMusic (GameMusic);
	}

	public void PlayButtonSound(){
		PlaySingle (buttonSound);
	}

	public void PlayEnemySound(){
		PlaySingle (enemySound);
	}

	public void PlayPlatformSound(){
		PlaySingle (platformSound);
	}

	public void SetVolume(float volume){
		MusicSource.volume = volume;
		EfxSource.volume = volume;
	}

	public float GetMusicVolume(){
		return MusicSource.volume;
	}

	public float GetEfxVolume(){
		return EfxSource.volume;
	}
}
