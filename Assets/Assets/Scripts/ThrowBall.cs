using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour {

    public Transform player;
    public Transform playerCam;
    public Transform cameraFollow;
    public float throwForce = 10f;

    public AudioClip throwsound1;
    public AudioClip throwsound2;
    public AudioClip throwsound3;
    public AudioClip PinDown;
    public AudioClip RollingBall;

    private AudioSource SFX;
    private float volumeLowRange = 0.5f;
    private float volumeHighRange = 1.0f;

    bool hasPlayer = false;
    bool beingCarried = false;
    public bool Thrown = false;

    public Transform BallSpawn;
    
    private bool start;
    public ScoreKeeper Scorekeeper;

    void Awake()
    {
        SFX = GetComponent<AudioSource>();
    }

    void Start()
    {
        playerCam.gameObject.SetActive(true);
        cameraFollow.gameObject.SetActive(false);
        GameObject foulwall =  GameObject.FindGameObjectWithTag("FoulWall");
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), foulwall.GetComponent<Collider>());
    }

    void Update()
    {
        float dist = Vector3.Distance(gameObject.transform.position, player.position);

        if (dist <= 0.85f)
        {
            hasPlayer = true;
        }
        else
        {
            hasPlayer = false;
        }

        if (hasPlayer && Input.GetMouseButtonDown(1))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = playerCam;
            beingCarried = true;
        }

        if (beingCarried)
        {
            
            transform.parent = playerCam;
 
            if (Input.GetMouseButtonDown(0))
            {

                start = true;
                transform.parent = null;
                playerCam.gameObject.SetActive(false);
                cameraFollow.gameObject.SetActive(true);
                Thrown = true;
                float vol = Random.Range(volumeLowRange, volumeHighRange);
                int SFXselector = Random.Range(1,4);

                if (SFXselector == 1)
                {
                    SFX.PlayOneShot(throwsound1, vol);
                    SFX.PlayOneShot(RollingBall, volumeLowRange);
                }
                else if (SFXselector == 2)
                {
                    SFX.PlayOneShot(throwsound2, vol);
                    SFX.PlayOneShot(RollingBall, volumeLowRange);
                }
                else
                {
                    SFX.PlayOneShot(throwsound3, vol);
                    SFX.PlayOneShot(RollingBall, volumeLowRange);
                }
                   
                    
                GetComponent<Rigidbody>().isKinematic = false;
                
                beingCarried = false;
                GetComponent<Rigidbody>().AddForce(playerCam.forward * throwForce);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                start = true;
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.L) && start) //reset in case of emergency
        {
                Reset(1);
                Scorekeeper.UpdateScore(1);
                beingCarried = false;
                start = false;
         
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Pin")
        {
            SFX.PlayOneShot(PinDown, volumeLowRange);
        }
    }

    public void Reset(object _ball)
    {
            Thrown = false;
            gameObject.transform.position = BallSpawn.position;
            gameObject.transform.rotation = BallSpawn.rotation;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            playerCam.gameObject.SetActive(true);
            cameraFollow.gameObject.SetActive(false);
    }

}

    

