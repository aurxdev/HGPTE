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
        SetHp(hp);
        SetStamina(stamina);
    }




    public void SetHp(float nb)
    {
        if (nb <= 0) {
            IsDead = true;
            nb = 0;
        } else if (nb > this.maxHp) {
            nb = this.maxHp;
        }

        this.hp = nb;
        float var = this.hp / this.maxHp;

        healthContainer.GetComponent<Image>().fillAmount = var;
    }

    public float GetHp()
    {
        return this.hp;
    }



    public void RemoveHp(float nb)
    {
        SetHp(this.hp - nb);
    }

    public void AddHp(float nb)
    {
        SetHp(this.hp + nb);
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
            AddHp(healthRegenRate * Time.deltaTime);
        }

        if (this.stamina < this.maxStamina && !movementManager.IsOnAnimation() && !movementManager.IsRunning)
        {
            AddStamina(10 * Time.deltaTime);
        }
    }

}
