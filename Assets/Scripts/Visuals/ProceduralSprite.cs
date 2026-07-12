using DG.Tweening;
using UnityEngine;

public class ProceduralSprite : MonoBehaviour
{
    [SerializeField] private Transform circleMaskTransform;
    [SerializeField] private float duration = 0.5f;

    // Vector3(1,1,1) clips the corners perfectly, leaving only a circle visible
    private readonly Vector3 circleScale = Vector3.one;
    
    // Vector3(1.42f, 1.42f, 1f) is mathematically large enough to reveal the square's corners
    private readonly Vector3 squareScale = new Vector3(1.42f, 1.42f, 1f);

    public void MorphToCircle()
    {
        circleMaskTransform.DOScale(circleScale, duration)
            .SetEase(Ease.InOutQuad);
    }

    public void MorphToSquare()
    {
        circleMaskTransform.DOScale(squareScale, duration)
            .SetEase(Ease.InOutQuad);
    }
}
