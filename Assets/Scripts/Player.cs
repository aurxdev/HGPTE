using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player : MonoBehaviour
{
    [SerializeField]
    private float hp;
    [SerializeField]
    private float maxHp;
    [SerializeField]
    private float healthRegenRate;

    [SerializeField]
    private float timeToChangeHealth;

    [SerializeField]
    private float stamina;
    [SerializeField]
    public float maxStamina;

    [SerializeField]
    private float staminaRegenRate;
    [SerializeField]
    public Inventory inventory;
    [SerializeField]
    private int maxInventory;
    [SerializeField]
    private GameObject healthContainer;
    [SerializeField]
    private GameObject staminaContainer;
    public bool IsDead {  get; set; }
    public bool IsOpening { get; set; }

    public Inventory chestInventory;

    private void Awake()
    {
        inventory = new Inventory(maxInventory);
        SetHp(hp, false);
        SetStamina(stamina);
    }

    private IEnumerator AnimateBarChange(Image bar, float startValue, float endValue, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            Debug.Log("time: " + time);
            bar.fillAmount = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        bar.fillAmount = endValue;
    }


    public void SetHp(float nb, bool animate)
    {
        if (nb <= 0) {
            IsDead = true;
            nb = 0;
        } else if (nb > this.maxHp) {
            nb = this.maxHp;
        }

        float startValue = this.hp / this.maxHp;
        this.hp = nb;
        float var = this.hp / this.maxHp;

        if(!animate)
        {
            healthContainer.GetComponent<Image>().fillAmount = var;
        }
        else
        {
            StartCoroutine(AnimateBarChange(healthContainer.GetComponent<Image>(), startValue, var, timeToChangeHealth));
        }

    }

    public float GetHp()
    {
        return this.hp;
    }



    public void RemoveHp(float nb, bool animate)
    {
        SetHp(this.hp - nb, animate);
    }


    public void AddHp(float nb, bool animate)
    {
        SetHp(this.hp + nb, animate);
    }




    public void SetStamina(float nb)
    {
        if (nb <= 0) {
            nb = 0;
        } else if (nb > this.maxStamina) {
            nb = this.maxStamina;
        }

        this.stamina = nb;
        float var = this.stamina / this.maxStamina;

        staminaContainer.GetComponent<Image>().fillAmount = var;   
    }

    public float GetStamina()
    {
        return this.stamina;
    }

    public void RemoveStamina(float nb)
    {
        SetStamina(this.stamina - nb);
    }

    public void AddStamina(float nb)
    {
        SetStamina(this.stamina + nb);
    }


    void Update()
    {
        MovementManager movementManager = GetComponent<MovementManager>();
        if (this.hp < this.maxHp && !IsDead) 
        {
            AddHp(healthRegenRate * Time.deltaTime, false);
        }

        if (this.stamina < this.maxStamina && !movementManager.IsOnAnimation() && !movementManager.IsRunning)
        {
            AddStamina(staminaRegenRate * Time.deltaTime);
        }
    }

}
