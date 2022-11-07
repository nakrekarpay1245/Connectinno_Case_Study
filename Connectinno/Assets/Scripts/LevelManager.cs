using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [Header("TIME")]
    [SerializeField]
    private float levelTime;
    [SerializeField]
    private TextMeshProUGUI levelTimeText;

    //PRIVATE VARIABLES
    private bool isLevelFinished;

    public static LevelManager singleton;
    private void Awake()
    {
        if (!singleton)
        {
            singleton = this;
        }
    }
    private void Update()
    {
        DisplayTime();
    }

    /// <summary>
    /// Manage the time
    /// </summary>
    private void DisplayTime()
    {
        if (levelTime > 0)
        {
            levelTime -= Time.deltaTime;
            levelTimeText.text = (((int)(levelTime / 60)).ToString() + " : " + ((int)(levelTime % 60)).ToString());

            if (levelTime <= 15)
            {
                levelTimeText.color = Color.red;
            }
        }
        else
        {
            // Debug.Log("Level Failed");
            FinishLevel(false);
        }
    }

    ///// <summary>
    ///// Bir sonraki seviyeye gecmeyi saglar
    ///// </summary>
    //public void NextLevel()
    //{
    //    Manager.singleton.NextLevel();
    //}

    /// <summary>
    /// Makes transactions on the finish of the game (param: true = levelCompleted false = levelFailed)
    /// </summary>
    public void FinishLevel(bool isLevelCompleted)
    {
        if (!isLevelFinished)
        {
            Debug.Log("Level Finished");

            isLevelFinished = true;
            StartCoroutine(FinishLevelRoutine(isLevelCompleted));
        }
    }

    private IEnumerator FinishLevelRoutine(bool isLevelCompleted)
    {
        Debug.Log("Finish Level Coroutine");

        Manager.singleton.CoinEffect(10, Vector3.zero);
        //FinishFood();
        //yield return new WaitForSeconds(1.75f);
        //pan.transform.DOMoveY(-8, 0.15f);
        //yield return new WaitForSeconds(0.15f);
        //timeDisplay.transform.DOLocalMoveY(1400, 0.15f);
        //levelDisplay.transform.DOLocalMoveY(1410, 0.15f);
        //mealDisplay.transform.DOLocalMoveY(1250, 0.15f);
        //yield return new WaitForSeconds(0.25f);

        //if (isLevelCompleted)
        //{
        //    Debug.Log("Level Completed");
        //    levelCompletedDisplay.transform.DOScale(1, 0.25f);
        //    nextButton.transform.DOScale(1, 0.25f);
        //    menuButton.transform.DOScale(1, 0.25f);
        //}
        //else
        //{
        //    Debug.Log("Level Failed");
        //    levelFailedDisplay.transform.DOScale(1, 0.25f);
        //    replayButton.transform.DOScale(1, 0.25f);
        //    extraButton.transform.DOScale(1, 0.25f);
        //}
        yield return new WaitForSeconds(0.15f);
    }

    /// <summary>
    /// Makes transactions on the start of the game
    /// </summary>
    public void StartLevel()
    {
        //StartCoroutine(StartLevelRoutine());
    }

    private IEnumerator StartLevelRoutine()
    {
        yield return new WaitForSeconds(1);
        //timeDisplay.transform.DOLocalMoveY(900, 0.15f);
        //levelDisplay.transform.DOLocalMoveY(910, 0.15f);
        //mealDisplay.transform.DOLocalMoveY(750, 0.15f);
        //yield return new WaitForSeconds(0.15f);
        //pan.transform.DOMoveY(-3, 0.15f);
    }
}
