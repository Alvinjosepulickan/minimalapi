using minimal.Models;

namespace minimal.Data
{
    public static class CouponStore
    {
        public static List<Coupon> CouponList = new List<Coupon>()
        {
            new Coupon()
            {
                Id=1,
                Name="100OFF",
                Percent=10,
                IsActive=true,
            },
            new Coupon()
            {
                Id=2,
                Name="200OFF",
                Percent=20,
                IsActive=true,
            }
        };
    }
}
