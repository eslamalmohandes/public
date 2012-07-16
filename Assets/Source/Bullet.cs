using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
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
        transform.Translate(Vector3.right * Time.deltaTime * mySpeed/*, Space.World*/);
        myDest += Time.deltaTime * mySpeed;
        if (myDest > myRange)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.tag == "sanawnawBullet")
        {
            if (other.collider.gameObject.tag == "enemy")
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (other.collider.gameObject.tag == "sanawnaw")
            {
                Destroy(this.gameObject);
            }
        }
        
    }
}
