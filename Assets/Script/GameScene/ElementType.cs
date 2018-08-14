using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementType {
    public enum Element
    {
        None = 0,
        Fire,
        Water,
        Lighting
    }
    public float TypeResult(ElementType.Element myElement, ElementType.Element targetElement)
    {
        if (myElement == targetElement)
        {
            return 1f;
        }
        else
        {
            if (myElement == Element.Fire)
            {
                return (targetElement == Element.Lighting) ? 2f : 0.5f;
            }
            if (myElement == Element.Water)
            {
                return (targetElement == Element.Fire) ? 2f : 0.5f;
            }
            if (myElement == Element.Lighting)
            {
                return (targetElement == Element.Water) ? 2f : 0.5f;
            }
        }
        return 0; 
    }
}
