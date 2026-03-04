using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageText : MonoBehaviour
{
    [SerializeField]
    private Animator _textAnimator;
    [SerializeField]
    private string _animationName;
    [SerializeField]
    private Text _text;
    private void Awake()
    {
        _text.text = string.Empty;
    }
    public void ShowDamage(DamageTarget damageTarget)
    {
        _text.text = damageTarget.damage.ToString("F0");
        transform.position = Camera.main.WorldToScreenPoint(damageTarget.target.position);
        _textAnimator.Play(_animationName);
        StartCoroutine(DisableAfterAnimation());
    }
    private IEnumerator DisableAfterAnimation()
    {
        yield return new WaitForSeconds(_textAnimator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }
}

[System.Serializable]
public class DamageTarget
{
    public Transform target;
    public float damage;
    public void SetDamageTarget(Transform target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }
}
