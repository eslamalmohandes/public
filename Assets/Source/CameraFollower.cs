using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour {
    public Transform target;
    public float advance = 100f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(target.position.x + advance, target.position.y, this.transform.position.z);
	}
}
