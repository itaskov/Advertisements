using Advertisements.Data;
using Advertisements.Web.App_Start;
using Moq;

namespace Advertisements.Infrastructures.Tests.Services
{
    public abstract class BaseServicesTests
    {
        protected readonly Mock<IAdsData> dataMock;

        protected BaseServicesTests()
        {
            this.dataMock = new Mock<IAdsData>(MockBehavior.Strict);

            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute();
        }
    }
}