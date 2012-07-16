using UnityEngine;
using System.Collections;

public class FireField : MonoBehaviour {
    public GameObject bullet;
    public Transform firePosition;

    public float timeBetweenFire;

    private bool fire = false;
    private float timeForNextFire = 0f;

    // Use this for initialization
    void Start()
    {
        timeForNextFire = Time.time + timeBetweenFire;
    }
	
	// Update is called once per frame
	void Update () {
        if (fire && Time.time > timeForNextFire)
        {
            //if (Time.time > timeForNextFire)
            //{
                timeForNextFire += timeBetweenFire;
                Instantiate(bullet, firePosition.position, firePosition.rotation);
                audio.Play();
            //}
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "sanawnaw")
        {
            timeForNextFire = Time.time;
            fire = true;
            transform.parent.gameObject.SendMessage("enemyInRange", SendMessageOptions.DontRequireReceiver);
        }
            
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "sanawnaw")
            fire = false;
    }
}
