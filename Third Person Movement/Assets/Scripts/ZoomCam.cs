using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ZoomCam : MonoBehaviour
{
    private CinemachineFreeLook freeLookCam;
    private CinemachineFreeLook.Orbit[] originalOrbit;

    public float minZoom = 0.5f;
    public float maxZoom = 1.0f;

    [AxisStateProperty]
    public AxisState zAxis = new AxisState(0,1,false,true,50f,0.1f,0.1f,"Mouse ScrollWheel",false);
    // Start is called before the first frame update
    void Start()
    {
        freeLookCam = GetComponentInChildren<CinemachineFreeLook>();
        if(freeLookCam != null)
        {
            originalOrbit = new CinemachineFreeLook.Orbit[freeLookCam.m_Orbits.Length];
            for(int i = 0; i < originalOrbit.Length; i++)
            {
                originalOrbit[i].m_Height = freeLookCam.m_Orbits[i].m_Height;
                originalOrbit[i].m_Radius = freeLookCam.m_Orbits[i].m_Radius;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(originalOrbit != null)
        {
            zAxis.Update(Time.deltaTime);
            float zoomScale = Mathf.Lerp(minZoom, maxZoom, zAxis.Value);

            for (int i = 0; i < originalOrbit.Length; i++)
            {
                originalOrbit[i].m_Height = freeLookCam.m_Orbits[i].m_Height * zoomScale;
                originalOrbit[i].m_Radius = freeLookCam.m_Orbits[i].m_Radius * zoomScale;
            }

            freeLookCam.m_Orbits = originalOrbit;
        }
    }
}
