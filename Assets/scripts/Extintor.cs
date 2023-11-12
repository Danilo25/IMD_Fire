using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extintor : MonoBehaviour
{
    public bool isBeingCarried = false;
    public Camera PlayerCamera;
    public Transform originalParent;
    public Transform mao;

    public float carga = 20.0f;
    public float decremento = 5.0f;

    public bool canPickUp = true;
    public float maxDistance = 2.0f; // Distância máxima para pegar o objeto

    private ActionsPrompt actionsPrompt;
    [SerializeField] string promptText = "Pegar (Q)";

    public GameObject origemParticulas; // Objeto que contém as partículas
    public ParticleSystem extintorParticles; // Referência para o sistema de partículas

    public LayerMask objectToExtinguish; // Camada do objeto a ser apagado

    private bool wasPickedUp = false;

    void Start()
    {
        originalParent = transform.parent;
        actionsPrompt = GameObject.FindWithTag("ActionsPrompt").gameObject.GetComponent<ActionsPrompt>();
        PlayerCamera = GameObject.FindGameObjectWithTag("Player").transform.GetComponentInChildren<Camera>();
        mao = PlayerCamera.transform.Find("slot");
        origemParticulas = transform.Find("origem").gameObject;

        // Obtém a referência do sistema de partículas do objeto "origem"
        extintorParticles = origemParticulas.GetComponent<ParticleSystem>();

        var em = extintorParticles.emission;
        em.enabled = false;
    }

    void Update()
    {
        if (isBeingCarried)
        {
            if (Input.GetMouseButton(0) && carga > 0) // Botão esquerdo do mouse está pressionado
            {
                //extintorParticles.Play(); // Inicia as partículas quando o botão do mouse é pressionado
                var em = extintorParticles.emission;
                em.enabled = true;

                // Configura a colisão das partículas com o objeto específico
                var collisionModule = extintorParticles.collision;
                collisionModule.enabled = true;
                //collisionModule.collidesWith = objectToExtinguish;

                carga -= decremento * Time.deltaTime;
            }
            else
            {
                //extintorParticles.Stop(); // Para as partículas quando o botão do mouse é solto
                var em = extintorParticles.emission;
                em.enabled = false;

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
            }
            else
            {
                // Atualize a posição e a rotação do objeto enquanto estiver sendo carregado pelo jogador.
                Vector3 newPosition = mao.position;
                transform.position = newPosition;

                // Mantenha a orientação vertical (em pé)
                transform.up = Vector3.up;

                // Ajuste a rotação da mão para manter a orientação correta
                mao.rotation = Quaternion.LookRotation(PlayerCamera.transform.forward, Vector3.up);

                // Ajuste a rotação do origemParticulas para ser igual à rotação da câmera
                origemParticulas.transform.rotation = PlayerCamera.transform.rotation;
            }
        }
        else
        {
            float distanceToPlayer = Vector3.Distance(transform.position, PlayerCamera.transform.position);

            if (canPickUp && distanceToPlayer <= maxDistance)
            {
                actionsPrompt.Show(promptText);
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    // Pegue o objeto quando o jogador pressiona "Q" e está dentro da distância máxima.
                    isBeingCarried = true;
                    canPickUp = false;
                    wasPickedUp = true;

                    // Esconde as partículas quando o extintor é pego
                    //extintorParticles.Stop();

                    Debug.Log("Pegou o Objeto");
                    transform.parent = mao; // Torna a mão o pai do objeto
                    GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
    }
}
