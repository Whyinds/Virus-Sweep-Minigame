using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyAudio : MonoBehaviour
{
    public GameObject audioObjectToSpawn;

    public void OnDelete()
    {
        var audioObject = Instantiate(audioObjectToSpawn);
        Destroy(audioObject, audioObject.GetComponent<AudioSource>().clip.length + 0.1f);
    }
}
