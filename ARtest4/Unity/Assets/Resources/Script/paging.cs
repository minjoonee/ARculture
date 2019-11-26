using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(ScrollRect))]
public class paging : MonoBehaviour
{
    [Tooltip("the container the screens or pages belong to")]
    public Transform ScreensContainer;
    [Tooltip("how many screens or pages are there within the content")]
    public int Screens = 4;
    [Tooltip("which screen or page to start on (starting at 1 for you designers)")]
    public int StartingScreen = 1;

    private List<Vector3> _positions;
    private ScrollRect _scroll_rect;
    private Vector3 _lerp_target;
    private bool _lerp;
    
    // Use this for initialization
    void Start()
    {
        _scroll_rect = gameObject.GetComponent<ScrollRect>();
        _scroll_rect.inertia = false;
        _lerp = false;

        _positions = new List<Vector3>();

        if (Screens > 0)
        {
            for (int i = 0; i < Screens; ++i)
            {
                _scroll_rect.horizontalNormalizedPosition = (float)i / (float)(Screens - 1);
                _positions.Add(ScreensContainer.localPosition);
            }
        }

        _scroll_rect.horizontalNormalizedPosition = (float)(StartingScreen - 1) / (float)(Screens - 1);
    }

    void Update()
    {
        if (_lerp)
        {
            ScreensContainer.localPosition = Vector3.Lerp(ScreensContainer.localPosition, _lerp_target, 10 * Time.deltaTime);
            if (Vector3.Distance(ScreensContainer.localPosition, _lerp_target) < 0.001f)
            {
                _lerp = false;
            }
        }
    }

    // 손을 때면
    public void DragEnd()
    {
        if (_scroll_rect.horizontal)
        {
            _lerp = true;
            _lerp_target = FindClosestFrom(ScreensContainer.localPosition, _positions);
        }
    }

    // 드래그 시작하면
    public void OnDrag()
    {
        _lerp = false;
    }

    Vector3 FindClosestFrom(Vector3 start, List<Vector3> positions)
    {
        Vector3 closest = Vector3.zero;
        float distance = Mathf.Infinity;

        foreach (Vector3 position in _positions)
        {
            if (Vector3.Distance(start, position) < distance)
            {
                distance = Vector3.Distance(start, position);
                closest = position;
            }
        }

        return closest;
    }
}
