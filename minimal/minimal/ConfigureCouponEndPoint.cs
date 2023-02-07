using Microsoft.AspNetCore.Mvc;
using minimal.Data;
using minimal.Models;

namespace minimal
{
    public static class ConfigureCouponEndPoint
    {
        public static void ConfigureCouponEndPointMethod(this WebApplication app)
        {
            app.MapGet("api/Coupon", (ApplicationDbContext _db) => Results.Ok(_db.Coupons)).RequireAuthorization();
            app.MapGet("api/Coupon/{id:int}", (int id) => Results.Ok(CouponStore.CouponList.FirstOrDefault(x => x.Id == id)));

            app.MapPost("api/coupon", (ApplicationDbContext _db, [FromBody] Coupon coupon) => {
                CouponStore.CouponList.Add(coupon);
                Results.Ok(CouponStore.CouponList);
            }
            );
        }
    }
}
