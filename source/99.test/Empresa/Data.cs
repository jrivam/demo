using library.Impl.Data.Sql;
using library.Impl.Data.Table;
using library.Interface.Data;
using library.Interface.Data.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data;

namespace test.Empresa
{
    [TestClass]
    public class Data
    {
        public static entities.Model.Empresa Entity;

        public Data(entities.Model.Empresa empresa)
        {
            Entity = empresa;
        }
        public Data()
            : this(new entities.Model.Empresa()
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

            var mockBuilderTable = new Moq.Mock<ISqlBuilderTable>();
            var mockCommandBuilder = new Moq.Mock<ISqlCommandBuilder>();

            return new data.Model.Empresa(new RepositoryTable<entities.Model.Empresa, data.Model.Empresa>(mockDatabase.mockCreator.Object,
                new entities.Reader.Empresa(),
                new data.Mapper.Empresa(),
                mockDatabase.mockSyntaxSign.Object,
                mockCommandBuilder.Object, mockBuilderTable.Object),
                new entities.Model.Empresa()
                {
                    Id = Entity.Id
                });
        }
        public data.Model.Empresa Data_Table_Select_NonDbCommand()
        {
            var dataselect = Data_Table_Select();

            return dataselect;
        }
        public data.Model.Empresa Data_Table_Select_DbCommand()
        {
            var dataselect = Data_Table_Select();

            dataselect.SelectDbCommand = (true, ("", CommandType.StoredProcedure, new List<SqlParameter>()));

            return dataselect;
        }
        [TestMethod]
        public void Data_Table_Select_NonDbCommand_Success()
        {
            var dataselect = Data_Table_Select_NonDbCommand();

            var select = dataselect.Select();

            Assert.IsTrue(select.result.Success);
            Assert.AreEqual(Entity.Id, select.data.Id);
            Assert.AreEqual(Entity.RazonSocial, select.data.RazonSocial);
            Assert.AreEqual(Entity.Activo, select.data.Activo);
        }
        [TestMethod]
        public void Data_Table_Select_DbCommand_Success()
        {
            var dataselect = Data_Table_Select_DbCommand();

            var select = dataselect.Select();

            Assert.IsTrue(select.result.Success);
            Assert.AreEqual(Entity.Id, select.data.Id);
            Assert.AreEqual(Entity.RazonSocial, select.data.RazonSocial);
            Assert.AreEqual(Entity.Activo, select.data.Activo);
        }

        public data.Model.Empresa Data_Table_Insert()
        {
            var mockDatabase = GetDatabaseMock();

            mockDatabase.mockCommand.Setup(x => x.ExecuteScalar())
                .Returns(Entity.Id);

            var mockBuilderTable = new Moq.Mock<ISqlBuilderTable>();
            var mockCommandBuilder = new Moq.Mock<ISqlCommandBuilder>();

            return new data.Model.Empresa(new RepositoryTable<entities.Model.Empresa, data.Model.Empresa>(mockDatabase.mockCreator.Object,
                new entities.Reader.Empresa(),
                new data.Mapper.Empresa(),
                mockDatabase.mockSyntaxSign.Object,
                mockCommandBuilder.Object, mockBuilderTable.Object),
                new entities.Model.Empresa()
                {
                    RazonSocial = Entity.RazonSocial,
                    Activo = Entity.Activo
                });
        }
        public data.Model.Empresa Data_Table_Insert_NonDbCommand()
        {
            var datainsert = Data_Table_Insert();

            return datainsert;
        }
        public data.Model.Empresa Data_Table_Insert_DbCommand()
        {
            var datainsert = Data_Table_Insert();

            datainsert.InsertDbCommand = (true, ("", CommandType.StoredProcedure, new List<SqlParameter>()));

            return datainsert;
        }
        [TestMethod]
        public void Data_Table_Insert_NonDbCommand_Success()
        {
            var datainsert = Data_Table_Insert_NonDbCommand();

            var insert = datainsert.Insert();

            Assert.IsTrue(insert.result.Success);
            Assert.AreEqual(Entity.Id, insert.data.Id);
            Assert.AreEqual(Entity.RazonSocial, insert.data.RazonSocial);
            Assert.AreEqual(Entity.Activo, insert.data.Activo);
        }
        [TestMethod]
        public void Data_Table_Insert_DbCommand_Success()
        {
            var datainsert = Data_Table_Insert_DbCommand();

            var insert = datainsert.Insert();       

            Assert.IsTrue(insert.result.Success);
            Assert.AreEqual(Entity.Id, insert.data.Id);
            Assert.AreEqual(Entity.RazonSocial, insert.data.RazonSocial);
            Assert.AreEqual(Entity.Activo, insert.data.Activo);
        }

        public data.Model.Empresa Data_Table_Update()
        {
            var mockDatabase = GetDatabaseMock();

            mockDatabase.mockCommand.Setup(x => x.ExecuteNonQuery())
                .Returns(1);

            var mockBuilderTable = new Moq.Mock<ISqlBuilderTable>();
            var mockCommandBuilder = new Moq.Mock<ISqlCommandBuilder>();

            return new data.Model.Empresa(new RepositoryTable<entities.Model.Empresa, data.Model.Empresa>(mockDatabase.mockCreator.Object,
                new entities.Reader.Empresa(),
                new data.Mapper.Empresa(),
                mockDatabase.mockSyntaxSign.Object,
                mockCommandBuilder.Object, mockBuilderTable.Object),
                new entities.Model.Empresa()
                {
                    Id = Entity.Id,
                    RazonSocial = Entity.RazonSocial,
                    Activo = Entity.Activo
                });
        }
        public data.Model.Empresa Data_Table_Update_NonDbCommand()
        {
            var dataupdate = Data_Table_Update();

            return dataupdate;
        }
        public data.Model.Empresa Data_Table_Update_DbCommand()
        {
            var dataupdate = Data_Table_Update();

            dataupdate.UpdateDbCommand = (true, ("", CommandType.StoredProcedure, new List<SqlParameter>()));

            return dataupdate;
        }
        [TestMethod]
        public void Data_Table_Update_NonDbCommand_Success()
        {
            var dataupdate = Data_Table_Update_NonDbCommand();

            var update = dataupdate.Update();

            Assert.IsTrue(update.result.Success);
            Assert.AreEqual(Entity.Id, update.data.Id);
            Assert.AreEqual(Entity.RazonSocial, update.data.RazonSocial);
            Assert.AreEqual(Entity.Activo, update.data.Activo);
        }
        [TestMethod]
        public void Data_Table_Update_DbCommand_Success()
        {
            var dataupdate = Data_Table_Update_DbCommand();

            var update = dataupdate.Update();

            Assert.IsTrue(update.result.Success);
            Assert.AreEqual(Entity.Id, update.data.Id);
            Assert.AreEqual(Entity.RazonSocial, update.data.RazonSocial);
            Assert.AreEqual(Entity.Activo, update.data.Activo);
        }

        public data.Model.Empresa Data_Table_Delete()
        {
            var mockDatabase = GetDatabaseMock();

            mockDatabase.mockCommand.Setup(x => x.ExecuteNonQuery())
                .Returns(1);

            var mockBuilderTable = new Moq.Mock<ISqlBuilderTable>();
            var mockCommandBuilder = new Moq.Mock<ISqlCommandBuilder>();

            return new data.Model.Empresa(new RepositoryTable<entities.Model.Empresa, data.Model.Empresa>(mockDatabase.mockCreator.Object,
                new entities.Reader.Empresa(),
                new data.Mapper.Empresa(),
                mockDatabase.mockSyntaxSign.Object,
                mockCommandBuilder.Object, mockBuilderTable.Object),
                new entities.Model.Empresa()
                {
                    Id = Entity.Id,
                    RazonSocial = Entity.RazonSocial,
                    Activo = Entity.Activo
                });
        }
        public data.Model.Empresa Data_Table_Delete_NonDbCommand()
        {
            var datatabledelete = Data_Table_Delete();

            return datatabledelete;
        }
        public data.Model.Empresa Data_Table_Delete_DbCommand()
        {
            var datadelete = Data_Table_Delete();

            datadelete.DeleteDbCommand = (true, ("", CommandType.StoredProcedure, new List<SqlParameter>()));

            return datadelete;
        }
        [TestMethod]
        public void Data_Table_Delete_NonDbCommand_Success()
        {
            var datadelete = Data_Table_Delete_NonDbCommand();

            var delete = datadelete.Delete();

            Assert.IsTrue(delete.result.Success);
        }
        [TestMethod]
        public void Data_Table_Delete_DbCommand_Success()
        {
            var datadelete = Data_Table_Delete_DbCommand();

            var delete = datadelete.Delete();

            Assert.IsTrue(delete.result.Success);
        }
    }
}
