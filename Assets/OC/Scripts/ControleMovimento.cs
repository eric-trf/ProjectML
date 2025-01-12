using UnityEngine;

public class ControleMovimento : MonoBehaviour
{
    public float speed = 3.0f; 
    public float turnSpeed = 720.0f;
    private float IDAnim = 0.0f;
    private float horizontalInput;
    private float verticalInput;

    private Animator anim;

    Rigidbody rb;
    Captura captura;

    void Start ()
	{
	    anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        captura = GameObject.FindGameObjectWithTag("Event").GetComponent<Captura>();
    }

    void Update()
    {
        //ler teclas
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //muda a animação de movimento e habilita o som de andar
        if (horizontalInput == 0.0f && verticalInput == 0.0f) 
        {
            IDAnim = 0.0f;
            GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioSource>().enabled = false;
        }

        else if (horizontalInput != 0.0f || verticalInput != 0.0f) 
        {
            Debug.Log("se movendo");
            IDAnim = 0.5f;
            GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioSource>().enabled = true;
        }

        anim.SetFloat("IDAnim", IDAnim);

        //coletar vetores de frente e direita da camera no plano xz (descartando o valor de y)
        Vector3 fowardCam = Camera.main.transform.TransformDirection(Vector3.forward);
        fowardCam.y=0.0f;
        Vector3 rightCam = Camera.main.transform.TransformDirection(Vector3.right);
        rightCam.y=0.0f;

        //vetor de frente e direita do personagem relativo a camera
        Vector3 fowardRelative = verticalInput*fowardCam;
        Vector3 rightRelative = horizontalInput*rightCam;

        //vetor de movimento final, unindo os vetores de frente e direita relativos
        Vector3 moveRelativeDirection = rightRelative + fowardRelative;
        moveRelativeDirection.Normalize();

        //aplicar movimento ao personagem na direção do vetor final, levando em conta a velocidade configurada do personagem
        //transform.Translate(moveRelativeDirection * speed * Time.deltaTime, Space.World);
        rb.MovePosition(transform.position + moveRelativeDirection * speed * Time.deltaTime); 
        
        //rotacionar o modelo do personagem para a direção que ele está andando
        if (moveRelativeDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveRelativeDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards (transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cervo"))
        {
            captura.CapturaFeita();
        }
    }
}
