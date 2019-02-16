using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telekinesis : MonoBehaviour
{
    //for telekinesis

    public GameObject plane;
    public GameObject effect;
    public float telekinesisSmooth;
    public int cost = 5;
    public AudioClip soundEffect;

    private Vector3 velocity = Vector3.zero;
    Rigidbody rb1 = null;
    bool telekinesis = false;
    GameObject createdPlane = null;
    Transform telekinesisItem = null;
    Vector2 previusPosition;
    Vector2 currentPosition;
    GameObject createdEffect = null;
    Character character;
    float costTick=0.5f;
    float tempTick;
    AudioSource soundSource;
    void Start()
    {
        character = GetComponent<Character>();
        soundSource = (AudioSource)gameObject.AddComponent<AudioSource>();
        soundSource.loop = true;
        soundSource.playOnAwake = false;
        soundSource.clip = soundEffect;
        soundSource.volume = 0.3f;
    }

    void FixedUpdate()
    {
        Telekines();
    }



    void Telekines()
    {
        if (Input.GetKey(KeyCode.Mouse0)&&character.getMP()>=cost)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Enviroment")))
            {
                previusPosition = currentPosition;
                currentPosition = new Vector2(hit.point.x, hit.point.y);
                if (createdPlane == null)
                {
                    createdPlane = (GameObject)Instantiate(plane, new Vector3(hit.point.x, hit.point.y, hit.transform.position.z), Quaternion.Euler(-90, 0, 0));
                    rb1 = hit.transform.gameObject.GetComponent<Rigidbody>();
                    rb1.useGravity = false;
                    telekinesisItem = hit.transform.gameObject.GetComponent<Transform>();
                    telekinesis = true;
                    createdEffect = (GameObject)Instantiate(effect, new Vector3(hit.point.x, hit.point.y - 0.2f, hit.transform.position.z), Quaternion.Euler(-90, 0, 0));
                }
                telekinesisItem.position = Vector3.SmoothDamp(telekinesisItem.position, new Vector3(hit.point.x, hit.point.y, hit.transform.position.z), ref velocity, telekinesisSmooth);
                createdEffect.transform.position = telekinesisItem.position;
                //Расход маны
                tempTick += Time.deltaTime;
                if (tempTick > costTick)
                {
                    character.setMP(character.getMP() - cost);
                    tempTick = 0;
                }
                //Проигрывание звука
                if(!soundSource.isPlaying)
                soundSource.Play();
            }
        }
        if ((!Input.GetKey(KeyCode.Mouse0) || character.getMP() < cost) && telekinesis)
        {
            soundSource.Stop();
            telekinesis = false;
            telekinesisItem = null;
            rb1.useGravity = true;
            Destroy(createdPlane);
            Destroy(createdEffect);
            Vector2 force = (currentPosition - previusPosition);
            rb1.AddForce(new Vector3(10000 * force.x, 15000 * force.y, 0));
        }
        if (rb1 != null && !telekinesis) rb1.useGravity = true;
    }
}
