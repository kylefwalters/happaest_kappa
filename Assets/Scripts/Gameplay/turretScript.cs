﻿using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class turretScript : MonoBehaviour
{
    public Rigidbody2D rb;

    //script trackers
    public sBossHealth bossHp;
    public enemyhealth parentScript;
    //location int for boss handeler
    public int location;
    //fire select
    public bool laser;
    public bool missle;
    private bool firing;
    //tracking
    public Transform player;
    //how long the fire phase should last
    public float fireTime;
    private bool oneShot; //used to trigger variables once per fireing sequence
    //prefab array
    public GameObject[] bullets;
    //fire ports
    public Transform port1;
    public Transform port2;

    public float refire;

    private bool swapBool;
    private float tweenSpeed;
    public float tweentime = 0.08f;
    private bool boop = false;


    public void die() 
    {
        if (boop == false)
        {
            //retrigger prevention
            boop = true;
            //tell boss tracker that a turret is dead
            bossHp.updateVarsTurret();
            Debug.Log("turret died");
        }
    }
    public void fire()
    {
        if (laser == true)//should be only one to freeze rotation
        {
            if (oneShot == false)
            {
                //spawn bullet
                Instantiate(bullets[0], port1.position, port1.rotation, parent: port1);
                fireTime = 7.5f;
                firing = true;
            }
            //retrigger limmiter
            oneShot = true;
        }
        else if (missle == true)
        {
            if (oneShot == false)
            {
                fireTime = 3;
                refire = 1.2f;
            }
            //retrigger limmiter
            oneShot = true;
            //fire while in firetime
            if (fireTime > 0 && refire<=0)
            {
                //set fire rate
                refire = 1.2f;
                //select port
                if (swapBool == false)
                {
                    //spawn bullet
                    Instantiate(bullets[1], port1.position, port1.rotation);
                    //swap fireport
                    swapBool = true;
                }
                else if (swapBool == true) 
                {
                    //spawn bullet
                    Instantiate(bullets[1], port2.position, port2.rotation);
                    //swap fireport
                    swapBool = false;
                }
            }
        }
        else //cannon
        {
            if (oneShot == false)
            {
                fireTime = 3;
                refire = 0.9f;
            }
            //retrigger limmiter
            oneShot = true;
            //fire for remaining fire time
            if (fireTime > 0 && refire <= 0)
            {
                refire = 0.9f;
                //spawn bullet
                Instantiate(bullets[2], port1.position, port1.rotation);

            }
        }
    }
    void Start()
    {
        firing = false;
        oneShot = false;
    }
    void Update()
    {
        //degrade timers
        refire -= 2 * Time.deltaTime;
        fireTime -= 1 * Time.deltaTime;
        //freze movement while firing
        if (firing == false)
        {
            //get object with tag
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            //set rotation
            Vector3 direction = player.position - transform.position;
            float targetAangle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetAangle, ref tweenSpeed, tweentime);
            rb.rotation = angle;
        }
        if (fireTime > 0) { fire(); }//fire while fireing WOW
        if (fireTime <= 0) { oneShot = false; firing = false; }//reset when not fireing

    }
}
