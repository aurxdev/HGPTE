using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player : MonoBehaviour
{
    [SerializeField]
    private int hp;
    [SerializeField]
    private int maxHp;
    [SerializeField]
    private int stamina;
    [SerializeField]
    public int maxStamina;
    [SerializeField]
    public Inventory inventory;
    [SerializeField]
    private int maxInventory;
    [SerializeField]
    private GameObject healthContainer;
    [SerializeField]
    private GameObject staminaContainer;
    private bool IsDead {  get; set; }
    public bool IsOpening { get; set; }

    public Inventory chestInventory;
    private void Awake()
    {
        inventory = new Inventory(maxInventory);
        SetHp(hp);
        SetStamina(stamina);
    }

    public void SetHp(int nb)
    {
        if (nb >= 0 && nb < this.maxHp){
            this.hp = nb;
            float var = (float)this.hp / (float)this.maxHp;
            healthContainer.GetComponent<Image>().fillAmount = var;
        }
        else IsDead = true;
    }

    public void SetStamina(int nb)
    {
        if (nb >= 0 && nb < this.maxStamina)
        {
            this.hp = nb;
            float var = (float)this.hp / (float)this.maxStamina;
            staminaContainer.GetComponent<Image>().fillAmount = var;
        }
    }

}
