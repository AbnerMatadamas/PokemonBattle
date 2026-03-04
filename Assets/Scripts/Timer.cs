using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private secondData[] secondsData;
    [SerializeField]
    private Image timerImage;
    [SerializeField]
    private string timerAnimationName;
    [SerializeField]
    private UnityEvent onTimerEnd;
    private Animator timerAnimator;
    private Coroutine timerCoroutine;
    private void Awake()
    {
        timerAnimator = timerImage.GetComponent<Animator>();
    }
    public void StartTimer(int duration)
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
        timerCoroutine = StartCoroutine(TimerCoroutine(duration));
    }
    private IEnumerator TimerCoroutine(int duration)
    {
        for (int i = 0; i < duration; i++)
        {
            SoundManager.instance.Play(secondsData[i].soundName);
            timerImage.sprite = secondsData[i].image;
            timerImage.SetNativeSize();
            timerAnimator.Play(timerAnimationName, 0, 0f);
            yield return new WaitForSeconds(1f);
        }
        onTimerEnd?.Invoke();
    }
    public void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }
}

[System.Serializable]
public class secondData
{
    public string soundName;
    public Sprite image;
}