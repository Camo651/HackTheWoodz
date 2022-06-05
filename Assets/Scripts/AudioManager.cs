using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public GameObject AudioSourcePrefab;
	public AudioSource helpUO;

	public AudioClip Rocks;
	public AudioClip Wood;
	public AudioClip Bell;
	public AudioClip Leaf;
	public AudioClip Dirt;
	public void Play(int index)
	{
		switch (index)
		{
			case 0:PlayAudio(Rocks);break;
			case 1:PlayAudio(Wood);break;
			case 2:PlayAudio(Bell);break;
			case 3:PlayAudio(Leaf);break;
			case 4:PlayAudio(Dirt);break;
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
		a.pitch = a.pitch + (Random.value > .5 ? 1 : -1) * Random.value * .2f;
		yield return new WaitForSeconds(Random.value*.3f);
		a.Play();
		while (a.isPlaying)
		{
			yield return null;
		}
		Destroy(a.gameObject);
	}
}
