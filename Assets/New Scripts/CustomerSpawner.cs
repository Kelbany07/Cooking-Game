using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject customerPrefab;
    public float spawnInterval = 3f;
    public int maxCustomers = 3;

    private Customer[] activeCustomers;

    private void Start()
    {
        activeCustomers = new Customer[spawnPoints.Length];
        InvokeRepeating(nameof(TrySpawnCustomer), 1f, spawnInterval);
    }

    private void TrySpawnCustomer()
    {
        int currentCount = 0;
        foreach (var c in activeCustomers)
        {
            if (c != null) currentCount++;
        }
        if (currentCount >= maxCustomers) return;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (activeCustomers[i] == null)
            {
                GameObject custObj = Instantiate(customerPrefab, spawnPoints[i].position, Quaternion.identity);
                Customer cust = custObj.GetComponent<Customer>();
                cust.Init(this);
                activeCustomers[i] = cust;
                break;
            }
        }
    }

    public void CustomerLeft(Customer customer, bool served)
    {
        for (int i = 0; i < activeCustomers.Length; i++)
        {
            if (activeCustomers[i] == customer)
            {
                activeCustomers[i] = null;
                break;
            }
        }
        // You can add score logic here if served == true
    }
}