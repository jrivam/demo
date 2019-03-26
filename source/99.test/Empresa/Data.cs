using Library.Impl.Data.Sql;
using Library.Impl.Data.Table;
using Library.Interface.Data;
using Library.Interface.Data.Query;
using Library.Interface.Data.Sql;
using Library.Interface.Data.Sql.Builder;
using Library.Interface.Data.Sql.Database;
using Library.Interface.Data.Sql.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data;

namespace test.Empresa
{
    [TestClass]
    public class Data
    {
        public static Entities.Table.Empresa Entity;

        public Data(Entities.Table.Empresa empresa)
        {
            Entity = empresa;
        }
        public Data()
            : this(new Entities.Table.Empresa()
            {
                Id = 1,
                RazonSocial = "razon_social",
                Activo = true
            })
        {
        }

        [TestInitialize]
        public void BeforeEachTest()
        {

        }


        private void AssertEqualFields(Entities.Table.Empresa entity, data.Model.Empresa data, bool assertid = false)
        {
            if(assertid)
            {
                Assert.AreEqual(Entity.Id, data.Id);
            }
            Assert.AreEqual(entity.Ruc, data.Ruc);
            Assert.AreEqual(entity.RazonSocial, data.RazonSocial);
            Assert.AreEqual(entity.Activo, data.Activo);
        }
        public (Mock<IDbCommand> mockCommand, Mock<ISqlSyntaxSign> mockSyntaxSign, Mock<ISqlCreator> mockCreator) 
            GetDatabaseMock()
        {
            //var mockMapper = new Moq.Mock<IMapperTable<domain.Model.Empresa, data.Model.Empresa>>();

            //mockMapper.Setup(x => x.Read(It.IsAny<data.Model.Empresa>(), It.IsAny<IDataReader>(), It.IsAny<IList<string>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            //    .Returns((data.Model.Empresa data, IDataReader reader, IList<string> prefixname, string aliasseparator, int maxdepth, int depth) =>
            //    {
            //        data["Id"].Value = data["Id"].DbValue = data.Id;
            //        data["RazonSocial"].Value = data["RazonSocial"].DbValue = "razon social";
            //        data["Activo"].Value = data["Activo"].DbValue = true;

            //        return data;
            //    });

            var mockConnection = new Moq.Mock<IDbConnection>();

            var mockCommand = new Moq.Mock<IDbCommand>();
            mockCommand.SetupGet<IDbConnection>(x => x.Connection)
                .Returns(mockConnection.Object);

            var mockSyntaxSign = new Moq.Mock<ISqlSyntaxSign>();
            mockSyntaxSign.SetupGet<string>(x => x.AliasSeparatorColumn)
                .Returns(".");

            var mockObjectCreator = new Moq.Mock<IDbObjectCreator>();

            var mockCreator = new Moq.Mock<ISqlCreator>();
            mockCreator.Setup(x => x.GetCommand(It.IsAny<string>(), It.IsAny<CommandType>(), It.IsAny<IList<SqlParameter>>()))
                .Returns(mockCommand.Object);

            return (mockCommand, mockSyntaxSign, mockCreator);
        }

        public data.Model.Empresa Data_Table_Select()
        {
            var mockDatabase = GetDatabaseMock();

            bool read = true;
            var mockDataReader = new Moq.Mock<IDataReader>();
            mockDataReader.Setup(x => x.Read()).Returns(() => read).Callback(() => read = false);
            mockDataReader.Setup(x => x["Empresa.Id"]).Returns(Entity.Id);
            mockDataReader.Setup(x => x["Empresa.RazonSocial"]).Returns(Entity.RazonSocial);
            mockDataReader.Setup(x => x["Empresa.Activo"]).Returns(Entity.Activo);

            mockDatabase.mockCommand.Setup(x => x.ExecuteReader())
                .Returns(mockDataReader.Object);

            var mockCommandBuilder = new Moq.Mock<ISqlCommandBuilder>();

            return new data.Model.Empresa(new Entities.Table.Empresa()
            {
                Id = Entity.Id
            },
            new RepositoryTable<Entities.Table.Empresa, data.Model.Empresa>(
                new Entities.Reader.Empresa(),
                new data.Mapper.Empresa(),
                mockDatabase.mockCreator.Object,
                mockDatabase.mockSyntaxSign.Object,
                mockCommandBuilder.Object));
        }
        public data.Model.Empresa Data_Table_Select_NonDbCommand()
        {
            var data = Data_Table_Select();

            return data;
        }
        public data.Model.Empresa Data_Table_Select_DbCommand()
        {
            var data = Data_Table_Select();

            data.SelectDbCommand = (true, ("", CommandType.StoredProcedure, new List<SqlParameter>()));

            return data;
        }
        [TestMethod]
        public void Data_Table_Select_NonDbCommand_Success()
        {
            var data = Data_Table_Select_NonDbCommand();

            var select = data.Select();

            Assert.IsTrue(select.result.Success);
            AssertEqualFields(Entity, select.data, true);
        }
        [TestMethod]
        public void Data_Table_Select_DbCommand_Success()
        {
            var data = Data_Table_Select_DbCommand();

            var select = data.Select();

            Assert.IsTrue(select.result.Success);
            AssertEqualFields(Entity, select.data, true);
        }

        public data.Model.Empresa Data_Table_Insert()
        {
            var mockDatabase = GetDatabaseMock();

            mockDatabase.mockCommand.Setup(x => x.ExecuteScalar())
                .Returns(Entity.Id);

            var mockBuilderTable = new Moq.Mock<ISqlBuilderTable>();
            var mockCommandBuilder = new Moq.Mock<ISqlCommandBuilder>();

            var data = new data.Model.Empresa(new Entities.Table.Empresa()
            {
                RazonSocial = Entity.RazonSocial,
                Ruc = Entity.Ruc,                
                Activo = Entity.Activo
            },
            new RepositoryTable<Entities.Table.Empresa, data.Model.Empresa>(
                new Entities.Reader.Empresa(),
                new data.Mapper.Empresa(),
                mockDatabase.mockCreator.Object,
                mockDatabase.mockSyntaxSign.Object,
                mockCommandBuilder.Object));

            var mockQueryRepositoryMethods = new Moq.Mock<IQueryDataMethods<Entities.Table.Empresa, data.Model.Empresa>>();
            mockQueryRepositoryMethods.Setup(x => x.SelectSingle(1)).Returns(null);

            return data;
        }
        public data.Model.Empresa Data_Table_Insert_NonDbCommand()
        {
            var data = Data_Table_Insert();

            return data;
        }
        public data.Model.Empresa Data_Table_Insert_DbCommand()
        {
            var data = Data_Table_Insert();

            data.InsertDbCommand = (true, ("", CommandType.StoredProcedure, new List<SqlParameter>()));

            return data;
        }
        [TestMethod]
        public void Data_Table_Insert_NonDbCommand_Success()
        {
            var data = Data_Table_Insert_NonDbCommand();

            var insert = data.Insert();

            Assert.IsTrue(insert.result.Success);
            AssertEqualFields(Entity, insert.data, true);
        }
        [TestMethod]
        public void Data_Table_Insert_DbCommand_Success()
        {
            var data = Data_Table_Insert_DbCommand();

            var insert = data.Insert();       

            Assert.IsTrue(insert.result.Success);
            AssertEqualFields(Entity, insert.data, true);
        }

        public data.Model.Empresa Data_Table_Update()
        {
            var mockDatabase = GetDatabaseMock();

            mockDatabase.mockCommand.Setup(x => x.ExecuteNonQuery())
                .Returns(1);

            var mockCommandBuilder = new Moq.Mock<ISqlCommandBuilder>();

            return new data.Model.Empresa(new Entities.Table.Empresa()
            {
                Id = Entity.Id,
                Ruc = Entity.Ruc,
                RazonSocial = Entity.RazonSocial,
                Activo = Entity.Activo
            },
            new RepositoryTable<Entities.Table.Empresa, data.Model.Empresa>(
                new Entities.Reader.Empresa(),
                new data.Mapper.Empresa(),
                mockDatabase.mockCreator.Object, 
                mockDatabase.mockSyntaxSign.Object,
                mockCommandBuilder.Object));
        }
        public data.Model.Empresa Data_Table_Update_NonDbCommand()
        {
            var data = Data_Table_Update();

            return data;
        }
        public data.Model.Empresa Data_Table_Update_DbCommand()
        {
            var data = Data_Table_Update();

            data.UpdateDbCommand = (true, ("", CommandType.StoredProcedure, new List<SqlParameter>()));

            return data;
        }
        [TestMethod]
        public void Data_Table_Update_NonDbCommand_Success()
        {
            var data = Data_Table_Update_NonDbCommand();

            var update = data.Update();

            Assert.IsTrue(update.result.Success);
            AssertEqualFields(Entity, update.data, true);
        }
        [TestMethod]
        public void Data_Table_Update_DbCommand_Success()
        {
            var data = Data_Table_Update_DbCommand();

            var update = data.Update();

            Assert.IsTrue(update.result.Success);
            AssertEqualFields(Entity, update.data, true);
        }

        public data.Model.Empresa Data_Table_Delete()
        {
            var mockDatabase = GetDatabaseMock();

            mockDatabase.mockCommand.Setup(x => x.ExecuteNonQuery())
                .Returns(1);

            var mockCommandBuilder = new Moq.Mock<ISqlCommandBuilder>();

            return new data.Model.Empresa(new Entities.Table.Empresa()
            {
                Id = Entity.Id,
                Ruc = Entity.Ruc,
                RazonSocial = Entity.RazonSocial,
                Activo = Entity.Activo
            },
            new RepositoryTable<Entities.Table.Empresa, data.Model.Empresa>(
                new Entities.Reader.Empresa(),
                new data.Mapper.Empresa(),
                mockDatabase.mockCreator.Object, 
                mockDatabase.mockSyntaxSign.Object,
                mockCommandBuilder.Object));
        }
        public data.Model.Empresa Data_Table_Delete_NonDbCommand()
        {
            var data = Data_Table_Delete();

            return data;
        }
        public data.Model.Empresa Data_Table_Delete_DbCommand()
        {
            var data = Data_Table_Delete();

            data.DeleteDbCommand = (true, ("", CommandType.StoredProcedure, new List<SqlParameter>()));

            return data;
        }
        [TestMethod]
        public void Data_Table_Delete_NonDbCommand_Success()
        {
            var data = Data_Table_Delete_NonDbCommand();

            var delete = data.Delete();

            Assert.IsTrue(delete.result.Success);
        }
        [TestMethod]
        public void Data_Table_Delete_DbCommand_Success()
        {
            var data = Data_Table_Delete_DbCommand();

            var delete = data.Delete();

            Assert.IsTrue(delete.result.Success);
        }
    }
}
