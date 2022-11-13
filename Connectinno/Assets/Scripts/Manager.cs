using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Manager : MonoBehaviour
{
    [Header("COIN")]
    [Tooltip("Total coin count player have")]
    [SerializeField]
    private int coinCount;
    [Tooltip("Total coin count display player have")]
    [SerializeField]
    private TextMeshProUGUI coinText;
    [Tooltip("Holds the coinCounText")]
    [SerializeField]
    private GameObject coinDisplay;

    [Tooltip("Coin object prefab for coinEffect")]
    [SerializeField]
    private GameObject coinPrefab;
    [Tooltip("Canvas on the scene for the generate coin")]
    [SerializeField]
    private GameObject canvas;

    [Tooltip("Button for the open chest")]
    [SerializeField]
    private GameObject chestButton;

    [Header("LEVEL")]
    [Tooltip("The level of player")]
    [SerializeField]
    private int level;
    [Tooltip("The text display of level of player")]
    [SerializeField]
    private TextMeshProUGUI levelText;
    [Tooltip("Holds the levelText")]
    [SerializeField]
    private GameObject levelDisplay;

    [Header("MENUs")]
    [Tooltip("Main Menu object in the scene")]
    [SerializeField]
    private GameObject mainMenu;

    [Tooltip("settings Menu object in the scene")]
    [SerializeField]
    private GameObject settingsMenu;

    [Tooltip("Shop Menu object in the scene")]
    [SerializeField]
    private GameObject shopMenu;

    [Header("Main Menu Effect")]
    [SerializeField]
    private GameObject panModel;
    [Tooltip("All of ingredients model")]
    [SerializeField]
    private List<GameObject> ingredientModelListInThePan;

    [Header("SFXs")]
    [Tooltip("AudioSource for SFXs")]
    [SerializeField]
    private AudioSource audioSource;
    [Tooltip("audioClips for Coin SFXs")]
    [SerializeField]
    private List<AudioClip> coinSFXList;
    [Tooltip("SoundManager for All SFXs' Sound")]
    [SerializeField]
    private SoundManager soundManager;

    public static Manager singleton;
    private void Awake()
    {
        if (!singleton)
        {
            singleton = this;
        }
        LoadCoin();
        LoadLevel();
        Manager.singleton.ChestDeactive();
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        OpenMainMenu();
    }

    /// <summary>
    /// Increse the coinCount amount value
    /// </summary>
    public void IncreaseCoin(int value)
    {
        coinCount += value;
        DisplayCoin();
        PlayCoinSFX();
    }

    /// <summary>
    /// Increse the coinCount 1 
    /// </summary>
    public void IncreaseCoin()
    {
        coinCount++;
        DisplayCoin();
        PlayCoinSFX();
    }

    /// <summary>
    /// Display the coinCount
    /// </summary>
    private void DisplayCoin()
    {
        coinText.text = coinCount.ToString();
    }

    /// <summary>
    /// Checks is coin greather than value
    /// </summary>
    /// <param name="_value"></param>
    /// <returns></returns>
    public bool CoinControl(int _value)
    {
        return coinCount > _value;
    }

    /// <summary>
    /// Decrese coin count amount value
    /// </summary>
    /// <param name="_value"></param>
    public void BuySomething(int _value)
    {
        coinCount -= _value;
        DisplayCoin();
        SaveCoin();
    }

    /// <summary>
    /// Generate a specified number of coins from the specfied position
    /// </summary>
    /// <param name="_count"></param>
    public void CoinEffect(int _count, Vector3 _generatePosition)
    {
        // Debug.Log(_count + " Coin Effect at " + _generatePosition);
        for (int i = 0; i < _count; i++)
        {
            float xPosition = UnityEngine.Random.Range(-150, 150);
            float yPosition = UnityEngine.Random.Range(-150, 150);

            Vector3 randomPosition = new Vector3(xPosition, yPosition, 0);

            GameObject currentCoin = Instantiate(coinPrefab, canvas.transform);

            currentCoin.transform.localPosition = randomPosition + _generatePosition;

            currentCoin.transform.localScale = Vector3.zero;

            currentCoin.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
                currentCoin.transform.DOLocalMove(coinDisplay.transform.localPosition, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
                    currentCoin.transform.DOScale(0, 0.15f).SetEase(Ease.InBack).OnComplete(() =>
                    IncreaseCoin())));

            // Debug.Log(i + " Coin Produced");
        }
        SaveCoin();
    }

    /// <summary>
    /// Displays the player levet
    /// </summary>
    public void DisplayLevelText()
    {
        Debug.Log("Level: " + level);
        level = level % 12;
        Debug.Log("Mod Level: " + level);
        levelText.text = "LEVEL - " + (level + 1);
    }

    /// <summary>
    /// Move the levelText
    /// </summary>
    public void MoveLevelDisplay(Vector3 movePosition)
    {
        levelDisplay.transform.DOLocalMove(movePosition, 0.25f).SetEase(Ease.InOutBack);
    }

    /// <summary>
    /// Allows t move to the next level
    /// </summary>
    public void NextLevel()
    {
        level++;
        SaveLevel();
        DisplayLevelText();
    }

    /// <summary>
    /// Plays coin SFX
    /// </summary>
    public void PlayCoinSFX()
    {
        SetRandomPitch();
        audioSource.PlayOneShot(coinSFXList[Random.Range(0, coinSFXList.Count)]);
    }


    /// <summary>
    /// Random Pitch for AudioSource
    /// </summary>
    public void SetRandomPitch()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
    }

    #region MAIN MENU

    /// <summary>
    /// starts the level
    /// </summary>
    public void PlayButton()
    {
        LevelManager.singleton.StartLevel();
        CloseMainMenu();
    }


    /// <summary>
    /// Opens the mainMenu
    /// </summary>
    public void OpenMainMenu()
    {
        OpenMainMenuEffect();
        mainMenu.transform.DOLocalMoveY(0, 0.25f).SetEase(Ease.OutBack);
        MoveLevelDisplay(new Vector3(0, 500, 0));
        LevelManager.singleton.HideLevelEndObjects();

        CloseShopMenu();
        CloseSettingsMenu();
    }

    /// <summary>
    /// Closes the mainMenu
    /// </summary>
    private void CloseMainMenu()
    {
        CloseMainMenuEffect();
        mainMenu.transform.DOLocalMoveY(-1920, 0.25f).SetEase(Ease.InBack);
    }

    /// <summary>
    /// starts the mainMenu effect
    /// </summary>
    private void OpenMainMenuEffect()
    {
        panModel.transform.DOLocalMoveY(-3, 0.25f).OnComplete(() =>
            panModel.transform.DOLocalRotate(new Vector3(-30, 120, -30), 0.15f).OnComplete(() =>
            IngredientEffectActive()));
    }

    /// <summary>
    /// Finishs the mainMenu Effect
    /// </summary>
    private void CloseMainMenuEffect()
    {
        panModel.transform.DOLocalMoveY(-15, 0.25f).OnComplete(() =>
            panModel.transform.DOLocalRotate(new Vector3(30, 120, 30), 0.15f).OnComplete(() =>
            IngredientEffectDeactive()));
    }

    /// <summary>
    /// Displays the ingredientsModel in pan
    /// </summary>
    private void IngredientEffectActive()
    {
        for (int i = 0; i < ingredientModelListInThePan.Count; i++)
        {
            ingredientModelListInThePan[i].transform.DOScale(0.75f, 0.75f);
        }
    }

    /// <summary>
    /// Hidesthe ingredientsModel in pan
    /// </summary>
    private void IngredientEffectDeactive()
    {
        for (int i = 0; i < ingredientModelListInThePan.Count; i++)
        {
            ingredientModelListInThePan[i].transform.DOScale(0, 0.25f);
        }
    }
    #endregion

    #region SHOP
    /// <summary>
    /// Opens the Shop Menu
    /// </summary>
    public void OpenShopMenu()
    {
        shopMenu.transform.DOLocalMoveX(0, 0.25f).SetEase(Ease.OutBack);
        MoveLevelDisplay(new Vector3(0, 1400, 0));
        CloseMainMenu();
        CloseSettingsMenu();
    }

    /// <summary>
    /// Closes the shop menu
    /// </summary>
    private void CloseShopMenu()
    {
        shopMenu.transform.DOLocalMoveX(-1080, 0.25f).SetEase(Ease.InBack);
    }
    #endregion

    #region SETTINGS

    /// <summary>
    /// Opens the setting menu
    /// </summary>
    public void OpenSettingsMenu()
    {
        settingsMenu.transform.DOLocalMoveX(0, 0.25f).SetEase(Ease.OutBack);
        MoveLevelDisplay(new Vector3(0, 1400, 0));
        CloseMainMenu();
        CloseShopMenu();
    }

    /// <summary>
    /// Closes the setting menu
    /// </summary>
    private void CloseSettingsMenu()
    {
        settingsMenu.transform.DOLocalMoveX(1080, 0.25f).SetEase(Ease.InBack);
    }

    /// <summary>
    /// Manages vibration with toggle
    /// </summary>
    /// <param name="toggleValue"></param>
    public void HapticToggle(bool toggleValue)
    {
        Vibrator.HapticControl(toggleValue);
    }

    /// <summary>
    /// Manages sound with toggle
    /// /// </summary>
    /// <param name="toggleValue"></param>
    public void SoundToggle(bool toggleValue)
    {
        Debug.Log("Sound Toogle: " + toggleValue);
        if (toggleValue)
        {
            soundManager.SetVolume(20);
        }
        else
        {
            soundManager.SetVolume(-80);
        }
    }
    #endregion

    #region CHEST

    /// <summary>
    /// Checks the chest button state
    /// </summary>
    public void ControlChest()
    {
        Debug.Log("Level: " + level + " Chest State: " + (level % 3 == 0));
        if (level % 3 == 0)
        {
            ChestActive();
        }
        else
        {
            ChestDeactive();
        }
    }

    /// <summary>
    /// Displays th echest button
    /// </summary>
    public void ChestActive()
    {
        chestButton.transform.DOLocalMoveX(350, 0.25f);
    }

    /// <summary>
    /// Hides the chest button
    /// </summary>
    public void ChestDeactive()
    {
        chestButton.transform.DOLocalMoveX(750, 0.25f);
    }
    #endregion

    #region SAVE&LOAD GAME

    #region COIN
    /// <summary>
    /// Saves the total coin count
    /// </summary>
    public void SaveCoin()
    {
        PlayerPrefs.SetInt("COIN_COUNT", coinCount);
    }

    /// <summary>
    /// Loads the total coin count
    /// </summary>
    public void LoadCoin()
    {
        coinCount = PlayerPrefs.GetInt("COIN_COUNT");
        DisplayCoin();
    }
    #endregion

    #region LEVEL
    /// <summary>
    /// Saves the player level
    /// </summary>
    public void SaveLevel()
    {
        PlayerPrefs.SetInt("LEVEL", level);
    }
    /// <summary>
    /// loads the player level
    /// </summary>
    public void LoadLevel()
    {
        level = PlayerPrefs.GetInt("LEVEL");
        DisplayLevelText();
    }
    #endregion

    #endregion
}
