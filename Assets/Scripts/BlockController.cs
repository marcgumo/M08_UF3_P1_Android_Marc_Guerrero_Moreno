using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [Header("Spawner")]
    SpawnerController spawner;

    [Header("Block Settings")]
    public LayerMask blockLayer;
    public float speed;
    GameObject blockInGroundParent;

    [Header("Ground Transform")]
    public Transform checkGroundTransform;
    float checkLength = 0.2f;

    [Header("Audio Settings")]
    public AudioClip clipStack;
    AudioSource source;

    bool inGround;

    void Start()
    {
        spawner = FindObjectOfType<SpawnerController>();
        blockInGroundParent = GameObject.Find("BlocksInGround");
        source = GameObject.Find("AudioSource").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!inGround)
        {
            int blockNumber = blockInGroundParent.transform.childCount;

            Collider[] blockCollider = Physics.OverlapBox(transform.position, transform.GetComponent<Collider>().bounds.extents + Vector3.one * 0.1f,
                transform.rotation, blockLayer);

            print(blockCollider.Length);

            if (blockCollider.Length > 1)
            {
                Debug.Log("Collision detected");
                if (blockNumber > 0)
                {
                    Debug.Log("Number of blocks > 0");
                    foreach (Collider collider in blockCollider)
                    {
                        Debug.Log("Position of collider" + collider.transform.position);
                        if (collider.gameObject.Equals(blockInGroundParent.transform.GetChild(blockNumber - 1).gameObject)/*Mirar esto*/)
                        {
                            transform.position = new Vector3(transform.position.x,
                                collider.transform.position.y + collider.bounds.extents.y +
                                GetComponent<Collider>().bounds.extents.y);

                            source.PlayOneShot(clipStack);
                            spawner.SetCanSpawn(true);
                            inGround = true;
                            transform.parent = blockInGroundParent.transform;
                            return;
                        }
                        else
                        {
                            spawner.SetCanSpawn(true);
                            Debug.Log("Block destroyed");
                            Destroy(gameObject);
                        }
                    }
                }
                else
                {
                    Debug.Log("First block");
                    transform.position = new Vector3(transform.position.x,
                        blockCollider[0].transform.position.y + blockCollider[0].bounds.extents.y +
                        GetComponent<Collider>().bounds.extents.y);

                    source.PlayOneShot(clipStack);
                    spawner.SetCanSpawn(true);
                    inGround = true;
                    transform.parent = blockInGroundParent.transform;
                }
            }
            transform.position += speed * Time.deltaTime * Vector3.down;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(checkGroundTransform.position, checkGroundTransform.position + Vector3.down * checkLength);
        Gizmos.DrawWireCube(transform.position, GetComponent<Collider>().bounds.size + Vector3.one * 0.1f);
    }
}
