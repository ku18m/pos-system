using PosSystem.Application.Contracts.Validations.Unit;

namespace PosSystem.Application.Contracts.Unit
{
    public class UnitOperationsContract
    {
        public string Id { get; set; }

        [UniqueUnitNameOnUpdate]
        public string Name { get; set; }
        public string? Notes { get; set; }
    }
}
