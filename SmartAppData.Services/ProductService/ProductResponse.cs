using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAppData.RestApi.Models.Response.Product
{
    public class ProductResponse : BaseResponse<DAL.Entities.Product>
    {
        public ProductResponse(DAL.Entities.Product product) : base(product) { }

        public ProductResponse(string message) : base(message) { }
    }
}
