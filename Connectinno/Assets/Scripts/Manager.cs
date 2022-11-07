using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Manager : MonoBehaviour
{
    [Header("COIN")]
    [SerializeField]
    private float coinCount;
    [SerializeField]
    private TextMeshProUGUI coinText;
    [SerializeField]
    private GameObject coinDisplay;

    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private GameObject canvas;


    [Header("LEVEL")]
    [SerializeField]
    private int level;
    [SerializeField]
    private TextMeshProUGUI levelText;
    [SerializeField]
    private GameObject levelDisplay;

    [Header("Always-ON")]
    [SerializeField]
    private GameObject footer;


    //[Header("Settings Menu Elements")]
    //[SerializeField]
    //private GameObject settingsMenuTitle;
    //[SerializeField]
    //private GameObject hapticToggle;
    //[SerializeField]
    //private GameObject soundToggle;

    //[Header("Shop Menu Elements")]
    //[SerializeField]
    //private GameObject shopMenuTitle;
    //[SerializeField]
    //private GameObject scrollMenu;

    //[Header("Main Menu Elements")]
    //[SerializeField]
    //private GameObject mainMenuTitle;
    //[SerializeField]
    //private GameObject pan;
    //[SerializeField]
    //private GameObject playButton;
    //[SerializeField]
    //private GameObject chestButton;
    //[SerializeField]
    //private List<GameObject> foodsInThePan;

    public static Manager singleton;
    private void Awake()
    {
        if (!singleton)
        {
            singleton = this;
        }
    }

    /// <summary>
    /// Increse the coinCount amount value
    /// </summary>
    public void IncreaseCoin(int value)
    {
        coinCount += value;
        DisplayCoin();
    }

    /// <summary>
    /// Increse the coinCount 1 
    /// </summary>
    public void IncreaseCoin()
    {
        coinCount++;
        DisplayCoin();
    }

    /// <summary>
    /// Display the coinCount
    /// </summary>
    private void DisplayCoin()
    {
        coinText.text = coinCount.ToString();
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="_count"></param>
    public void CoinEffect(int _count, Vector3 _generatePosition)
    {
        Debug.Log("Coin Effect");
        for (int i = 0; i < _count; i++)
        {
            float xPosition = UnityEngine.Random.Range(-150, 150);
            float yPosition = UnityEngine.Random.Range(-150, 150);

            Vector3 randomPosition = new Vector3(xPosition, yPosition, 0);

            GameObject currentCoin = Instantiate(coinPrefab, canvas.transform);

            currentCoin.transform.localPosition = randomPosition + _generatePosition;

            currentCoin.transform.localScale = Vector3.zero;

            currentCoin.transform.DOScale(1, 0.5f).OnComplete(() =>
                currentCoin.transform.DOLocalMove(coinDisplay.transform.localPosition, 0.5f).OnComplete(() =>
                    currentCoin.transform.DOScale(0, 0.15f).OnComplete(() =>
                    IncreaseCoin())));

            Debug.Log(i + " Coin Produced");
        }
    }

    public void DisplayLevel()
    {
        levelText.text = "LEVEL - " + level;
    }

    public void NextLevel()
    {
        level++;
        DisplayLevel();
    }

    ///// <summary>
    ///// 
    ///// </summary>
    //public void MainMenuActive()
    //{
    //    mainMenuTitle.transform.DOScale(1, 0.25f);
    //    pan.transform.DOLocalMove(Vector3.zero, 0.25f).OnComplete(() =>
    //            mainMenuTitle.transform.DOScale(1, 0.25f)).OnComplete(() =>
    //            FoodEffectActive());
    //    playButton.transform.DOScale(1, 0.25f);
    //    ChestActive();
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //public void MainMenuDeactive()
    //{
    //    mainMenuTitle.transform.DOScale(0, 0.25f);
    //    pan.transform.DOLocalMove(new Vector3(1150, -500, 0), 0.25f).OnComplete(() =>
    //              mainMenuTitle.transform.DOScale(0, 0.25f)).OnComplete(() =>
    //              FoodEffectDeactive());
    //    playButton.transform.DOScale(0, 0.25f);
    //    ChestDeactive();
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //private void FoodEffectActive()
    //{
    //    float amount = 0;
    //    for (int i = 0; i < foodsInThePan.Count; i++)
    //    {
    //        amount += 25;
    //        Vector3 foodPosition =
    //            new Vector3(foodsInThePan[i].transform.localPosition.x -
    //            UnityEngine.Random.Range(-150, 250) - amount,
    //            foodsInThePan[i].transform.localPosition.y +
    //            UnityEngine.Random.Range(0, 400) + amount, 0);

    //        foodsInThePan[i].transform.DOLocalMove(foodPosition, 0.25f);
    //    }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //private void FoodEffectDeactive()
    //{
    //    for (int i = 0; i < foodsInThePan.Count; i++)
    //    {
    //        foodsInThePan[i].transform.DOLocalMove(Vector3.zero, 0.15f);
    //    }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //private void ChestActive()
    //{        
    //    chestButton.transform.DOLocalMoveX(350, 0.25f);
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //private void ChestDeactive()
    //{        
    //    chestButton.transform.DOLocalMoveX(750, 0.25f);
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //public void SettingsMenuActive()
    //{
    //    settingsMenuTitle.transform.DOScale(1, 0.25f);
    //    soundToggle.transform.DOScale(1, 0.25f);
    //    hapticToggle.transform.DOScale(1, 0.25f);
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //public void SettingsMenuDeactive()
    //{
    //    settingsMenuTitle.transform.DOScale(0, 0.25f);
    //    soundToggle.transform.DOScale(0, 0.25f);
    //    hapticToggle.transform.DOScale(0, 0.25f);
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //public void ShopMenuActive()
    //{
    //    shopMenuTitle.transform.DOScale(1, 0.25f);
    //    scrollMenu.transform.DOScale(1, 0.25f);
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //public void ShopMenuDeactive()
    //{
    //    shopMenuTitle.transform.DOScale(0, 0.25f);
    //    scrollMenu.transform.DOScale(0, 0.25f);
    //}
}
