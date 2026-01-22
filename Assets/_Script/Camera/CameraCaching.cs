using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraCaching : Singleton<CameraCaching>
{
    public CinemachineVirtualCamera mainCamera;
    public CinemachineVirtualCamera lookCamera;
    public CinemachineVirtualCamera aimCamera;
    ThirdPersonCamera lookThirdPersonCamera;
    ThirdPersonCamera aimThirdPersonCamera;
    Camera aimCam;
    public bool isAimingCamera;

    protected override void Awake()
    {
        base.Awake();
        GameUIManager.Instance.UseCursor(false);
        lookThirdPersonCamera = lookCamera.Follow.GetComponent<ThirdPersonCamera>();
        aimThirdPersonCamera = aimCamera.Follow.GetComponent<ThirdPersonCamera>();
    }

    public void SwitchToAimCamera()
    {
        if(mainCamera == aimCam)
            return;

        aimThirdPersonCamera.SynchnizeOther(lookThirdPersonCamera);
        mainCamera = aimCamera;
        lookCamera.gameObject.SetActive(false);
        aimCamera.gameObject.SetActive(true);
        isAimingCamera = true;
    }

    public void SwitchToNormalCamera()
    {
        if(mainCamera == lookCamera)
            return;

        // Ray aimRay = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2));
        lookThirdPersonCamera.SynchnizeOther(aimThirdPersonCamera);
        
        mainCamera = lookCamera;
        lookCamera.gameObject.SetActive(true);
        aimCamera.gameObject.SetActive(false);
        isAimingCamera = false;
    }

    public void GetScreenCenterRay()
    {
        Camera.main.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2));
    }

    public Vector3 GetLookDirection() => mainCamera.transform.forward;


}
