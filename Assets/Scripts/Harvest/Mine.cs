using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : IHarvest
{
    public override IEnumerator Harvest()
    {
        isHarvesting = true;
        MovementManager movementManager = player.GetComponent<MovementManager>();
        movementManager.IsHarvesting = true;
        int amount = Random.Range(minAmount, maxAmount);
        yield return new WaitForSeconds(harvestTime);
        for (int i = 0; i < amount; i++)
        {
                //ajouter les itemdata à l'inventaire et fait de même pour la méthode cut, quadn c'est fait dit le stp
        }        
        movementManager.IsHarvesting = false;
        Destroy(gameObject);
    }

    // Update is called once per frame
    public override void Update()
    {
        if (player != null && Input.GetKeyDown(KeyCode.E) && !isHarvesting && player.CanMine)
        {
            StartCoroutine(Harvest());
        }
    }
}
