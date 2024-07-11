using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class Car : MonoBehaviour
{
    public Route route;

    public Transform bottomTransform;
    public Transform bodyTransform;

    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    private ParticleSystem smokeFX;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float danceValue;

    [SerializeField]
    private float durationMultiplier;

    private void Start()
    {
        bodyTransform.DOLocalMoveY(danceValue, .1f) // dịch chuyển animation theo chiều Y
                     .SetLoops(-1, LoopType.Yoyo) // -1 trong DOTween là lặp lại vô hạn
                     .SetEase(Ease.Linear);

        // SetEase(Ease.Linear): Thiết lập easing tuyến tính cho animation,
        // nghĩa là giá trị sẽ thay đổi đều đặn từ bắt đầu đến kết thúc
        // mà không có sự thay đổi về tốc độ.
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Car othercar))
        {
            StopDancingAnim();
            rb.DOKill(false); // DOKill dùng để dừng các hoạt động trong TWEENING

            // add Explotion
            Vector3 hitpoint = collision.contacts[0].point;
            AddExplotionForce(hitpoint);
            smokeFX.Play();

            Game.Instance.OnCarCollision?.Invoke();
        }
    }

    private void AddExplotionForce(Vector3 point)
    {
        rb.AddExplosionForce(400f, point, 3f);
        rb.AddForceAtPosition(Vector3.up * 2f, point, ForceMode.Impulse);
        rb.AddTorque(new Vector3(GetRandomAngle(), GetRandomAngle(), GetRandomAngle()));
        // addtorque: lực xoắn
    }

    private float GetRandomAngle()
    {
        float angle = 10f;
        float rand = Random.value;
        return rand > 0.5f ? angle : -angle;
    }




    public void Move(Vector3[] path)
    {
        rb.DOLocalPath(path, 2f * durationMultiplier * path.Length)
          .SetLookAt(0.01f, Vector3.right) 
          .SetEase(Ease.Linear);

        // SetLookAt: đảm bảo rằng đối tượng sẽ "nhìn"
        // về hướng di chuyển trong suốt quá trình di chuyển.
    }

    public void StopDancingAnim()
    {
        bodyTransform.DOKill(true);
    }

    public void SetColor(Color color)
    {
        meshRenderer.sharedMaterials[0].color = color;
    }
}
