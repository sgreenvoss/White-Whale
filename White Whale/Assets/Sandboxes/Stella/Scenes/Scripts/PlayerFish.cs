using Unity.VisualScripting;
using UnityEngine;

public class PlayerFish : MonoBehaviour
{
    int fishCount = 0;
    public GameObject net;
    BoxCollider boxCollider;
    [SerializeField] KeyCode fishKey = KeyCode.Mouse0; // left click
    Vector3 maxScale = Vector3.one * 3f;
    Vector3 scaleInc = Vector3.one * 0.1f;
    Vector3 currentScale;

    private void Start()
    {
        net.transform.localScale = Vector3.zero;
        boxCollider = net.GetComponent<BoxCollider>();
        boxCollider.size = net.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        currentScale = net.transform.localScale;
        if (Input.GetKey(fishKey) && currentScale.x < maxScale.x) // cube, so one attr can work as measure of scale.
        {
            net.transform.localScale += scaleInc;
        }
        if (Input.GetKeyUp(fishKey))
        {
            CatchFish();
            net.transform.localScale = Vector3.zero;
        }
        boxCollider.size = net.transform.localScale;
    }

    void CatchFish()
    {
        Collider[] hits = Physics.OverlapBox(
            net.transform.position,
            net.transform.localScale / 2f,
            net.transform.rotation
        );

        foreach (Collider col in hits)
        {
            Debug.Log("collision " + col.tag);
            if (col.CompareTag("Fish"))
            {
                fishCount++;
                Debug.Log(fishCount.ToString());
                Destroy(col.gameObject);
            }
        }
    }
}


