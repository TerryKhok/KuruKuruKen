using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    [SerializeField] GameObject player1, player2;
    [SerializeField] float MinSize;
    Vector3 center;
    float targetWidth, targetHeight;
    Camera cam;
    float smoothSpeed = 5f;
    float fixedDeltaTime;
    [SerializeField] float waitsec = 4f;
    public bool death = false;

    //particle
    public ParticleSystem kamifubukiP1;
    public ParticleSystem kamifubukiP2;

    void Start()
    {
        cam = GetComponent<Camera>();
        fixedDeltaTime = Time.fixedDeltaTime;
    }

    void FixedUpdate()
    {
        //==========両プレイヤーいる時中央点を計算し、カメラを中央点に移動、両プレイヤー映せるようにZoom Outする
        if (player1 != null && player2 != null)
        {
            center = (player1.transform.localPosition + player2.transform.localPosition) / 2.0f;
            transform.position = Vector3.Lerp(transform.position, new Vector3(center.x, center.y, transform.position.z), smoothSpeed * fixedDeltaTime);

            targetWidth = Mathf.Abs(player1.transform.localPosition.x - player2.transform.localPosition.x) / 2;
            targetHeight = Mathf.Abs(player1.transform.localPosition.y - player2.transform.localPosition.y) / 2;

            if (Mathf.Max(targetWidth, targetHeight) > MinSize)
            {
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, Mathf.Max(targetWidth, targetHeight), smoothSpeed * fixedDeltaTime);
            }
        }
        //==========片方のプレイヤー死んだ時、カメラを活けているプレイヤーの場所に移動==========
        else if (player1 != null && player2 == null)
        {
            StartCoroutine(wait());

            if (death)
            {
                ParticleSystem newParticle = Instantiate(kamifubukiP1, player1.transform);
                newParticle.Play();
                Destroy(newParticle.gameObject, 5f);

                Vector3 targetPosition = new Vector3(player1.transform.position.x, player1.transform.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * fixedDeltaTime);
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, MinSize / 2, smoothSpeed * fixedDeltaTime);
            }
        }
        else if (player1 == null && player2 != null)
        {
            StartCoroutine(wait());
            if (death)
            {
                ParticleSystem newParticle = Instantiate(kamifubukiP2, player2.transform);
                newParticle.Play();
                Destroy(newParticle.gameObject, 5f);

                Vector3 targetPosition = new Vector3(player2.transform.position.x, player2.transform.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * fixedDeltaTime);
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, MinSize - 4f, smoothSpeed - 4f * fixedDeltaTime);
            }
        }
        //================================================================================
    }

    private IEnumerator wait()  //wait関数
    {
        yield return new WaitForSeconds(waitsec);
        death = true;
    }
}
