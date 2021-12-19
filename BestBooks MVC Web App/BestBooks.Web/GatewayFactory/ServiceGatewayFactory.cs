using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using BestBooks.ServiceGateway;
using BestBooks.ServiceGateway.BookServiceGateway;

namespace BestBooks.Web.GatewayFactory
{
    public class ServiceGatewayFactory : IServiceGatewayFactory
    {
        IBookServiceGateway _BookServiceGateday;
        public ServiceGatewayFactory(IBookServiceGateway BookServiceGateway)
        {
            _BookServiceGateday = BookServiceGateway;
        }

        IBookServiceGateway IServiceGatewayFactory.BookServiceGatewayInstance()
        {
            return _BookServiceGateday;
        }
    }
}
