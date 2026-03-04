using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField]
    private Text winnerText;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private string showAnimationName = "Show";
    [SerializeField]
    private string hideAnimationName = "Hidden";
    public void ShowWinScreen(string winnerName)
    {
        winnerText.text = winnerName + " wins!";
        animator.Play(showAnimationName, 0, 0f);
    }
    public void HideWinScreen()
    {
        animator.Play(hideAnimationName, 0, 0f);
    }
}
