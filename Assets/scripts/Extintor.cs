using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extintor : MonoBehaviour
{
    public bool isBeingCarried = false;
    public Camera PlayerCamera;
    public Transform originalParent;
    public Transform mao;

    public bool canPickUp = true;
    public float maxDistance = 2.0f; // Dist�ncia m�xima para pegar o objeto

    public GameObject origemParticulas; // Objeto que cont�m as part�culas
    public ParticleSystem extintorParticles; // Refer�ncia para o sistema de part�culas

    public LayerMask objectToExtinguish; // Camada do objeto a ser apagado

    private bool wasPickedUp = false;

    void Start()
    {
        originalParent = transform.parent;

        // Obt�m a refer�ncia do sistema de part�culas do objeto "origem"
        extintorParticles = origemParticulas.GetComponent<ParticleSystem>();
        //extintorParticles.Stop(); // Certifica-se de que as part�culas est�o desativadas no in�cio
        var em = extintorParticles.emission;
        em.enabled = false;
    }

    void Update()
    {

        if (isBeingCarried)
        {
            if (Input.GetMouseButton(0)) // Bot�o esquerdo do mouse est� pressionado
            {
                //extintorParticles.Play(); // Inicia as part�culas quando o bot�o do mouse � pressionado
                var em = extintorParticles.emission;
                em.enabled = true;

                // Configura a colis�o das part�culas com o objeto espec�fico
                var collisionModule = extintorParticles.collision;
                collisionModule.enabled = true;
                collisionModule.collidesWith = objectToExtinguish;
            }
            else
            {
                //extintorParticles.Stop(); // Para as part�culas quando o bot�o do mouse � solto
                var em = extintorParticles.emission;
                em.enabled = false;

                // Desativa a colis�o das part�culas quando o bot�o do mouse � solto
                var collisionModule = extintorParticles.collision;
                collisionModule.enabled = false;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                // Solte o objeto quando o jogador pressiona "Q".
                canPickUp = true;
                transform.parent = originalParent;
                GetComponent<Rigidbody>().isKinematic = false;
                isBeingCarried = false;

                if (wasPickedUp)
                {
                    // Mostra as part�culas quando o extintor � solto
                    //extintorParticles.Play();
                }
            }
            else
            {
                // Atualize a posi��o do objeto enquanto estiver sendo carregado pelo jogador.
                Vector3 newPosition = mao.position;
                transform.position = newPosition;
            }
        }
        else
        {
            float distanceToPlayer = Vector3.Distance(transform.position, PlayerCamera.transform.position);

            if (canPickUp && distanceToPlayer <= maxDistance)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    // Pegue o objeto quando o jogador pressiona "Q" e est� dentro da dist�ncia m�xima.
                    isBeingCarried = true;
                    canPickUp = false;
                    wasPickedUp = true;

                    // Esconde as part�culas quando o extintor � pego
                    //extintorParticles.Stop();

                    Debug.Log("Pegou o Objeto");
                    transform.parent = mao; // Torna a m�o o pai do objeto
                    GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
    }
}
