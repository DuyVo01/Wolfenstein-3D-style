using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSummary : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelRecordTitle;
    [SerializeField] private GameObject levelRecordTime;
    [SerializeField] private GameObject levelRecordEnemy;
    [SerializeField] private GameObject levelRecordTreasure;
    [SerializeField] private GameObject levelRecordScore;
    [SerializeField] private GameObject buttions;
    [SerializeField] private Image levelRecordBackground;
    [SerializeField] private float appearanceTime;
    [SerializeField] private SaveManager saveManager;

    private Color startBackgroundColor;
    private Color endBackgroundColor;

    private Color startTitleColor;
    private Color endTitleColor;
    private float timePassed;


    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void Start()
    {
        startBackgroundColor = new Color(levelRecordBackground.color.r, levelRecordBackground.color.g, levelRecordBackground.color.b, 0);
        endBackgroundColor = new Color(levelRecordBackground.color.r, levelRecordBackground.color.g, levelRecordBackground.color.b, 1);

        startTitleColor = new Color(levelRecordTitle.color.r, levelRecordTitle.color.g, levelRecordTitle.color.b, 0);
        endTitleColor = new Color(levelRecordTitle.color.r, levelRecordTitle.color.g, levelRecordTitle.color.b, 1);


        levelRecordScore.SetActive(false);
        levelRecordTime.SetActive(false);
        levelRecordTreasure.SetActive(false);
        levelRecordEnemy.SetActive(false);
    }


    public void Summary()
    {
        saveManager.Save();
        StartCoroutine(Appear());
    }

    private IEnumerator Appear()
    {
        timePassed = 0;
        levelRecordBackground.gameObject.SetActive(true);
        levelRecordTitle.gameObject.SetActive(true);
        while (timePassed < appearanceTime)
        {
            timePassed += Time.unscaledDeltaTime;
            levelRecordBackground.color = Color.Lerp(startBackgroundColor, endBackgroundColor, timePassed/appearanceTime);
            levelRecordTitle.color = Color.Lerp(startTitleColor, endTitleColor, timePassed / appearanceTime);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSecondsRealtime(0.2f);
        levelRecordTime.SetActive(true);
        yield return new WaitForSecondsRealtime(0.2f);
        levelRecordEnemy.SetActive(true);
        yield return new WaitForSecondsRealtime(0.2f);
        levelRecordTreasure.SetActive(true);
        yield return new WaitForSecondsRealtime(0.2f);
        levelRecordScore.SetActive(true);
        yield return new WaitForSecondsRealtime(0.2f);
        buttions.SetActive(true);
    }
}
