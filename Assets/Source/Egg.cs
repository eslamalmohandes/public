using UnityEngine;
using System.Collections;

public class Egg : MonoBehaviour {
    public float myScore = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "sanawnaw")
        {
            audio.Play();
            //Destroy(this.gameObject);
            this.renderer.enabled = false;
            Destroy(this.gameObject, audio.clip.length);
        }
    }
}
