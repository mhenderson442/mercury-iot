using System;
using Mercuryiot.Functions;
using Mercuryiot.Functions.Models;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Mercuryiot.Test.Functions.FunctionTests
{
    [Trait("Function App Tests", "Client Function Tests")]
    public partial class ClientFunctionTests
    {
        //private readonly Client _badClient;
        //private readonly string _badCustomerKey;
        //private readonly string _customerKey;
        //private readonly string _errorInput;
        //private readonly Client _goodClient;
        private readonly NullLogger<ClientFunctions> _nullLogger;

        public ClientFunctionTests()
        {
            //_errorInput = "Error Input";
            //_customerKey = Guid.NewGuid().ToString();

            //_badCustomerKey = "Bad Customer Key";

            //_badClient = TestFactory.CreateMockClient();
            //_badClient.id = _badCustomerKey;

            //_goodClient = TestFactory.CreateMockClient();
            //_goodClient.id = "Good Customer Key";

            _nullLogger = new NullLogger<ClientFunctions>();
        }
    }
}