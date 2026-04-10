public class BenchSlot : BaseSlot
{
    void Start()
    {
        slotType = SlotType.Bench;
    }
    
    public override bool CanPlaceUnit(Team unitTeam)
    {
        // Banquillo solo para el jugador
        return base.CanPlaceUnit(unitTeam) && unitTeam == Team.Player;
    }
}