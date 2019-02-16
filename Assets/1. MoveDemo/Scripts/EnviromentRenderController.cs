using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Скрипт отрисовки необходимой глубины уровня
/// </summary>
public class EnviromentRenderController : MonoBehaviour
{
    public GameObject player;
    public GameObject enviroment;
    public float rayLength = 1000f;
    public float y=0.3f;

    private List<GameObject> enviromentObjects;
    private int playerPositionDepth = 0;
    private float range;
    int lenghti = 1;
    int lenghtj = 10;

    void Start()
    {
        Transform[] allChildren = enviroment.GetComponentsInChildren<Transform>();
        enviromentObjects = new List<GameObject>();
        foreach (Transform child in allChildren)
        {
            if (!child.gameObject.name.Equals("LevelEnviroment"))
            enviromentObjects.Add(child.gameObject);
        }
        range = player.GetComponent<CharacterMovementController>().GetStartPosition().z - enviroment.transform.position.z;

    }

    void FixedUpdate()
    {   
        RenderDepth();
      //  RenderVision();
       
    }
    //блок отрисовки только необходимой глубины уровня
    void RenderDepth()
    {
        foreach (GameObject obj in enviromentObjects)
        {
            if ((obj.transform.position.z - player.transform.position.z) < range) obj.SetActive(false);
            else obj.SetActive(true);

            if (obj.layer == LayerMask.NameToLayer("Enviroment"))
            {
               // obj.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    // блок отрисовки только видимых объектов
    void RenderVision()
    {
        for (int i = 0; i < lenghti; i++)
        {
            
            Vector3 head = GameObject.Find("head").transform.position;
            Vector3 origin = new Vector3(player.transform.position.x, y, player.transform.position.z);
           // Vector3 origin = new Vector3(head.x, y, head.z);
           
            Vector3 direction = new Vector3(0,-1,0);
            float cos = Mathf.Cos(18 * 3.14f / 180);
            float sin = Mathf.Sin(18 * 3.14f / 180);
            for (int j = 0; j < lenghtj; j++)
            {
                Vector3 viewDirection = player.GetComponent<CharacterMovementController>().GetViewDirrection();
                direction = new Vector3((direction.x*cos-direction.y*sin), direction.x * sin + direction.y * cos, 0);

                Debug.Log(direction);
                // Debug.Log(j+" origin:"+origin +" direction:"+ direction + "box:" + GameObject.Find("box").transform.position );
                //direction = GameObject.Find("head").transform.right;
                Ray ray = new Ray(origin, direction);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, rayLength,LayerMask.GetMask("Enviroment")))
                {
                    Debug.Log(hit.transform.gameObject.name);
                    Transform objectHit = hit.transform;
                    objectHit.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    Cast(hit.transform.position, transform.right);
                    

                }
            }
        }
    }

    void Cast(Vector3 pos, Vector3 direction)
    {
        RaycastHit hit;
        Ray ray1 = new Ray(pos, direction);
        if (Physics.Raycast(ray1, out hit, rayLength, LayerMask.GetMask("Enviroment")))
        {
            Transform objectHit1 = hit.transform;
            objectHit1.gameObject.GetComponent<MeshRenderer>().enabled = true;
            // Do something with the object that was hit by the raycast.
            Cast(hit.transform.position, transform.right);
        }
    }

   
}
