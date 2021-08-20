using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodInstantiator : MonoBehaviour
{
    [SerializeField] private PathMapCalculator mapCalculator;
    [SerializeField] private GameObject foodPrefab;

    private PathGrid pathGrid;

    private void Start()
    {
        pathGrid = mapCalculator.pathGrid;

        for (int width = 0; width < pathGrid.Width; width++)
        {
            for (int height = 0; height < pathGrid.Height; height++)
            {
                if (pathGrid.GetValue(width, height).IsObstacle)
                    continue;

                Vector3 location = pathGrid.GetWorldPosition(width, height);
                Collider2D[] colliders = Physics2D.OverlapCircleAll(location, 0.25f);
                if (colliders.Length > 0)
                    continue;

                Instantiate(foodPrefab, location, Quaternion.identity, this.transform);
            }
        }
    }
}
