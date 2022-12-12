using UnityEngine;
using Unity.Netcode;
using Cinemachine;
public class CameraController : Singleton<CameraController>
{
    [SerializeField] private CinemachineVirtualCamera cam;

    // Start is called before the first frame update

    [SerializeField]
    private float amplitudeGain = 0.2f;

    [SerializeField]
    private float frequencyGain = 0.2f;

    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void FollowPlayer(Transform transform)
    {
        // not all scenes have a cinemachine virtual camera so return in that's the case
        if (cinemachineVirtualCamera == null) return;

        cinemachineVirtualCamera.Follow = transform;

        var perlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = amplitudeGain;
        perlin.m_FrequencyGain = frequencyGain;
    }
}
