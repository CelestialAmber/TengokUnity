using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
	AudioSource audioSource;

	void Start(){
		audioSource = GetComponent<AudioSource>();
	}

	public void PlaySound(AudioClip clip, float volume = 1f){
		audioSource.PlayOneShot(clip,volume);
	}

	public void Stop(){
		audioSource.Stop();
	}
}
