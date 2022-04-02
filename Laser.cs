using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Laser : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    private GameObject prismaHolder;
    private bool oneTimeRunMirror;
    [SerializeField] private float remainingLength;
    [SerializeField] 
    public int reflections;
    public float maxLength;
    public float currentLaserLength;
    public Vector3 laserVector;
    private LineRenderer lineRenderer;
    public List<GameObject> laserMirrors = new List<GameObject>();
    


    public float laserMovingSpeed =0.5f;
    private float laserSpeedHolder;

    private float laserPosHolder;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Start() 
    {
        oneTimeRunMirror = false;
    }
    // Update is called once per frame
    void Update()
    {
        ray = new Ray(transform.position,laserVector);

        lineRenderer.positionCount= 1;
        lineRenderer.SetPosition(0,transform.position);
        
        remainingLength = maxLength;
        //laser();
        for (int i = 0; i < reflections; i++)
        {
            if(Physics.Raycast(ray.origin,ray.direction,out hit ,remainingLength))
            {
                lineRenderer.positionCount +=1;
                lineRenderer.SetPosition(lineRenderer.positionCount -1,hit.point);
                if (hit.collider.tag == "Mirror")
                {
                    hit.transform.gameObject.GetComponent<Mirror>().state = Mirror.State.Reflecting;
                    if(!laserMirrors.Contains(hit.transform.gameObject))
                    {
                        laserMirrors.Add(hit.transform.gameObject);
                    }
                    remainingLength -=Vector3.Distance(ray.origin , hit.point);
                    ray = new Ray(hit.point,Vector3.Reflect(ray.direction,hit.normal));
                    laserPosHolder = lineRenderer.GetPosition(lineRenderer.positionCount-1).z;
                }

                else if(hit.collider.tag == "Glass")
                {
                    prismaHolder = hit.transform.gameObject;
                    prismaHolder.GetComponent<PrismaLaser>().laserControl = true;
                    hit.transform.gameObject.GetComponent<PrismaLaser>().LaserLengthControl();
                    hit.transform.gameObject.transform.GetChild(0).GetComponent<PrismaLaser>().laserControl = true;
                    hit.transform.gameObject.transform.GetChild(0).GetComponent<PrismaLaser>().LaserLengthControl();
                    break;
                }

                /*else if (hit.collider.tag != "Glass" && prismaHolder != null)
                {
                    prismaHolder.GetComponent<PrismaLaser>().laserLength = 0;
                    prismaHolder.transform.gameObject.transform.GetChild(0).GetComponent<PrismaLaser>().laserLength = 0;
                } */
                
                else if (hit.collider.tag == "End")
                {
                    for (int j = 0; j < PlaceObjectOnGrid.Instance.mirrors.Count; j++)
                    {
                        PlaceObjectOnGrid.Instance.mirrors[j].transform.GetChild(0).GetComponent<ClickObje>().mouseRotateSpeed = 0 ;
                    }
                    StartCoroutine(hit.transform.gameObject.GetComponent<End>().EndPointComplated());
                    break;
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
    private bool laserControl = true;

    public void laser()
    {
        if(lineRenderer.positionCount > 2)
        {
            maxLength = Mathf.Lerp(lineRenderer.GetPosition(lineRenderer.positionCount-1).z, maxLength, laserSpeedHolder);
            laserSpeedHolder += laserMovingSpeed * Time.deltaTime;
        }
        
    }

    public void LaserLengthControl()
    {
        if(laserControl == true)
        {
            currentTime += Time.deltaTime;
            currentLaserLength=Mathf.Lerp((maxLength-remainingLength), 200, 0.00001f);

            remainingLength = maxLength;
        }
        
    }

}
