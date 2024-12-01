using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BicycleController : MonoBehaviour
{
    private BicycleService bicycleService;

    public TMP_InputField AddBrandInputField;
    public TMP_InputField AddModelInputField;

    public TMP_InputField UpdateBrandInputField;
    public TMP_InputField UpdateModelInputField;
    public TMP_InputField UpdateIdInputField;

    public TMP_InputField DeleteIdInputField;

    private void Start()
    {
        bicycleService = gameObject.AddComponent<BicycleService>();
    }

    public void GetBicycles()
    {
        bicycleService.GetBicycles();
    }

    public void AddBicycle()
    {
        Bicycle bicycle = new();
        bicycle.brand = AddBrandInputField.text;
        bicycle.model = AddModelInputField.text;

        bicycleService.CreateBicycle(bicycle);
    }

    public void UpdateBicycle()
    {
        Bicycle bicycle = new();
        bicycle.id = int.Parse(UpdateIdInputField.text);
        bicycle.brand = UpdateBrandInputField.text;
        bicycle.model = UpdateModelInputField.text;

        bicycleService.UpdateBicycle(bicycle.id, bicycle);
    }

    public void DeleteBicycle()
    {
        int id = int.Parse(DeleteIdInputField.text);

        bicycleService.DeleteBicycle(id);
    }
}
