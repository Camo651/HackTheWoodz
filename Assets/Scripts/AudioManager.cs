using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public GameObject AudioSourcePrefab;


	public AudioClip Rocks;
	public AudioClip Wood;
	public AudioClip Bell;

	public void PlayAudio(AudioClip clip)
	{
		AudioSource source = Instantiate(AudioSourcePrefab, transform).GetComponent<AudioSource>();
		source.Play();
		StartCoroutine(Clipper(source));
	}

	IEnumerator Clipper(AudioSource a)
	{
		yield return null;
		while (a.isPlaying)
		{
			yield return null;
		}
		Destroy(a.gameObject);
	}
}
