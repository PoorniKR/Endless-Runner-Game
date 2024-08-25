using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    private Touch initialTouch = new Touch();
    private float distance = 0;
    private bool hasSwiped = false;

    public bool jump = false;
    public bool slide  = false;
    public Animator anim;

    public GameObject trigger;

    public float score = 0;
    

    public bool boost = false;
    public Rigidbody rbody;
    public CapsuleCollider myCollider;

    public bool death = false;
    public Image gameOverImg;
    public Text scoreText;
    public Text bestScoreText;
    public Text UsernameInput;
    public float lastScore;
    public AudioSource deathsong;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        myCollider = GetComponent<CapsuleCollider>();

        lastScore = PlayerPrefs.GetFloat("MyScore");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        foreach (Touch t in Input.touches)
        {
            if (t.phase == TouchPhase.Began)
            {
                initialTouch = t;

            }
            else if (t.phase == TouchPhase.Moved && !hasSwiped)
            {
                float deltaX = initialTouch.position.x - t.position.x;
                float deltaY = initialTouch.position.y - t.position.y;
                distance = Mathf.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                bool swipedSideWay = Mathf.Abs(deltaX) > Mathf.Abs(deltaY);

                if (distance > 100f)
                {
                    if (swipedSideWay && deltaX > 0)
                    {
                        //swipe left

                    }
                    if (swipedSideWay && deltaX <= 0)
                    {
                        //swipe right

                    }
                    if (!swipedSideWay && deltaY > 0)
                    {
                        //swipe down
                        slide = true;
                        StartCoroutine(SlideController());

                    }
                    if (!swipedSideWay && deltaY <= 0)
                    {
                        //swipe up
                        jump = true;
                        StartCoroutine(JumpController());
                    }
                    hasSwiped = true;

                }
            }
            else if (t.phase == TouchPhase.Ended)
            {
                initialTouch = new Touch();
                hasSwiped = false;
            }


        }


        scoreText.text = score.ToString();

        if (score > lastScore)
        {
            bestScoreText.text= "Your Best Score:" +score.ToString();
        }
        else
        {
            bestScoreText.text = "Your Score:" + score.ToString();
        }

        if (death == true)
        {
            gameOverImg.gameObject.SetActive(true);
        }
        //Player control start
        if (score >= 100 && death != true)
        {
            transform.Translate(0, 0, 0.5f);
        }
        else if (score >= 200 && death != true)
        {
            transform.Translate(0, 0, 0.6f);
        }
        else if (score >= 500 && death != true)
        {
            transform.Translate(0, 0, 0.7f);
        }
        else if (score >= 1000 && death != true)
        {
            transform.Translate(0, 0, 0.8f);
        }
        else if (score >= 4000 && death != true)
        {
            transform.Translate(0, 0, 0.9f);
        }
        else if (score >= 10000 && death != true)
        {
            transform.Translate(0, 0, 1f);
        }
        else if (death == true)
        {
            transform.Translate(0, 0, 0);
            deathsong.Play();
            jump = false;
            slide = false;
        }
        else
        {
            transform.Translate(0, 0, 0.3f);
        }


        if (boost == true)
        {
            transform.Translate(0, 0, 1f);
            myCollider.enabled = false;
            rbody.isKinematic=true;

        }
        else
        {
            myCollider.enabled = true;
            rbody.isKinematic = false;
        }

        
        if (Input.GetKey(KeyCode.Space))
        {
            jump = true;
            StartCoroutine(JumpController());
        }
        //else
        //{
        //    jump = false;
        //}
        if (Input.GetKey(KeyCode.DownArrow))
        {
            slide = true;
            StartCoroutine(SlideController());
        }
        //else
        //{
        //    slide = false;
        //}


        if (jump == true)
        {
            anim.SetBool("IsJump", jump);
            transform.Translate(0, 0.3f, 0.1f);
        }
        else if (jump == false)
        {
            anim.SetBool("IsJump", jump);
            //transform.Translate(0, 0, 0.1f);
        }
        if (slide == true)
        {
            anim.SetBool("IsSlide", slide);
            transform.Translate(0, 0, 0.1f);
            myCollider.height = 1f;

        }
        else if (slide == false)
        {
            anim.SetBool("IsSlide", slide);
            myCollider.height = 2.05f;
            //transform.Translate(0, 0, 0.1f);
        }
        trigger = GameObject.FindGameObjectWithTag("Obstacle");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerTrigger")
        {
            Destroy(trigger.gameObject);
        }
        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject,0.5f);
            score += 5f;
        }
        if (other.gameObject.tag == "Boost")
        {
            Destroy(other.gameObject);
            StartCoroutine(BoostController());
            score += 50f; 
        }
        if (other.gameObject.tag == "DeathPoint")
        {
            death = true;
            UsernameInput.text=PlayerPrefs.GetString("UsernameInput");
            StartCoroutine(Main.Instance.Web.InsertScore(UsernameInput.text, score));
            
            if (score > lastScore)
            {
                PlayerPrefs.SetFloat("MyScore",score);
            }
            
        }
    }

    IEnumerator BoostController()
    {
        boost = true;
        yield return new WaitForSeconds(1);
        boost = false; 
    }

    IEnumerator JumpController()
    {
        jump = true;
        yield return new WaitForSeconds(0.2f);
        jump = false;
    }

    IEnumerator SlideController()
    {
        slide = true;
        yield return new WaitForSeconds(0.7f);
        slide = false;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(2);
    }

}
