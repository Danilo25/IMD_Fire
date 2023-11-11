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
    public float maxDistance = 2.0f; // Distância máxima para pegar o objeto

    public GameObject origemParticulas; // Objeto que contém as partículas
    public ParticleSystem extintorParticles; // Referência para o sistema de partículas

    public LayerMask objectToExtinguish; // Camada do objeto a ser apagado

    private bool wasPickedUp = false;

    void Start()
    {
        originalParent = transform.parent;

        // Obtém a referência do sistema de partículas do objeto "origem"
        extintorParticles = origemParticulas.GetComponent<ParticleSystem>();
        extintorParticles.Stop(); // Certifica-se de que as partículas estão desativadas no início
    }

    void Update()
    {
        if (isBeingCarried)
        {
            if (Input.GetMouseButton(0)) // Botão esquerdo do mouse está pressionado
            {
                extintorParticles.Play(); // Inicia as partículas quando o botão do mouse é pressionado

                // Configura a colisão das partículas com o objeto específico
                var collisionModule = extintorParticles.collision;
                collisionModule.enabled = true;
                collisionModule.collidesWith = objectToExtinguish;
            }
            else
            {
                extintorParticles.Stop(); // Para as partículas quando o botão do mouse é solto

                // Desativa a colisão das partículas quando o botão do mouse é solto
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
                    // Mostra as partículas quando o extintor é solto
                    extintorParticles.Play();
                }
            }
            else
            {
                // Atualize a posição do objeto enquanto estiver sendo carregado pelo jogador.
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
                    // Pegue o objeto quando o jogador pressiona "Q" e está dentro da distância máxima.
                    isBeingCarried = true;
                    canPickUp = false;
                    wasPickedUp = true;

                    // Esconde as partículas quando o extintor é pego
                    extintorParticles.Stop();

                    Debug.Log("Pegou o Objeto");
                    transform.parent = mao; // Torna a mão o pai do objeto
                    GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
    }
}
