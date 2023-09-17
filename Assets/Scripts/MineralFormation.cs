using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral : MonoBehaviour
{
    public GameObject IronOre;
    public GameObject GoldMine;
    public GameObject DiamondMine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IronOreFormation(Vector3 IronOrePosition)
    {
        GameObject ironOre = Instantiate(IronOre, IronOrePosition, Quaternion.identity);
    }

    void IronOreDisappear(Vector3 IronOrePosition)
    {
        if(transform.position == IronOrePosition)
        {
            Destroy(gameObject);
        }
    }

    void GoldMineFormation(Vector3 GoldMinePosition)
    {
        GameObject goldMine = Instantiate(GoldMine, GoldMinePosition, Quaternion.identity);
    }

    void GoldMineDisappear(Vector3 GoldMinePosition)
    {
        if (transform.position == GoldMinePosition)
        {
            Destroy(gameObject);
        }
    }

    void DiamondMineFormation(Vector3 DiamondMinePosition)
    {
        GameObject diamondMine = Instantiate( DiamondMine, DiamondMinePosition, Quaternion.identity);
    }

    void DiamondMineDisappear(Vector3 DiamondMinePosition)
    {
        if (transform.position == DiamondMinePosition)
        {
            Destroy(gameObject);
        }
    }
}
