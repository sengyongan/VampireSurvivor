using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 玩家的硬币控制器 */
public class CoinController : MonoBehaviour
{
    public static CoinController instance;
    public int currentCoins;
    public int maxCoins = 50;
    public CoinPickup coin;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddConis(int amount)
    {
        if (currentCoins + amount <= maxCoins)
        {
            currentCoins += amount;
        }
        else
        {
            currentCoins = maxCoins;
        }
    }
    public void DropCoin(Vector3 position, int vale)
    {
        CoinPickup newCoin = Instantiate(coin, position + new Vector3(.2f, .1f, 0f), Quaternion.identity);
        newCoin.value = vale;
        newCoin.gameObject.SetActive(true);
    }
}
