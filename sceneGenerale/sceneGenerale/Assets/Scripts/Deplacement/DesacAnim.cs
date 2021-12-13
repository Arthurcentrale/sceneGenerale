using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesacAnim : MonoBehaviour
{

    public GameObject player;
    public GameObject perso;
    private Vector3 position;
    public GameObject[] gameobjectAnimes;

    // Start is called before the first frame update
    double distance(Vector3 pos1, Vector3 pos2)
    {
        double dis = (pos1.x - pos2.x) * (pos1.x - pos2.x) + (pos1.y - pos2.y) * (pos1.y - pos2.y) + (pos1.z - pos2.z) * (pos1.z - pos2.z);
        return System.Math.Sqrt(dis);
    }


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        position = player.transform.position;
        gameobjectAnimes = GameObject.FindGameObjectsWithTag("Eau");
        foreach (GameObject Animes in gameobjectAnimes)
        {
            if (distance(position, Animes.transform.position) > 40)
            {
                Animes.GetComponent<Animator>().enabled = false;
            }
            else
            {
                Animes.GetComponent<Animator>().enabled = true;
            }
        }

        gameobjectAnimes = GameObject.FindGameObjectsWithTag("Arbre");
        foreach (GameObject Animes in gameobjectAnimes)
        {
            
            if (distance(position, Animes.transform.position) > 40)
            {
                Animes.transform.GetChild(0).gameObject.GetComponent<Animator>().enabled = false;
            }
            else
            {
                Animes.transform.GetChild(0).gameObject.GetComponent<Animator>().enabled = true;
            }
        }
    }
}
