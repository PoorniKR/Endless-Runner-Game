using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float tansitionTime = 1f;

    private Touch initialTouch = new Touch();
    //private float distance = 0;
    private bool hasSwiped = false;

    // Update is called once per frame
    void Update()
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
               // distance = Mathf.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                bool swipedSideWay = Mathf.Abs(deltaX) > Mathf.Abs(deltaY);

                //if (Input.GetMouseButtonDown(0)
                if (!swipedSideWay && deltaY <= 0)
                {
                    LoadNextLevel();
                }
            }
        }
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(tansitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
