using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {
    public float mySpeed = 10;
    public float myRange = 10;
    public float myStrength = 1;

    private float myDest;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-Vector3.up * Time.deltaTime * mySpeed, Space.World);
        myDest += Time.deltaTime * mySpeed;
        if (myDest > myRange)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.collider.gameObject.tag == "Enemy" || other.collider.gameObject.tag == "ground")
        {
            audio.Play();
            renderer.enabled = false;
            Destroy(gameObject, audio.clip.length);
        }
    }
}
