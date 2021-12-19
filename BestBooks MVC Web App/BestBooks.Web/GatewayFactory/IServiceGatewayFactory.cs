using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BestBooks.ServiceGateway.BookServiceGateway;

namespace BestBooks.Web.GatewayFactory
{
    public interface IServiceGatewayFactory
    {
        IBookServiceGateway BookServiceGatewayInstance();
    }
}
