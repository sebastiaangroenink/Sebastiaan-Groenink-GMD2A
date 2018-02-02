using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour {
    public PlayerController playerController;

    public GameObject rangePowerUp;
    public GameObject bombLimitPowerUp;

	public void SpawnUpgrade(GameObject box)
    {
        int random = Random.Range(0, 10);
        if(random == 1)
        {
            Instantiate(rangePowerUp, box.transform.position, Quaternion.identity);
        }
        else if(random == 2)
        {
            Instantiate(bombLimitPowerUp, box.transform.position, Quaternion.identity);
        }
    }

    public void TriggerUpgrade(Transform upgrade)
    {
        if(upgrade.transform.name == "BombLimitPowerUp(Clone)")
        {
            playerController.bombLimit++;
            Destroy(upgrade.gameObject);
        }
        else if(upgrade.transform.name == "RangePowerUp(Clone)")
        {
            playerController.bombRange++;
            Destroy(upgrade.gameObject);
        }
    }
}
