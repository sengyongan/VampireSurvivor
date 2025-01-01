using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

/* 相机跟随玩家 */
public class CameraController : MonoBehaviour
{
    private Transform target;
    public float moveSpeed;
    public CinemachineVirtualCamera virtualCamera;/* 引用场景中的虚拟相机组件 */
    private CinemachineBasicMultiChannelPerlin noiseProfile;/* 控制相机的抖动效果,Perlin噪声算法生成 */
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerHealthController.instance.transform;

        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        noiseProfile = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetVec3 = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetVec3, moveSpeed * Time.deltaTime);
    }
    /* 开始震动屏幕 */
    public void CameraShake(float duration = 0.25f, float amplitude = 1f, float frequency = 1f)/* 抖动持续时间duration,抖动幅度amplitude,抖动频率frequency */
    {
        if (noiseProfile != null)
        {
            noiseProfile.m_AmplitudeGain = amplitude;
            noiseProfile.m_FrequencyGain = frequency;
            Invoke(nameof(StopShaking), duration);
        }
    }
    /* 停止震屏 */
    private void StopShaking()
    {
        if (noiseProfile != null)
        {
            noiseProfile.m_AmplitudeGain = 0f;
            noiseProfile.m_FrequencyGain = 0f;
        }
    }
}
