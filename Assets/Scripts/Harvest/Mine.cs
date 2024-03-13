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
            player.inventory.Add(new Item(item));
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
