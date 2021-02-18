
using UnityEngine;
using UnityEngine.UI;

public class CarriagePortrait : MonoBehaviour
{
    [SerializeField] private RawImage carriageAvatar;

    public void Init(GameObject photoTarget)
    {
        carriageAvatar.texture = Root.PreviewStage.MakeIconOf(photoTarget);
    }
}
