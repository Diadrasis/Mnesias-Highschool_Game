using System;
using System.Collections;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private Vector3 targetPos;//the new position when we raycast on top of a gameObject

    public float speed = 2f;//the speed will have the player when playing the animation
    public Animator anim;//to start playing the animation of the go
    public bool facingRight = true;//bool to check when player is facing right or not
    Vector3 offset; //offset to get the sqrmagnitude for the distance when player reaches the hit position and play sound
    float soundDist;//local variable to hold offset sqrMagnitude

    [HideInInspector]
    public bool hasPlayed;//for the sound in order to play it once, when player reaches a specific distance from the target
    public HighSchoolManager manager;//manager for Audio and get other methods.
    string tempTransformName;//to hold the gameObject name when hit in order to play the correspoding audio

    public Transform originalPos;


    [HideInInspector]
    public bool isReset;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        targetPos = transform.position;
        hasPlayed = false;
        isReset = false;
        Physics.IgnoreLayerCollision(5, 5);
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 5 << 7;// to raycast on specific layers
        layerMask = ~layerMask;//we are raycasting on each layer except from player layer
        //Debug.Log("layerMask: " + layerMask);
        

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), targetPos, layerMask);//get the raycast on the layermask from mouse position
            
            if (hit.collider != null && hit.collider.gameObject.layer == 6 && hit.collider.tag == "Tone")
            {

                targetPos = new Vector2(hit.point.x, hit.point.y);

                tempTransformName = hit.transform.name;

                anim.SetBool("isWalking", true);

                if (hit.point.x < transform.position.x && facingRight)
                {
                    Flip();
                }
                else if (hit.point.x > transform.position.x && !facingRight)
                {
                    Flip();
                }
                Physics.GetIgnoreLayerCollision(5, 5);
            }
            /*else if (hit.collider != null && hit.collider.gameObject.layer == 5)
            {
                
            }*/

        }
        else
        {
            if (transform.position.x != targetPos.x)
            {

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPos.x, transform.position.y), speed * Time.deltaTime);
                transform.rotation = Quaternion.identity;                
                //Debug.Log("isStillPlaying");


                offset = transform.position - targetPos;
                soundDist = offset.sqrMagnitude;
                //Debug.Log("HasPlayed: "+hasPlayed+" reset: "+isReset);


                if (soundDist <= 1.0f && !hasPlayed)
                {

                    if (!manager.highSchool.sourceCam.isPlaying && !isReset && tempTransformName != string.Empty)
                    {
                        Debug.Log("here, check: " + tempTransformName);
                        PlayAudio(tempTransformName);

                        ShowImage(tempTransformName);
                        manager.ShowText(Int32.Parse(tempTransformName.Replace("Tone ", "")));

                        hasPlayed = true;
                    }

                }
                else
                {
                    ShowImage(tempTransformName);
                    hasPlayed = false;
                    manager.highSchool.sourceCam.volume = 1.0f;
                }

            }

            if (transform.position.x == targetPos.x)
            {

                anim.SetBool("isWalking", false);

            }

            
        }


    }

    //flip sprite depending on the target each time
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }

    //play audio when we raycasthit on specific gameobject and get it's name to play the corresponding audio
    void PlayAudio(string gameT)
    {
        foreach (AudioClip clip in manager.highSchool.clips)
        {
            if (clip.name.Contains(gameT))
            {
                manager.highSchool.sourceCam.clip = clip;
                manager.highSchool.sourceCam.PlayOneShot(clip);
                StartCoroutine(Cooldown(2.5f));
                Invoke("IsFinished", clip.length);

            }
            else
            {

                //Debug.Log("Is not contained");
            }

        }
    }

    //this method is used in order to close the panel after the clip has finished playing, is called where the clip is loaded
    void IsFinished()
    {
        manager.pnlInfoMain.SetActive(false);
    }

    //get the image, which has the same name as the audioclip in order to load it and show it on the info panel
    void ShowImage(string imgName)
    {
        foreach (Sprite item in manager.imageLoads)
        {
            if (item.name.Contains(imgName))
            {
                manager.imgForInfo.sprite = item;
            }
        }
    }

    //is used in order to have a cooldown before we show the infor panel of the specific instrument
    IEnumerator Cooldown(float num)
    {
        yield return new WaitForSeconds(num);
        manager.PanelInfoMethod();
        
    }
    public void ResetPosition()
    {
        isReset = true;
        if (isReset && !hasPlayed)
        {

            transform.position = targetPos;
            targetPos = originalPos.position;
            tempTransformName = string.Empty;
            manager.highSchool.sourceCam.volume = 0.0f;
            //Debug.Log("Reset: " + isReset);
            isReset = false;
        }
    }
}

