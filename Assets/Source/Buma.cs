using UnityEngine;
using System.Collections;

public class Buma : MonoBehaviour {
    public float yMax;
    public float yMin;
    public float speed;
    public float health = 5f;
    public float myScore = 1f;

    public Material[] materials;
    public float changeInterval = 0.33F;

    private int direction = 1;
    private float translation = 0f;

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

        translation = direction * speed * Time.deltaTime;

        if (transform.position.y + translation > yMax)
        {
            transform.position = new Vector3(transform.position.x, yMax, transform.position.z);
            direction *= -1;
        }
        else if (transform.position.y + translation < yMin)
        {
            transform.position = new Vector3(transform.position.x, yMin, transform.position.z);
            direction *= -1;
        }
        else
        {
            transform.Translate(Vector3.up * direction * speed * Time.deltaTime, Space.World);
        }

        //transform.Translate(Vector3.up * direction * speed * Time.deltaTime, Space.World);

        //if (transform.position.y <= yMin || transform.position.y >= yMax)
        //{
        //    direction *= -1;
        //}
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "sanawnawBullet")
        {
            health--;
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
