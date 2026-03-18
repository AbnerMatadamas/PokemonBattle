using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private int minimumFighters = 2;
    [SerializeField]
    private int maximumFighters = 4;
    [SerializeField]
    private UnityEvent onStartBattleCount;
    [SerializeField]
    private UnityEvent onStartBattle;
    [SerializeField]
    private UnityEvent onStopBattle;
    [SerializeField]
    private UnityEvent<string>onWinBattle;
    private Coroutine battleCoroutine;
    private List<Fighter> fighters = new List<Fighter>();
    public void AddFighter(Fighter fighter)
    {
        if (fighters.Count < maximumFighters)
        {
            DialogSystem.Instance.ShowDialog(fighter.CharacterData.characterName + " has entered the battle!");
            fighters.Add(fighter);
            fighter.Initialize();
            if (fighters.Count >= minimumFighters)
            {
                StopBattle();
                onStartBattleCount?.Invoke();
            }
        }
    }
    public void RemoveFighter(Fighter fighter)
    {
        if (fighters.Contains(fighter))
        {
            fighters.Remove(fighter);
            if (fighters.Count < minimumFighters)
            {
                onStopBattle?.Invoke();
            }
        }
    }
    public void StartBattle()
    {
        onStartBattle?.Invoke();
        battleCoroutine = StartCoroutine(BattleCoroutine());
    }
    private IEnumerator BattleCoroutine()
    {
        while (fighters.Count > 1)
        {
            Fighter attacker = fighters[Random.Range(0, fighters.Count)];
            Fighter defender = fighters[Random.Range(0, fighters.Count)];
            while (defender == attacker)
            {
                defender = fighters[Random.Range(0, fighters.Count)];
            }
            Attack attack = attacker.GetRandomAttack();
            DialogSystem.Instance.ShowDialog(attacker.CharacterData.characterName + " attacks with " + attack.attackData.attackName + "!");
            defender.transform.LookAt(attacker.transform);
            attacker.transform.LookAt(defender.transform);
            SoundManager.instance.Play(attack.attackData.attackSoundName);
            attacker.Animator.Play(attack.attackData.animationName);
            attack.particlesPool.InstantiateObject(attacker.transform.position);
            float damage = Random.Range(attack.attackData.minDamage, attack.attackData.maxDamage);
            yield return new WaitForSeconds(attack.attackData.attackDuration);
            attack.hitParticlesPool.InstantiateObject(defender.transform.position);
            defender.Health.TakeDamage(damage);
            if (defender.Health.CurrentHealth <= 0)
            {
                RemoveFighter(defender);
            } 
            else
            {
                yield return new WaitForSeconds(1.5f);
            }
        }
        if (fighters.Count == 1)
        {
            WinBattle(fighters[0]);
        }
    }
    public void WinBattle(Fighter winner)
    {
        onWinBattle?.Invoke(winner.CharacterData.characterName);
        winner.Animator.Play(winner.CharacterData.winAnimationName);
        winner.transform.LookAt(Camera.main.transform);
    }
    public void StopBattle()
    {
        if (battleCoroutine != null)
        {
            StopCoroutine(battleCoroutine);
            battleCoroutine = null;
        }
    }
}
