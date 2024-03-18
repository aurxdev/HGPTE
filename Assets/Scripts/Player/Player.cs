using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private float timeToChangeStamina;

    [SerializeField]
    public Inventory inventory;
    [SerializeField]
    private int maxInventory;
    [SerializeField]
    private GameObject healthContainer;
    [SerializeField]
    private GameObject staminaContainer;


    private bool isDead;

    public bool IsDead
    {
        get { return isDead; }
        set {
            isDead = value;
            if (isDead) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
    public bool IsOpening { get; set; }

    public bool IsPausing { get; set; }
    public bool IsFarming { get; set;}

    public bool CanAttack { get; set; } = false;

    public bool CanMine { get; set; } = false;

    public bool CanCutting { get; set; } = false;

    public Inventory chestInventory;

    public int selectedSlot;
    public delegate void OnSlotChanged();
    public event OnSlotChanged onSlotChanged;

    // appelé avant la première frame
    private void Awake()
    {
        inventory = new Inventory(maxInventory);
        SetHp(hp, false);
        SetStamina(stamina, false);
        IsPausing=true;
        IsDead=false;
    } // Awake()

    private IEnumerator AnimateHealthBarChange(float startValue, float endValue)
    {
        float elapsedTime = 0;
        while (elapsedTime < timeToChangeHealth)
        {
            elapsedTime += Time.deltaTime;
            float var = Mathf.Lerp(startValue, endValue, elapsedTime / timeToChangeHealth);
            healthContainer.GetComponent<Image>().fillAmount = var;
            this.hp = var * this.maxHp;
            yield return null;
        }
    }


    public void SetHp(float nb, bool animate)
    {
        if (nb <= 0) {
            IsDead = true;
            nb = 0;
        } else if (nb > this.maxHp) {
            nb = this.maxHp;
        }


        if(!animate)
        {
            float startValue = this.hp / this.maxHp;
            this.hp = nb;
            float var = this.hp / this.maxHp;
            healthContainer.GetComponent<Image>().fillAmount = var;
        }
        else
        {
            float startValue = this.hp / this.maxHp;
            StartCoroutine(AnimateHealthBarChange(startValue, nb / this.maxHp));
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



    private IEnumerator AnimateStaminaBarChange(float startValue, float endValue)
    {
        float elapsedTime = 0;
        while (elapsedTime < timeToChangeStamina)
        {
            elapsedTime += Time.deltaTime;
            float var = Mathf.Lerp(startValue, endValue, elapsedTime / timeToChangeStamina);
            staminaContainer.GetComponent<Image>().fillAmount = var;
            this.stamina = var * this.maxStamina;
            yield return null;
        }
    }



    public void SetStamina(float nb, bool animate)
    {
        if (nb <= 0) {
            nb = 0;
        } else if (nb > this.maxStamina) {
            nb = this.maxStamina;
        }


        if(!animate)
        {
            float startValue = this.stamina / this.maxStamina;
            this.stamina = nb;
            float var = this.stamina / this.maxStamina;
            staminaContainer.GetComponent<Image>().fillAmount = var;
        }
        else
        {
            float startValue = this.stamina / this.maxStamina;
            StartCoroutine(AnimateStaminaBarChange(startValue, nb / this.maxStamina));
        }

    }

    public float GetStamina()
    {
        return this.stamina;
    }

    public void RemoveStamina(float nb, bool animate)
    {
        SetStamina(this.stamina - nb, animate);
    }


    public void AddStamina(float nb, bool animate)
    {
        SetStamina(this.stamina + nb, animate);
    }

    // écoute les touches pour changer de slot
    public int SelectedSlotNumber(){
        if(Input.GetKeyDown(KeyCode.Alpha1)) return 0;
        if(Input.GetKeyDown(KeyCode.Alpha2)) return 1;
        if(Input.GetKeyDown(KeyCode.Alpha3)) return 2;
        if(Input.GetKeyDown(KeyCode.Alpha4)) return 3;
        if(Input.GetKeyDown(KeyCode.Alpha5)) return 4;
        if(Input.GetKeyDown(KeyCode.Alpha6)) return 5;
        return -1;
    } // SelectedSlotNumber()

    void Update()
    {
        MovementManager movementManager = GetComponent<MovementManager>();
        int slotNumber = SelectedSlotNumber();
        if (slotNumber != -1)
        {
            selectedSlot = slotNumber;
            onSlotChanged?.Invoke();
            if (inventory.slots[selectedSlot].id == 10) {
                CanAttack = true;
            } else {
                CanAttack = false;
            }
            if (inventory.slots[selectedSlot].id == 12) {
                CanMine = true;
            } else {
                CanMine = false;
            }
            if (inventory.slots[selectedSlot].id == 11) {
                CanCutting = true;
            } else {
                CanCutting = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape) && !IsDead && !IsOpening ){
            IsPausing = !IsPausing;
        }

        if (this.hp < this.maxHp && !IsDead) 
        {
            AddHp(healthRegenRate * Time.deltaTime, false);
        }

        if (this.stamina < this.maxStamina && !movementManager.IsOnAnimation() && !movementManager.IsRunning)
        {
            AddStamina(staminaRegenRate * Time.deltaTime, false);
        }
    }
}
