using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    [Header("TIME")]
    [Tooltip("The time that level is to end")]
    [SerializeField]
    private float levelTime;
    // Dynamic Time
    private float dynamicLevelTime;
    [Tooltip("The text for the display levelTime")]
    [SerializeField]
    private TextMeshProUGUI levelTimeText;

    [Header("Level Objects - WORLD")]
    [Tooltip("Shelf holding mealDishes")]
    [SerializeField]
    private GameObject mealShelf;
    [Tooltip("Pan cooking meals")]
    [SerializeField]
    private GameObject pan;

    [Header("Level Objects - UI")]
    [Tooltip("Shelf holding mealDishes")]
    [SerializeField]
    private GameObject levelDisplaysMenu;

    [Header("Level End Objects - UI")]
    [Tooltip("The thing to dipslay when you complete the level")]
    [SerializeField]
    private GameObject levelCompletedDisplay;
    [Tooltip("The button to ensure the next level")]
    [SerializeField]
    private GameObject nextButton;
    [Tooltip("The button opens the main menu")]
    [SerializeField]
    private GameObject menuButton;

    [Tooltip("The thing to dipslay when you fail the level")]
    [SerializeField]
    private GameObject levelFailedDisplay;
    [Tooltip("The button to restart the current level")]
    [SerializeField]
    private GameObject replayButton;
    [Tooltip("The button to inrease level lime 30 second")]
    [SerializeField]
    private GameObject extraButton;
    [Tooltip("Indicates that money is insufficient")]
    [SerializeField]
    private GameObject poorDisplay;

    [Header("SFXs")]
    [Tooltip("AudioSource for ingredient SFXs")]
    [SerializeField]
    private AudioSource audioSource;
    [Tooltip("audioClip for LevelComplete SFXs")]
    [SerializeField]
    private AudioClip levelCompleteSFX;
    [Tooltip("audioClip for LevelFail SFXs")]
    [SerializeField]
    private AudioClip levelFailSFX;
    // PRIVATE VARIABLES
    private bool isLevelFinished;

    //Singleton
    public static LevelManager singleton;
    private void Awake()
    {
        if (!singleton)
        {
            singleton = this;
        }
        ResetTime();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        DisplayTime();

        //GEÇİCİ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartLevel();
        }
    }

    /// <summary>
    /// Resets the Time
    /// </summary>
    public void ResetTime()
    {
        dynamicLevelTime = levelTime;
    }

    /// <summary>
    /// Manage the time
    /// </summary>
    private void DisplayTime()
    {
        if (dynamicLevelTime > 0)
        {
            dynamicLevelTime -= Time.deltaTime;
            levelTimeText.text = (((int)(dynamicLevelTime / 60)).ToString() + " : " + ((int)(dynamicLevelTime % 60)).ToString());

            if (dynamicLevelTime <= 15)
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

    /// <summary>
    /// Restarts the level
    /// </summary>
    public void ReplayLevel()
    {
        StartLevel();
    }

    /// <summary>
    /// Inreases the level lime 30 second
    /// </summary>
    public void ExtraButton()
    {
        if (Manager.singleton.CoinControl(5))
        {
            Manager.singleton.BuySomething(5);
            dynamicLevelTime += 30;
            levelTimeText.color = Color.white;

            levelFailedDisplay.transform.DOScale(0, 0.25f).SetEase(Ease.InBack); 
            replayButton.transform.DOScale(0, 0.25f).SetEase(Ease.InBack);
            extraButton.transform.DOScale(0, 0.25f).SetEase(Ease.InBack);

            pan.transform.DOMoveY(-2, 0.15f);
            mealShelf.transform.DOMoveY(-5.75f, 0.15f);

            levelDisplaysMenu.transform.DOLocalMoveY(0, 0.15f).SetEase(Ease.OutBack);

            isLevelFinished = false;
        }
        else
        {
            // Debug.Log("Paran yetmiyo Allahın fakiri");
            StartCoroutine(PoorDisplayRoutine());
        }
    }

    private IEnumerator PoorDisplayRoutine()
    {
        poorDisplay.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(1.25f);
        poorDisplay.transform.DOScale(0, 0.25f).SetEase(Ease.InBack);
        yield return new WaitForSeconds(0);
    }

    /// <summary>
    /// Inreases the level time specified amount
    /// </summary>
    public void ExtraButton(float _value)
    {
        dynamicLevelTime += _value;
        levelTimeText.color = Color.white;

        levelFailedDisplay.transform.DOScale(0, 0.25f).SetEase(Ease.InBack);
        replayButton.transform.DOScale(0, 0.25f).SetEase(Ease.InBack);
        extraButton.transform.DOScale(0, 0.25f).SetEase(Ease.InBack);

        pan.transform.DOMoveY(-2, 0.15f);
        mealShelf.transform.DOMoveY(-5.75f, 0.15f);

        levelDisplaysMenu.transform.DOLocalMoveY(0, 0.15f).SetEase(Ease.InBack);

        isLevelFinished = false;
    }

    /// <summary>
    /// Opens the main menu
    /// </summary>
    public void MenuButton()
    {
        Manager.singleton.OpenMainMenu();
        Manager.singleton.NextLevel();
    }


    /// <summary>
    /// allows to go to the next level
    /// </summary>
    public void NextLevelButton()
    {
        Manager.singleton.NextLevel();
        StartLevel();
    }

    /// <summary>
    /// Turns off out-menu objects
    /// </summary>
    public void HideLevelEndObjects()
    {
        levelCompletedDisplay.transform.DOScale(0, 0.25f).SetEase(Ease.InBack);
        nextButton.transform.DOScale(0, 0.25f).SetEase(Ease.InBack);
        menuButton.transform.DOScale(0, 0.25f).SetEase(Ease.InBack);
        levelFailedDisplay.transform.DOScale(0, 0.25f).SetEase(Ease.InBack);
        replayButton.transform.DOScale(0, 0.25f).SetEase(Ease.InBack);
        extraButton.transform.DOScale(0, 0.25f).SetEase(Ease.InBack);
    }

    /// <summary>
    /// Plays LevelCompleted SFX
    /// </summary>
    public void PlayLevelFailedSFX()
    {
        SetRandomPitch();
        audioSource.PlayOneShot(levelFailSFX);
    }

    /// <summary>
    /// Plays LevelFailed SFX
    /// </summary>
    public void PlayLevelCompleteSFX()
    {
        SetRandomPitch();
        audioSource.PlayOneShot(levelCompleteSFX);
    }

    /// <summary>
    /// Random Pitch for AudioSource
    /// </summary>
    public void SetRandomPitch()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
    }

    /// <summary>
    /// Makes transactions on the finish of the game (param: true = levelCompleted false = levelFailed)
    /// </summary>
    /// <param name="isLevelCompleted"></param>
    public void FinishLevel(bool isLevelCompleted)
    {
        if (!isLevelFinished)
        {
            Debug.Log("Level Finished");

            isLevelFinished = true;
            StartCoroutine(FinishLevelRoutine(isLevelCompleted));
        }
    }

    /// <summary>
    /// Makes transactions on the finish of the game (param: true = levelCompleted false = levelFailed)
    /// </summary>
    /// <param name="isLevelCompleted"></param>
    /// <returns></returns>
    private IEnumerator FinishLevelRoutine(bool isLevelCompleted)
    {
        Debug.Log("Finish Level Coroutine");

        if (isLevelCompleted)
        {
            Debug.Log("Level Completed");
            PlayLevelCompleteSFX();
            Manager.singleton.ControlChest();
            levelCompletedDisplay.transform.DOScale(1, 0.25f).SetEase(Ease.OutBack);
            nextButton.transform.DOScale(1, 0.25f).SetEase(Ease.OutBack);
            menuButton.transform.DOScale(1, 0.25f).SetEase(Ease.OutBack);
            Manager.singleton.CoinEffect(10, Vector3.zero);
            IngredientManager.singleton.FinishGeneratedIngredients();
        }
        else
        {
            Debug.Log("Level Failed");
            PlayLevelFailedSFX();
            levelFailedDisplay.transform.DOScale(1, 0.25f).SetEase(Ease.OutBack);
            replayButton.transform.DOScale(1, 0.25f).SetEase(Ease.OutBack);
            extraButton.transform.DOScale(1, 0.25f).SetEase(Ease.OutBack);
        }
        yield return new WaitForSeconds(0.15f);
        pan.transform.DOMoveY(-8, 0.15f);
        mealShelf.transform.DOMoveY(-11, 0.15f);

        yield return new WaitForSeconds(0.15f);
        levelDisplaysMenu.transform.DOLocalMoveY(1920, 0.15f);

        yield return new WaitForSeconds(0.05f);
    }

    /// <summary>
    /// Makes transactions on the start of the game
    /// </summary>
    public void StartLevel()
    {
        if (isLevelFinished)
        {
            isLevelFinished = false;
        }
        StartCoroutine(StartLevelRoutine());
    }

    /// <summary>
    /// Makes transactions on the start of the game
    /// </summary>
    private IEnumerator StartLevelRoutine()
    {
        Debug.Log("Start Level Coroutine");
        ResetTime();

        yield return new WaitForSeconds(0.15f);
        levelCompletedDisplay.transform.DOScale(0, 0.25f).SetEase(Ease.InBack);
        nextButton.transform.DOScale(0, 0.25f).SetEase(Ease.InBack);
        menuButton.transform.DOScale(0, 0.25f).SetEase(Ease.InBack);
        levelFailedDisplay.transform.DOScale(0, 0.25f).SetEase(Ease.InBack);
        replayButton.transform.DOScale(0, 0.25f).SetEase(Ease.InBack);
        extraButton.transform.DOScale(0, 0.25f).SetEase(Ease.InBack);

        yield return new WaitForSeconds(0.15f);
        pan.transform.DOMoveY(-2, 0.15f);
        mealShelf.transform.DOMoveY(-5.75f, 0.15f);

        yield return new WaitForSeconds(0.15f);
        levelDisplaysMenu.transform.DOLocalMoveY(0, 0.15f);
        Manager.singleton.MoveLevelDisplay(new Vector3(0, 900, 0));

        yield return new WaitForSeconds(0.25f);
        IngredientManager.singleton.GenerateIngredients(100);
        MealManager.singleton.GenerateMeal();
        yield return new WaitForSeconds(0);
    }
}
