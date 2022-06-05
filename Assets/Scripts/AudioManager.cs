using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public GameObject AudioSourcePrefab;


	public AudioClip Rocks;
	public AudioClip Wood;
	public AudioClip Bell;
	public void Play(int index)
	{
		switch (index)
		{
			case 0:PlayAudio(Rocks);break;
			case 1:PlayAudio(Wood);break;
			case 2:PlayAudio(Bell);break;
		}
	}
	public void PlayAudio(AudioClip clip)
	{
		AudioSource source = Instantiate(AudioSourcePrefab, transform).GetComponent<AudioSource>();
		source.clip = clip;
		StartCoroutine(Clipper(source));
	}

	IEnumerator Clipper(AudioSource a)
	{
		a.pitch = a.pitch + (Random.value > .5 ? 1 : -1) * Random.value * .1f;
		yield return new WaitForSeconds(Random.value*.8f);
		a.Play();
		while (a.isPlaying)
		{
			yield return null;
		}
		Destroy(a.gameObject);
	}
}
