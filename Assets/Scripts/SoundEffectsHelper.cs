using UnityEngine;
using System.Collections;

/// <summary>
/// Creating instance of sounds from code with no effort
/// </summary>
public class SoundEffectsHelper : MonoBehaviour
{
	
	/// <summary>
	/// Singleton
	/// </summary>
	public static SoundEffectsHelper Instance;
	
	public AudioClip explosionSound;
	public AudioClip playerShotSound;
	public AudioClip enemyShotSound;
	public AudioClip ObstructExplosionSound;
    public AudioClip cheeringSound;

    void Awake()
	{
		// Register the singleton
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of SoundEffectsHelper!");
		}
		Instance = this;
	}
	
	public void MakeExplosionSound()
	{
		MakeSound(explosionSound);
	}
	
	public void MakePlayerShotSound()
	{
        MakeSound(playerShotSound);
	}
	
	public void MakeEnemyShotSound()
	{
		MakeSound(enemyShotSound);
	}
    public void MakeCheeringSound()
    {
        MakeSound(cheeringSound);
    }
    public void MakeObstructExplosionSound()
	{
		MakeSound (ObstructExplosionSound);
	}
	
	/// <summary>
	/// Play a given sound
	/// </summary>
	/// <param name="originalClip"></param>
	private void MakeSound(AudioClip originalClip)
	{
        // As it is not 3D audio clip, position doesn't matter.

         //AudioSource.PlayClipAtPoint(originalClip, transform.position,100.0f);
        AudioSource.PlayClipAtPoint(originalClip, new Vector3(0, 0, -10),1f);
        ////Vector3 cameraZPos = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
        //AudioSource.PlayClipAtPoint(originalClip, cameraZPos, 5f);
    }
}