using System;
using Advertisements.Data;
using Advertisements.Infrastructures.Services.Contracts;
using Advertisements.Web.App_Start;
using Advertisements.Web.Infrastructure.DataLoader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Advertisements.Web.Tests.Controllers
{
    public abstract class BaseControllerTests
    {
        protected readonly Mock<IAdvertisementsService> advertisementsServiceMock;
        protected readonly Mock<IAdsData> dataMock;
        protected readonly Mock<IDataLoader> dataLoaderMock;

        protected BaseControllerTests()
        {
            this.advertisementsServiceMock = new Mock<IAdvertisementsService>(MockBehavior.Strict);
            this.dataMock = new Mock<IAdsData>(MockBehavior.Strict);
            this.dataLoaderMock = new Mock<IDataLoader>(MockBehavior.Strict);

            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute();
        }
    }
}
