using System;
using UnityEngine;

public class Line : MonoBehaviour
{
    public string lineName;

    private GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            if (lineName.Equals("Start") && gm.isFinished)
            {
                gm.StartGame();
            }

            if (lineName.Equals("Finish"))
            {
                gm.FinishLevel();
            }
        }
    }
}
