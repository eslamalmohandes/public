using UnityEngine;
using System.Collections;

public class Sanawnaw : MonoBehaviour {
    public float yMin = -157f;
    public float yMax = 426f;
    public float xMin = 0;
    public float xMax = 48600;

    public float speedX;
    public float speedY;
    public float health = 100;

    public GameObject fireEgg;
    public GameObject bombEgg;
    public Transform firePosition;
    public Transform bombPosition;

    public float timeBetweenFire;
    public int numberOfBombs;

    public Material[] materials;
    public float changeInterval = 0.33F;

    public AudioClip bulletClip;
    private AudioSource bulletAudio;

    public AudioClip bombClip;
    private AudioSource bombAudio;

    //public int lifes = 3;

    public Texture2D levelEnd;
    public Texture2D backToMainMenu;
    public Texture2D goOn;

    public Texture2D gameEnd;
    public Texture2D playAgain;

    private float timeForNextFire = 0f;
    [HideInInspector]
    public static float score = 0f;

    private static int level = 1;

    enum GameStates
    {
        Playing,
        LevelEnd,
        GameEnd
    }
    GameStates gameState = GameStates.Playing;

	// Use this for initialization
	void Start () {
        timeForNextFire = Time.time + timeBetweenFire;

        bulletAudio = gameObject.AddComponent<AudioSource>();
        bulletAudio.clip = bulletClip;
        bulletAudio.playOnAwake = false;

        bombAudio = gameObject.AddComponent<AudioSource>();
        bombAudio.clip = bombClip;
        bulletAudio.playOnAwake = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (gameState == GameStates.Playing)
        {
            if (materials.Length == 0)
                return;

            int index = (int)(Time.time / changeInterval);
            index = index % materials.Length;
            renderer.sharedMaterial = materials[index];

            handleUserInput();
        }
	}

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(0, yMin, 0), new Vector3(60000, yMin, 0));
        Gizmos.DrawLine(new Vector3(0, yMax, 0), new Vector3(60000, yMax, 0));
    }

    private void handleUserInput()
    {
        float translationX = /*Input.GetAxis("Horizontal") **/ speedX * Time.deltaTime;
        float translationY = Input.GetAxis("Vertical") * speedY * Time.deltaTime;

        if (transform.position.y + translationY < yMax && transform.position.y + translationY > yMin
            && transform.position.x + translationX > xMin && transform.position.x + translationX < xMax)
        {
            transform.Translate(translationX, translationY, 0, Space.World);
        }

        else if (transform.position.y + translationY < yMax && transform.position.y + translationY > yMin)
        {
            transform.Translate(0, translationY, 0, Space.World);
        }
        else if (transform.position.x + translationX > xMin && transform.position.x + translationX < xMax)
        {
            transform.Translate(translationX, 0, 0, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time > timeForNextFire)
            {
                timeForNextFire += timeBetweenFire;
                Instantiate(fireEgg, firePosition.position, firePosition.rotation);
                bulletAudio.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            //if (numberOfBombs > 0 && Time.time > timeForNextFire)
            //{
            //    numberOfBombs--;
                timeForNextFire += timeBetweenFire;
                Instantiate(bombEgg, bombPosition.position, this.gameObject.transform.rotation);
                bombAudio.Play();
            //}
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "bullet")
        {
            Bullet bullet = collider.gameObject.GetComponent<Bullet>();
            health -= bullet.myStrength;
            if (health < 0)
            {
                sanawnawIsDead();
            }
        }
        else if (collider.gameObject.tag == "enemy")
        {
            sanawnawIsDead();
        }
        else if (collider.gameObject.tag == "egg")
        {
            Egg egg = collider.gameObject.GetComponent<Egg>();
            score += egg.myScore;
        }
        else if (collider.gameObject.tag == "nest")
        {
            gameState = GameStates.LevelEnd;
        }
        else if (collider.gameObject.tag == "nestFinal")
        {
            gameState = GameStates.GameEnd;
        }
    }

    private void sanawnawIsDead()
    {
        //lifes--;
        //if (lifes >= 0)
        //{
            Application.LoadLevel(Application.loadedLevel);
        //}
        //else
        //{
        //    Application.LoadLevel(Application.loadedLevel);
        //}
        
    }

    void OnGUI()
    {
        if (gameState == GameStates.Playing)
        {
            GUI.TextArea(new Rect(5, 5, 80, 20), "Score: " + score);
            GUI.TextArea(new Rect(5, 30, 80, 20), "Health: " + health);
            GUI.TextArea(new Rect((Screen.width / 2 - 40), 5, 80, 20), "Level " + level + "/3");
        }
        else if (gameState == GameStates.LevelEnd)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), levelEnd);
            if (GUI.Button(new Rect( 18f / 1024 * Screen.width, 537f / 768 * Screen.height,
                307f / 1024 * Screen.width, 102f / 768 * Screen.height), goOn))
            {
                Debug.Log("go on");
                level++;
                Application.LoadLevel("Level" + level);
            }

            if (GUI.Button(new Rect(679f / 1024 * Screen.width, 547f / 768 * Screen.height,
                294f / 1024 * Screen.width, 92f / 768 * Screen.height), backToMainMenu))
            {
                Debug.Log("Main Menu");
                score = 0;
                level = 1;
                Application.LoadLevel("MainMenu");
            }
        }
        else if (gameState == GameStates.GameEnd)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), gameEnd);
            if (GUI.Button(new Rect(18f / 1024 * Screen.width, 537f / 768 * Screen.height,
                307f / 1024 * Screen.width, 102f / 768 * Screen.height), playAgain))
            {
                Debug.Log("play again");
                score = 0;
                level = 1;
                Application.LoadLevel("Level" + level);
            }

            if (GUI.Button(new Rect(404f / 1024 * Screen.width, 537f / 768 * Screen.height,
                263f / 1024 * Screen.width, 102f / 768 * Screen.height), backToMainMenu))
            {
                Debug.Log("Main Menu");
                score = 0;
                level = 1;
                Application.LoadLevel("MainMenu");
            }
        }
    }
}
