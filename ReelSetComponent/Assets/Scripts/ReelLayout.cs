using UnityEngine;
using System.Collections.Generic;

public class ReelLayout : MonoBehaviour
{
    [SerializeField]
    private GameObject symbolPrefab;
    [SerializeField]
    private int visibleSymbols;

    private List<GameObject> symbolObjects = new List<GameObject>();
    private AnimationCurve layoutCurve;
    private float topSymbolPosition;
    private float symbolEnterPosition;

    // Use this for initialization
    void Start()
    {
        if (symbolPrefab == null)
        {
            return;
        }

        layoutCurve = AnimationCurve.Linear(0, 0, 1, -10);

        // Get the position of the top symbol. This will never change.
        // All other symbol positions will be calculation from this value.
        int totalSymbols = visibleSymbols;

        symbolEnterPosition = 1.0f / (totalSymbols + 1);
        topSymbolPosition = symbolEnterPosition;

        for (int i = 1; i <= totalSymbols; ++i)
        {
            float y = layoutCurve.Evaluate(i * topSymbolPosition);

            GameObject symbol = Instantiate(symbolPrefab, new Vector3(0, y, -1), Quaternion.identity) as GameObject;
            symbol.transform.parent = gameObject.transform;
            symbolObjects.Add(symbol);
        }
    }

    void Update()
    {
        // Calculate the position of all symbols based on the position of the top symbol.
        float symbolPosition = topSymbolPosition;

        foreach (GameObject symbol in symbolObjects)
        {
            float y = layoutCurve.Evaluate(symbolPosition);
            Vector3 currentPos = symbol.transform.position;

            currentPos.y = y;
            symbol.transform.position = currentPos;
            symbolPosition += symbolEnterPosition;
        }

        topSymbolPosition += 0.01f;

        if (layoutCurve.Evaluate(symbolPosition - symbolEnterPosition) <= -10.0f)
        {
            // Add a new symbol to the start of the list.
            GameObject symbol = Instantiate(symbolPrefab, new Vector3(0, layoutCurve.Evaluate(symbolEnterPosition), -1), Quaternion.identity) as GameObject;
            symbol.transform.parent = gameObject.transform;
            symbolObjects.Insert(0, symbol);

            // Remove the last symbol.
            Object.DestroyImmediate(symbolObjects[symbolObjects.Count - 1]);
            symbolObjects.RemoveAt(symbolObjects.Count - 1);

            topSymbolPosition = symbolEnterPosition;
        }
    }
}
