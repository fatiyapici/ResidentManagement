namespace ResidentManagement;

public class ApartmentDropdownOptions
{
    public int ID { get; set; }
    public string NumberFloorBlockInfo { get; set; }

    public ApartmentDropdownOptions(int id, string numberFloorBlockInfo)
    {
        this.ID = id;
        this.NumberFloorBlockInfo = numberFloorBlockInfo;
    }
}