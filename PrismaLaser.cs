using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismaLaser : MonoBehaviour
{

    private Ray ray;
    private RaycastHit hit;
    [SerializeField] public float laserLength;
    public int reflections;
    [HideInInspector] public float remainingLength;
    public List<GameObject> mirrors = new List<GameObject>();
    public float maxLength;

    public LineRenderer lineRenderer;
    public Vector3 laserVector;

    public bool laserControl = false;
    
    


    private void Awake() {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }
    private void Start() {
    }
    void Update()
    {
        ray = new Ray(transform.position,laserVector);

        lineRenderer.positionCount= 1;
        lineRenderer.SetPosition(0,transform.position);
        remainingLength = laserLength;

        for (int i = 0; i < reflections; i++)
        {
            if(Physics.Raycast(ray.origin,ray.direction,out hit ,remainingLength))
            {
                lineRenderer.positionCount +=1;
                lineRenderer.SetPosition(lineRenderer.positionCount -1,hit.point);
                remainingLength -=Vector3.Distance(ray.origin , hit.point);
                ray = new Ray(hit.point,Vector3.Reflect(ray.direction,hit.normal));
                if(hit.collider.tag == "DoubleEnd")
                {
                    StartCoroutine(hit.transform.gameObject.GetComponent<End>().EndPointComplated());
                    for (int j = 0; j < mirrors.Count; j++)
                    {
                        mirrors[j].transform.GetComponent<ClickObje>().mouseRotateSpeed = 0 ;
                    }
                    break;
                }
                if(hit.collider.tag == "Mirror")
                {
                    if(!mirrors.Contains(hit.transform.gameObject))
                    {
                        mirrors.Add(hit.transform.gameObject);
                    }
                   
                }

                else if(hit.collider.tag !="Mirror")
                {
                    break;
                }
               
            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount -1,ray.origin + ray.direction * remainingLength);
            }
        }
        

        
    }

    public float currentTime;
    private float timeLimit = 10;
    public void LaserLengthControl()
    {
        if(laserControl == true)
        {
            currentTime += Time.deltaTime;
            laserLength=Mathf.Lerp(0, maxLength, 1*currentTime / timeLimit);
        }
        
        else 
        {
            currentTime -= Time.deltaTime;
            laserLength=Mathf.Lerp(0, maxLength, 1*currentTime / timeLimit);
            Debug.Log(remainingLength);
        }
    }
}
