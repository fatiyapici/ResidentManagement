namespace ResidentManagement;

public class ApartmentDropdownOptions
{
    public int ID { get; set; }
    public string NumberFloorInfo { get; set; }

    public ApartmentDropdownOptions(int id, string numberFloorInfo)
    {
        this.ID = id;
        this.NumberFloorInfo = numberFloorInfo;
    }
}