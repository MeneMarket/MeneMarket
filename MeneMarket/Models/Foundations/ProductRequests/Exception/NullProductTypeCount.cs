using Xeptions;

namespace MeneMarket.Models.Foundations.ProductRequests.Exception
{
    public class NullProductTypeCount : Xeption
    {
        public NullProductTypeCount()
            : base(message: "Product Type Count is 0")
        { }
    }
}