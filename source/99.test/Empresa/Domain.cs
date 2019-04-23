using Library.Impl;
using Library.Interface.Persistence.Sql.Builder;
using Library.Interface.Persistence.Sql.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test.Empresa
{
    [TestClass]
    public class Domain
    {
        public static test.Empresa.Data Data;

        public Domain(test.Empresa.Data data)
        {
            Data = data;
        }
        public Domain()
            : this(new test.Empresa.Data())
        {
        }

        [TestInitialize]
        public void BeforeEachTest()
        {

        }

        public Business.Table.Empresa Domain_Load_NonDbCommand()
        {
            var data = Data.Data_Table_Select_NonDbCommand();

            return new Business.Table.Empresa(data)
            {
            };
        }
        [TestMethod]
        public void Domain_Load_NonDbCommand_Success()
        {
            var load = Domain_Load_NonDbCommand().Load();

            Assert.IsTrue(load.result.Success);
            Assert.AreEqual(Data.Entity.Id, load.domain.Id);
            Assert.AreEqual(Data.Entity.RazonSocial, load.domain.RazonSocial);
            Assert.AreEqual(Data.Entity.Activo, load.domain.Activo);
        }

        public Moq.Mock<Business.Table.Empresa> Domain_Erase_NonDbCommand()
        {
            var data = Data.Data_Table_Delete_NonDbCommand();

            return new Moq.Mock<Business.Table.Empresa>(data)
            {
                CallBase = true
            };
        }
        [TestMethod]
        public void Domain_Erase_NonDbCommand_Success()
        {
            var mockDatabase = Data.GetDatabaseMock();
            mockDatabase.mockCommand.Setup(x => x.ExecuteNonQuery())
                .Returns(1);

            var mockBuilderQuery = new Moq.Mock<ISqlBuilderQuery>();
            var mockCommandBuilderQuery = new Moq.Mock<ISqlCommandBuilder>();

            //var data = new data.Query.Empresa
            //    (
            //        new RepositoryQuery<data.Query.Empresa, Entities.Table.Empresa, data.Model.Empresa>
            //        (mockDatabaseQuery.mockCreator.Object,
            //        new Entities.Reader.Empresa(),
            //        new data.Mapper.Empresa(),
            //        mockDatabaseQuery.mockSyntaxSign.Object,
            //        mockCommandBuilderQuery.Object, mockBuilderQuery.Object)
            //    );

            //var query = new Moq.Mock<domain.Query.Empresa>(data)
            //{ CallBase = true };
            //query.Setup(x => x.Data.Sucursal(It.IsAny<data.Query.Sucursal>())).Returns(new data.Query.Sucursal
            //    (
            //        new RepositoryQuery<data.Query.Sucursal, Entities.Table.Sucursal, data.Model.Sucursal>
            //        (mockDatabaseQuery.mockCreator.Object,
            //        new Entities.Reader.Sucursal(),
            //        new data.Mapper.Sucursal(),
            //        mockDatabaseQuery.mockSyntaxSign.Object,
            //        mockCommandBuilderQuery.Object, mockBuilderQuery.Object))
            //    );

            var mockDomain = Domain_Erase_NonDbCommand();
            mockDomain.Setup(x => x.Sucursales.EraseAll()).Returns(new Result() { Success = true });

            //erasecommand.Protected().Setup<Result>("EraseChildren2", ItExpr.IsAny<domain.Query.Sucursal>()).Returns(new Result() { Success = true });
            //erasecommand.SetupGet(x => x.Query).Returns(query.Object);

            var erase = mockDomain.Object.Erase();

            Assert.IsTrue(erase.result.Success);
            Assert.IsTrue(erase.domain.Deleted);
        }

        public Business.Table.Empresa Domain_Save_NonDbCommand_Insert()
        {
            var data = Data.Data_Table_Insert_NonDbCommand();

            return new Business.Table.Empresa(data)
            {
            };
        }
        [TestMethod]
        public void Domain_Save_Insert_NonDbCommand_Success()
        {
            var domain = Domain_Save_NonDbCommand_Insert();

            domain.Ruc = "RUC";
            domain.RazonSocial += "1";
            domain.Activo = false;

            var save = domain.Save();

            Assert.IsTrue(save.result.Success);
            Assert.IsTrue(!save.domain.Changed);
            Assert.AreEqual(Data.Entity.Id, save.domain.Id);
            Assert.AreEqual(Data.Entity.RazonSocial + "1", save.domain.RazonSocial);
            Assert.AreEqual(!Data.Entity.Activo, false);
        }

        public Business.Table.Empresa Domain_Save_NonDbCommand_Update()
        {
            var data = Data.Data_Table_Update_NonDbCommand();

            return new Business.Table.Empresa(data)
            {
            };
        }
        [TestMethod]
        public void Domain_Save_Update_NonDbCommand_Success()
        {
            var domain = Domain_Save_NonDbCommand_Insert();

            domain.RazonSocial += "1";
            domain.Activo = false;

            var save = domain.Save();

            Assert.IsTrue(save.result.Success);
            Assert.IsTrue(!save.domain.Changed);
            Assert.AreEqual(Data.Entity.Id, save.domain.Id);
            Assert.AreEqual($"{Data.Entity.RazonSocial}1", save.domain.RazonSocial);
            Assert.AreEqual(!Data.Entity.Activo, false);
        }
    }
}
