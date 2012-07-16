using UnityEngine;
using System.Collections;

public class Hunter : MonoBehaviour {
    public float myScore = 1f;
    public AudioClip shootClip;

    private AudioSource shootAudio;

	// Use this for initialization
	void Start () {
        shootAudio = this.gameObject.AddComponent<AudioSource>();
        shootAudio.playOnAwake = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "sanawnawBomb")
        {
            Sanawnaw.score += myScore;
            Destroy(this.gameObject);
        }
    }
}
