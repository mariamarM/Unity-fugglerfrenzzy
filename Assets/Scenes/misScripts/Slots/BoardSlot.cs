public class BoardSlot : BaseSlot
{
    public Team assignedTeam; // Player o Enemy
    
    void Start()
    {
        slotType = SlotType.Board;
    }
    
    public override bool CanPlaceUnit(Team unitTeam)
    {
        return base.CanPlaceUnit(unitTeam) && assignedTeam == unitTeam;
    }
}