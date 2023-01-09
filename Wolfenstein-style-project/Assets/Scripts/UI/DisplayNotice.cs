using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayNotice : MonoBehaviour
{
    public TextMeshProUGUI textPrefab;
    public GameObject textHolder;
    public GameObject imageHitNotice;
    public static GameObject imageHitNoticeStatic;
    public float offsetY;
    public int fadingTimeInSeconds;
    public static TextMeshProUGUI staticTextPrefab;
    public static GameObject staticTextHolder;
    public static Queue<TextMeshProUGUI> notices;
    public static TextMeshProUGUI noticeText;

    private static int numberOfDisplayedText;

    private static Vector3 offset;

    float time;
    private void Start()
    {
        notices = new Queue<TextMeshProUGUI>();
        staticTextPrefab = textPrefab;
        staticTextHolder = textHolder;
        offset.y = offsetY;
        time = fadingTimeInSeconds;
        imageHitNoticeStatic = imageHitNotice;
    }

    public static void AddText(string text)
    {
        staticTextPrefab.text = text;
        TextMeshProUGUI noticeText = Instantiate(staticTextPrefab, staticTextHolder.transform);
        notices.Enqueue(noticeText);
        numberOfDisplayedText = notices.Count - 1;
        RePositionText();
    }

    public static void HitNotice()
    {
        imageHitNoticeStatic.SetActive(false);
        imageHitNoticeStatic.SetActive(true);
    }

    private void Update()
    {
        if(notices.Count > 0)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                time = fadingTimeInSeconds;
                DestroyText();
            }

            if(notices.Count > 5)
            {
                DestroyText();
            }
        }
    }

    private void DestroyText()
    {
        //notices.Dequeue(); 
        TextMeshProUGUI fadingText = notices.Dequeue();
        Animator textFadingAnimator = fadingText.GetComponent<Animator>();
        textFadingAnimator.SetBool("Fading", true);
    }

    private static void RePositionText()
    {
        int count = numberOfDisplayedText;
        for (int i = 0; i < notices.Count; i++)
        {
            TextMeshProUGUI text = notices.Dequeue();
            RectTransform textTransform = text.GetComponent<RectTransform>();
            textTransform.anchoredPosition = new Vector2(0, -offset.y * count);
            notices.Enqueue(text);
            count--;
        }
    }
}
