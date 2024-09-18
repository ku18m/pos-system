using System.ComponentModel.DataAnnotations;

namespace PosSystem.Application.Contracts.Validations.Product
{
    public class SellingNotLessThanBuyingAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var buyingPriceProperty = validationContext.ObjectType.GetProperty("BuyingPrice");
            if (buyingPriceProperty == null)
            {
                return new ValidationResult("BuyingPrice property not found.");
            }

            var buyingPriceValue = buyingPriceProperty.GetValue(validationContext.ObjectInstance, null);
            if (buyingPriceValue == null)
            {
                return new ValidationResult("BuyingPrice value is null.");
            }

            var buyingPrice = (decimal)buyingPriceValue;
            var sellingPrice = (decimal)value!;

            if (sellingPrice < buyingPrice)
            {
                return new ValidationResult("Selling price cannot be less than buying price.");
            }

            return ValidationResult.Success;
        }
    }
}
