using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public TextMeshProUGUI healthText;
    public bool takeDamage = false;
    public EnemyMovement enemyMovement;
    public EnemyAnimation enemyAnimation;

    public virtual void OnEnable()
    {
        SetValue();
    }
    public void SetValue()
    {
        healthText.gameObject.SetActive(true);
        int maxHealth1 = GameManager.Instance.waveCount * maxHealth;
        SetMaxHealth(maxHealth1);
    }

    public void SetMaxHealth(int value)
    {
        health = value;
        UpdateHealthText();
    }
    public void UpdateHealthText()
    {
        if (health < 0)
        {
            healthText.text = "" + 0;
        }
        else
        {
            healthText.text = "" + health;
        }
    }

    public void TakeDamage(int damage, int index)
    {
        if (!enemyMovement.dead)
        {
            health -= damage;
            enemyAnimation.blinkTimer = enemyAnimation.blinkDuration;
            AudioManager.instance.PlayShot("ZombieBaby", 0);
            UpdateHealthText();
            enemyAnimation.CreateVfx(index);
            if (health <= 0)
            {
                enemyMovement.dead = true;
                healthText.gameObject.SetActive(false);
                enemyAnimation.ChangeStateAnimation("die", 0.25f, 0, 0);
                GameManager.Instance.enemySpawned.Remove(gameObject);
                DOVirtual.DelayedCall(2, () => { enemyMovement.SetDead(); });
            }

        }
    }
}
