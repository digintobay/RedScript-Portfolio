using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHideObject : MonoBehaviour
{

    public float checkRadius = 2f;          // 카메라 주변 구체 반경
    public LayerMask ObstacleLayer;         // 감지할 레이어
    private List<Renderer> lastHidden = new List<Renderer>();

    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
     

        // 이전에 숨겼던 오브젝트 복원
        RestoreLastHidden();

        // 카메라 위치를 중심으로 구체 충돌체 감지
        Collider[] hits = Physics.OverlapSphere(mainCam.transform.position, checkRadius, ObstacleLayer);

        foreach (Collider hit in hits)
        {
            // 본인 Renderer
            Renderer[] renderers = hit.GetComponents<Renderer>();
            // 자식 Renderer
            Renderer[] childRenderers = hit.GetComponentsInChildren<Renderer>();

            foreach (Renderer r in renderers)
            {
                if (r != null && !lastHidden.Contains(r))
                {
                    r.enabled = false;
                    lastHidden.Add(r);
                }
            }

            foreach (Renderer r in childRenderers)
            {
                if (r != null && !lastHidden.Contains(r))
                {
                    r.enabled = false;
                    lastHidden.Add(r);
                }
            }
        }

        // 디버그용 구체
        Debug.DrawRay(mainCam.transform.position, Vector3.up * 0.01f, Color.red); // 위치 표시용
    }

    void RestoreLastHidden()
    {
        foreach (Renderer r in lastHidden)
        {
            if (r != null)
                r.enabled = true;
        }
        lastHidden.Clear();
    }
}
