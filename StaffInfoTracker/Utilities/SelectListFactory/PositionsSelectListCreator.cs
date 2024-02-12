using Microsoft.AspNetCore.Mvc.Rendering;
using StaffInfoTracker.Models;

namespace StaffInfoTracker.Utilities.SelectListFactory;

public class PositionsSelectListCreator(IList<Position> positions, int selectedPositionId, bool isForFiltering) : SelectListCreator
{
    public override SelectList Create()
    {
        if (isForFiltering)
            positions.Insert(0, new Position() { PositionName = "Нічого", PositionId = 0 });

        return new SelectList(positions, nameof(Position.PositionId), nameof(Position.PositionName), selectedPositionId);
    }
}
