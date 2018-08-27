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
    public float TypeResult(ElementType.Element myElement, ElementType.Element targetElement, bool isItem,int itemID)
    {
        float itemBuffer = 1;
        if(itemID == 1003)
        {
            itemBuffer = 1.5f;
            if (myElement == targetElement)
            {
                return 1f * itemBuffer;
            }
            else
            {
                if (myElement == Element.Fire)
                {
                    return (targetElement == Element.Lighting) ? 2f * itemBuffer : 0.5f * itemBuffer;
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
        }
        else if (itemID == 1004)
        {
            itemBuffer = 1.5f;
            if (myElement == targetElement)
            {
                return 1f * itemBuffer;
            }
            else
            {
                if (myElement == Element.Fire)
                {
                    return (targetElement == Element.Lighting) ? 2f : 0.5f;
                }
                if (myElement == Element.Water)
                {
                    return (targetElement == Element.Fire) ? 2f * itemBuffer : 0.5f * itemBuffer;
                }
                if (myElement == Element.Lighting)
                {
                    return (targetElement == Element.Water) ? 2f : 0.5f;
                }
            }
        }
        else if (itemID == 1005)
        {
            itemBuffer = 1.5f;
            if (myElement == targetElement)
            {
                return 1f * itemBuffer;
            }
            else
            {
                if (myElement == Element.Fire)
                {
                    return (targetElement == Element.Lighting) ? 2f : 0.5f;
                }
                if (myElement == Element.Water)
                {
                    return (targetElement == Element.Fire) ? 2f : 0.5f ;
                }
                if (myElement == Element.Lighting)
                {
                    return (targetElement == Element.Water) ? 2f * itemBuffer : 0.5f * itemBuffer;
                }
            }
        }
        else
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
        }    
        return 0;
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
