using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Car : MonoBehaviour
{
    public Route route;

    public Transform bottomTransform;
    public Transform bodyTransform;

    [SerializeField]
    private MeshRenderer meshRenderer;

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

    public void Move(Vector3[] path)
    {
        rb.DOLocalPath(path, 2f * durationMultiplier * path.Length)
          .SetLookAt(0.01f, Vector3.right) 
          .SetEase(Ease.Linear);

        // SetLookAt: đảm bảo rằng đối tượng sẽ "nhìn"
        // về hướng di chuyển trong suốt quá trình di chuyển.
    }

    public void SetColor(Color color)
    {
        meshRenderer.sharedMaterials[0].color = color;
    }
}
