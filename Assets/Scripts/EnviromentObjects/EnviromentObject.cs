using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnviromentObject : MonoBehaviour
{
    static Color highlightColor = Color.yellow;

    public bool grabbable;

    [HideInInspector]
    public Hand handIAmGrabbedBy;

    protected Renderer[] renderers;
    protected Color[] originalColors;
    protected bool isGrabbed;
    protected bool stopCoroutine = false;

    public abstract void Grab(Hand hand);
    public abstract void Degrab();

    protected void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        originalColors = new Color[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            originalColors[i] = renderers[i].material.color;
        }
    }

    public void Highlight()
    {
        foreach(Renderer r in renderers)
        {
            r.material.color = highlightColor;
        }
    }

    public void Dehighlight()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = originalColors[i];
        }
    }

    protected IEnumerator Move(Transform what, Vector3 where)
    {
        while (what.localPosition != where && !stopCoroutine)
        {
            what.localPosition = Vector3.LerpUnclamped(what.localPosition, where, 20 * Time.deltaTime);

            yield return 0;
        }
    }
}