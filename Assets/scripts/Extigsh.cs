using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extigsh : MonoBehaviour
{
    public ParticleSystem extigshParticles;

    void Start()
    {
        extigshParticles = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (extigshParticles.isEmitting)
        {
            if (CheckForFire("FirePoint"))
            {
                Debug.Log("Apagando");
            }
        }
        
    }

    bool CheckForFire(string fire, float dis_modifier = 1f)
    {
        GameObject[] lista = GameObject.FindGameObjectsWithTag(fire);

        foreach (GameObject g in lista)
        {
            float distanceToFire = Vector3.Distance(g.transform.position, transform.position);
            float fireRadius = g.GetComponent<ParticleSpread>().GetRadius();

            if (distanceToFire <= fireRadius * g.transform.localScale.x * dis_modifier)
             {
                // Diminuir o raio do objeto "FirePoint"
                fireRadius -= Time.deltaTime; // Ajuste conforme necessário
                //g.GetComponent<ParticleSpread>().SetRadius(fireRadius);
                var em = g.GetComponent<ParticleSystem>().emission;
                em.enabled = false;
                Debug.Log("Raio do FirePoint diminuindo");
            }
        }

        return false;
    }
}
