using UnityEngine;
using System.Collections;

public class Eagle : MonoBehaviour {
    public float speed;
    public float health = 5;
    public float myScore = 1f;

    public Material[] materials;
    public float changeInterval = 0.33F;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (materials.Length == 0)
            return;

        int index = (int)(Time.time / changeInterval);
        index = index % materials.Length;
        renderer.sharedMaterial = materials[index];


        if (this.transform.position.x > 0)
        {
            transform.Translate(-Vector3.right * Time.deltaTime * speed, Space.World);
        }
        else
        {
            Destroy(this.gameObject);   
        }
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "sanawnawBullet")
        {
            health --;
            if (health < 0)
            {
                Sanawnaw.score += myScore;
                Destroy(this.gameObject);
            }
        }
    }

    public void enemyInRange()
    {
        if (!audio.isPlaying)
            audio.Play();
    }
}
