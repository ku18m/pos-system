using PosSystem.Application.Contracts.Validations.Unit;

namespace PosSystem.Application.Contracts.Unit
{
    public class UnitCreationContract
    {
        [UniqueUnitName]
        public string Name { get; set; }
        public string? Note { get; set; }
    }
}
