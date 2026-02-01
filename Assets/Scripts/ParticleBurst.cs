using UnityEngine;

public class ParticleBurst : MonoBehaviour
{
	public ParticleSystem bubbles;

	public void PlayBurst()
	{
		bubbles.Play();
	}
}
