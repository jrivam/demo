using library.Impl;
using library.Interface.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace test
{
    [TestClass]
    public class Empresa
    {
        [TestMethod]
        public void Load_Success()
        {
            var mockMapper = new Moq.Mock<IMapperTable<domain.Model.Empresa, data.Model.Empresa>>();
            var mockRepository = new Moq.Mock<IRepository<domain.Model.Empresa, data.Model.Empresa>>(mockMapper.Object);

            mockRepository.Setup(x => x.Select(It.IsAny<data.Model.Empresa>()))
            .Returns((IMapperTable<domain.Model.Empresa, data.Model.Empresa> mapper, data.Model.Empresa param) => 
            {
                return (new Result() { Success = true }, param);
            });

            var load = new data.Model.Empresa(mockRepository.Object, "", "") { Id = 1 }.Select();

            Assert.IsTrue(load.result.Success);
        }
    }
}
