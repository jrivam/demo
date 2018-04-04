using library.Impl.Data.Repository;
using library.Impl.Data.Sql;
using library.Interface.Data;
using library.Interface.Data.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data;

namespace test
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

        public Mock<IDbCommand> GetCommand(Mock<ISqlBuilder<entities.Model.Empresa>> mockBuilder)
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

            mockBuilder.SetupGet<string>(x => x.SyntaxSign.AliasSeparatorColumn)
                .Returns(".");

            return mockCommand;
        }

        private (data.Model.Empresa data, Mock<ISqlBuilder<entities.Model.Empresa>> mockBuilder, Mock<IDbCommand> mockCommand) Data_Select()
        {
            var mockBuilder = new Moq.Mock<ISqlBuilder<entities.Model.Empresa>>();
            var mockCommand = GetCommand(mockBuilder);

            bool read = true;
            var mockDataReader = new Moq.Mock<IDataReader>();
            mockDataReader.Setup(x => x.Read()).Returns(() => read).Callback(() => read = false);
            mockDataReader.Setup(x => x["Id"]).Returns(Entity.Id);
            mockDataReader.Setup(x => x["RazonSocial"]).Returns(Entity.RazonSocial);
            mockDataReader.Setup(x => x["Activo"]).Returns(Entity.Activo);

            mockCommand.Setup(x => x.ExecuteReader())
                .Returns(mockDataReader.Object);

            return (new data.Model.Empresa(new Repository<entities.Model.Empresa, data.Model.Empresa>(new data.Mapper.Empresa(), mockBuilder.Object), new entities.Model.Empresa(), "", "")
            {
                Id = Entity.Id
            }, mockBuilder, mockCommand);
        }
        public (data.Model.Empresa data, Mock<ISqlBuilder<entities.Model.Empresa>> mockBuilder, Mock<IDbCommand> mockCommand) Data_Select_Query()
        {
            var dataselect = Data_Select();

            dataselect.mockBuilder.Setup(x => x.Select(It.IsAny<IEntityTable<entities.Model.Empresa>>()))
                .Returns(dataselect.mockCommand.Object);

            return dataselect;
        }
        public (data.Model.Empresa data, Mock<ISqlBuilder<entities.Model.Empresa>> mockBuilder, Mock<IDbCommand> mockCommand) Data_Select_DbCommand()
        {
            var dataselect = Data_Select();

            dataselect.data.SelectDbCommand = (true, ("", CommandType.StoredProcedure, new List<DbParameter>()));
            dataselect.mockBuilder.Setup(x => x.GetCommand(It.IsAny<string>(), It.IsAny<CommandType>(), It.IsAny<IList<DbParameter>>()))
                .Returns(dataselect.mockCommand.Object);

            return dataselect;
        }
        [TestMethod]
        public void Data_Select_Query_Success()
        {
            var dataselect = Data_Select_Query();

            var select = dataselect.data.Select();

            Assert.IsTrue(select.result.Success);
            Assert.AreEqual(Entity.Id, select.data.Id);
            Assert.AreEqual(Entity.RazonSocial, select.data.RazonSocial);
            Assert.AreEqual(Entity.Activo, select.data.Activo);
        }
        [TestMethod]
        public void Data_Select_DbCommand_Success()
        {
            var dataselect = Data_Select_DbCommand();

            var select = dataselect.data.Select();

            Assert.IsTrue(select.result.Success);
            Assert.AreEqual(Entity.Id, select.data.Id);
            Assert.AreEqual(Entity.RazonSocial, select.data.RazonSocial);
            Assert.AreEqual(Entity.Activo, select.data.Activo);
        }

        private (data.Model.Empresa data, Mock<ISqlBuilder<entities.Model.Empresa>> mockBuilder, Mock<IDbCommand> mockCommand)Data_Insert()
        {
            var mockBuilder = new Moq.Mock<ISqlBuilder<entities.Model.Empresa>>();
            var mockCommand = GetCommand(mockBuilder);

            mockCommand.Setup(x => x.ExecuteScalar())
                .Returns(Entity.Id);

            return (new data.Model.Empresa(new Repository<entities.Model.Empresa, data.Model.Empresa>(new data.Mapper.Empresa(), mockBuilder.Object), new entities.Model.Empresa(), "", "")
            {
                RazonSocial = Entity.RazonSocial,
                Activo = Entity.Activo
            }, mockBuilder, mockCommand);
        }
        public (data.Model.Empresa data, Mock<ISqlBuilder<entities.Model.Empresa>> mockBuilder, Mock<IDbCommand> mockCommand) Data_Insert_Query()
        {
            var datainsert = Data_Insert();

            datainsert.mockBuilder.Setup(x => x.Insert(It.IsAny<IEntityTable<entities.Model.Empresa>>()))
                .Returns(datainsert.mockCommand.Object);

            return datainsert;
        }
        public (data.Model.Empresa data, Mock<ISqlBuilder<entities.Model.Empresa>> mockBuilder, Mock<IDbCommand> mockCommand) Data_Insert_DbCommand()
        {
            var datainsert = Data_Insert();

            datainsert.data.InsertDbCommand = (true, ("", CommandType.StoredProcedure, new List<DbParameter>()));
            datainsert.mockBuilder.Setup(x => x.GetCommand(It.IsAny<string>(), It.IsAny<CommandType>(), It.IsAny<IList<DbParameter>>()))
                .Returns(datainsert.mockCommand.Object);

            return datainsert;
        }
        [TestMethod]
        public void Data_Insert_Query_Success()
        {
            var datainsert = Data_Insert_Query();

            var insert = datainsert.data.Insert();

            Assert.IsTrue(insert.result.Success);
            Assert.AreEqual(Entity.Id, insert.data.Id);
            Assert.AreEqual(Entity.RazonSocial, insert.data.RazonSocial);
            Assert.AreEqual(Entity.Activo, insert.data.Activo);
        }
        [TestMethod]
        public void Data_Insert_DbCommand_Success()
        {
            var datainsert = Data_Insert_DbCommand();

            var insert = datainsert.data.Insert();       

            Assert.IsTrue(insert.result.Success);
            Assert.AreEqual(Entity.Id, insert.data.Id);
            Assert.AreEqual(Entity.RazonSocial, insert.data.RazonSocial);
            Assert.AreEqual(Entity.Activo, insert.data.Activo);
        }

        private (data.Model.Empresa data, Mock<ISqlBuilder<entities.Model.Empresa>> mockBuilder, Mock<IDbCommand> mockCommand)Data_Update()
        {
            var mockBuilder = new Moq.Mock<ISqlBuilder<entities.Model.Empresa>>();
            var mockCommand = GetCommand(mockBuilder);

            mockCommand.Setup(x => x.ExecuteNonQuery())
                .Returns(1);

            return (new data.Model.Empresa(new Repository<entities.Model.Empresa, data.Model.Empresa>(new data.Mapper.Empresa(), mockBuilder.Object), new entities.Model.Empresa(), "", "")
            {
                Id = Entity.Id,
                RazonSocial = Entity.RazonSocial,
                Activo = Entity.Activo
            }, mockBuilder, mockCommand);
        }
        public (data.Model.Empresa data, Mock<ISqlBuilder<entities.Model.Empresa>> mockBuilder, Mock<IDbCommand> mockCommand) Data_Update_Query()
        {
            var dataupdate = Data_Update();

            dataupdate.mockBuilder.Setup(x => x.Update(It.IsAny<IEntityTable<entities.Model.Empresa>>()))
                .Returns(dataupdate.mockCommand.Object);

            return dataupdate;
        }
        public (data.Model.Empresa data, Mock<ISqlBuilder<entities.Model.Empresa>> mockBuilder, Mock<IDbCommand> mockCommand) Data_Update_DbCommand()
        {
            var dataupdate = Data_Update();

            dataupdate.data.UpdateDbCommand = (true, ("", CommandType.StoredProcedure, new List<DbParameter>()));
            dataupdate.mockBuilder.Setup(x => x.GetCommand(It.IsAny<string>(), It.IsAny<CommandType>(), It.IsAny<IList<DbParameter>>()))
                .Returns(dataupdate.mockCommand.Object);

            return dataupdate;
        }
        [TestMethod]
        public void Data_Update_Query_Success()
        {
            var dataupdate = Data_Update_Query();

            var update = dataupdate.data.Update();

            Assert.IsTrue(update.result.Success);
            Assert.AreEqual(Entity.Id, update.data.Id);
            Assert.AreEqual(Entity.RazonSocial, update.data.RazonSocial);
            Assert.AreEqual(Entity.Activo, update.data.Activo);
        }
        [TestMethod]
        public void Data_Update_DbCommand_Success()
        {
            var dataupdate = Data_Update_DbCommand();

            var update = dataupdate.data.Update();

            Assert.IsTrue(update.result.Success);
            Assert.AreEqual(Entity.Id, update.data.Id);
            Assert.AreEqual(Entity.RazonSocial, update.data.RazonSocial);
            Assert.AreEqual(Entity.Activo, update.data.Activo);
        }

        private (data.Model.Empresa data, Mock<ISqlBuilder<entities.Model.Empresa>> mockBuilder, Mock<IDbCommand> mockCommand)Data_Delete()
        {
            var mockBuilder = new Moq.Mock<ISqlBuilder<entities.Model.Empresa>>();
            var mockCommand = GetCommand(mockBuilder);

            mockCommand.Setup(x => x.ExecuteNonQuery())
                .Returns(1);

            return (new data.Model.Empresa(new Repository<entities.Model.Empresa, data.Model.Empresa>(new data.Mapper.Empresa(), mockBuilder.Object), new entities.Model.Empresa(), "", "")
            {
                Id = Entity.Id,
                RazonSocial = Entity.RazonSocial,
                Activo = Entity.Activo
            }, mockBuilder, mockCommand);
        }
        public (data.Model.Empresa data, Mock<ISqlBuilder<entities.Model.Empresa>> mockBuilder, Mock<IDbCommand> mockCommand) Data_Delete_Query()
        {
            var datadelete = Data_Delete();

            datadelete.mockBuilder.Setup(x => x.Delete(It.IsAny<IEntityTable<entities.Model.Empresa>>()))
                .Returns(datadelete.mockCommand.Object);

            return datadelete;
        }
        public (data.Model.Empresa data, Mock<ISqlBuilder<entities.Model.Empresa>> mockBuilder, Mock<IDbCommand> mockCommand) Data_Delete_DbCommand()
        {
            var datadelete = Data_Delete();

            datadelete.data.DeleteDbCommand = (true, ("", CommandType.StoredProcedure, new List<DbParameter>()));
            datadelete.mockBuilder.Setup(x => x.GetCommand(It.IsAny<string>(), It.IsAny<CommandType>(), It.IsAny<IList<DbParameter>>()))
                .Returns(datadelete.mockCommand.Object);

            return datadelete;
        }
        [TestMethod]
        public void Data_Delete_Query_Success()
        {
            var datadelete = Data_Delete_Query();

            var delete = datadelete.data.Delete();

            Assert.IsTrue(delete.result.Success);
        }
        [TestMethod]
        public void Data_Delete_DbCommand_Success()
        {
            var datadelete = Data_Delete_DbCommand();

            var delete = datadelete.data.Delete();

            Assert.IsTrue(delete.result.Success);
        }
    }
}
