using library.Impl;
using library.Impl.Data.Query;
using library.Interface.Data.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;

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

        public domain.Model.Empresa Domain_Load_NonDbCommand()
        {
            var dataselect = Data.Data_Table_Select_NonDbCommand();

            return new domain.Model.Empresa(dataselect)
            {
            };
        }
        [TestMethod]
        public void Domain_Load_NonDbCommand_Success()
        {
            var domainload = Domain_Load_NonDbCommand().Load();

            Assert.IsTrue(domainload.result.Success);
            Assert.AreEqual(Data.Entity.Id, domainload.domain.Id);
            Assert.AreEqual(Data.Entity.RazonSocial, domainload.domain.RazonSocial);
            Assert.AreEqual(Data.Entity.Activo, domainload.domain.Activo);
        }

        public Moq.Mock<domain.Model.Empresa> Domain_Erase_NonDbCommand()
        {
            var datadelete = Data.Data_Table_Delete_NonDbCommand();

            return new Moq.Mock<domain.Model.Empresa>(datadelete)
            {
                CallBase = true
            };
        }
        [TestMethod]
        public void Domain_Erase_NonDbCommand_Success()
        {
            var mockDatabaseQuery = Data.GetDatabaseMock();
            mockDatabaseQuery.mockCommand.Setup(x => x.ExecuteNonQuery())
                .Returns(1);

            var mockBuilderQuery = new Moq.Mock<ISqlBuilderQuery>();
            var mockCommandBuilderQuery = new Moq.Mock<ISqlCommandBuilder>();

            var query = new Moq.Mock<domain.Query.Empresa>(new data.Query.Empresa
                (
                    new RepositoryQuery<data.Query.Empresa, entities.Model.Empresa, data.Model.Empresa>
                    (mockDatabaseQuery.mockCreator.Object, 
                    new entities.Reader.Empresa(),
                    new data.Mapper.Empresa(),
                    mockDatabaseQuery.mockSyntaxSign.Object,
                    mockCommandBuilderQuery.Object, mockBuilderQuery.Object))
                 )
            { CallBase = true };
            query.Setup(x => x.Data.Sucursal(It.IsAny<data.Query.Sucursal>())).Returns(new data.Query.Sucursal
                (
                    new RepositoryQuery<data.Query.Sucursal, entities.Model.Sucursal, data.Model.Sucursal>
                    (mockDatabaseQuery.mockCreator.Object,
                    new entities.Reader.Sucursal(),
                    new data.Mapper.Sucursal(),
                    mockDatabaseQuery.mockSyntaxSign.Object,
                    mockCommandBuilderQuery.Object, mockBuilderQuery.Object))
                );

            var erasecommand = Domain_Erase_NonDbCommand();
            //erasecommand.Protected().Setup<Result>("EraseChildren2", ItExpr.IsAny<domain.Query.Sucursal>()).Returns(new Result() { Success = true });
            erasecommand.SetupGet(x => x.Query).Returns(query.Object);

            var domainerase = erasecommand.Object.Erase();

            Assert.IsTrue(domainerase.result.Success);
            Assert.IsTrue(domainerase.domain.Deleted);
        }

        public domain.Model.Empresa Domain_Save_NonDbCommand_Insert()
        {
            var datainsert = Data.Data_Table_Insert_NonDbCommand();

            return new domain.Model.Empresa(datainsert)
            {
            };
        }
        [TestMethod]
        public void Domain_Save_Insert_NonDbCommand_Success()
        {
            var domainsave = Domain_Save_NonDbCommand_Insert();

            domainsave.RazonSocial += "1";
            domainsave.Activo = false;

            var save = domainsave.Save();

            Assert.IsTrue(save.result.Success);
            Assert.IsTrue(!save.domain.Changed);
            Assert.AreEqual(Data.Entity.Id, save.domain.Id);
            Assert.AreEqual(Data.Entity.RazonSocial + "1", save.domain.RazonSocial);
            Assert.AreEqual(!Data.Entity.Activo, false);
        }

        public domain.Model.Empresa Domain_Save_NonDbCommand_Update()
        {
            var datainsert = Data.Data_Table_Update_NonDbCommand();

            return new domain.Model.Empresa(datainsert)
            {
            };
        }
        [TestMethod]
        public void Domain_Save_Update_NonDbCommand_Success()
        {
            var domainsave = Domain_Save_NonDbCommand_Insert();

            domainsave.RazonSocial += "1";
            domainsave.Activo = false;

            var save = domainsave.Save();

            Assert.IsTrue(save.result.Success);
            Assert.IsTrue(!save.domain.Changed);
            Assert.AreEqual(Data.Entity.Id, save.domain.Id);
            Assert.AreEqual($"{Data.Entity.RazonSocial}1", save.domain.RazonSocial);
            Assert.AreEqual(!Data.Entity.Activo, false);
        }
    }
}
