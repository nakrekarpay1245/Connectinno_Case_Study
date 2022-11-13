using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Chest : MonoBehaviour
{
    [Header("Chest Parts")]
    [SerializeField]
    private GameObject chestTop;

    [Header("Chest Glow Particle")]
    [SerializeField]
    private ParticleSystem chestGlowParticle;

    private void Awake()
    {
        chestGlowParticle = GetComponentInChildren<ParticleSystem>();
    }

    /// <summary>
    /// Opens the reward chest
    /// </summary>
    public void OpenChest()
    {
        StartCoroutine(OpenChestRoutine());
    }

    /// <summary>
    /// Opens the reward chest
    /// </summary>
    private IEnumerator OpenChestRoutine()
    {
        Manager.singleton.ChestDeactive();
        yield return new WaitForSeconds(0);

        Vector3 rotateRandomly = (Vector3.up) * 10;
        transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
        transform.DOPunchRotation(rotateRandomly, 1);
        yield return new WaitForSeconds(1);

        chestTop.transform.DOLocalRotate(new Vector3(0, 0, 0), 1);
        chestTop.transform.DOLocalMove(new Vector3(0, 3.25f, 0.9f), 1);
        chestGlowParticle.Play();
        yield return new WaitForSeconds(0.75f);

        Manager.singleton.CoinEffect(15, Vector3.zero);
        yield return new WaitForSeconds(1);

        chestGlowParticle.Stop();
        yield return new WaitForSeconds(0.25f);

        chestTop.transform.DOLocalRotate(new Vector3(-105, 0, 0), 1);
        chestTop.transform.DOLocalMove(new Vector3(0, 1, -0.95f), 1);
        transform.DOScale(0, 0.75f).SetEase(Ease.InBack);
        yield return new WaitForSeconds(0);
    }
}
