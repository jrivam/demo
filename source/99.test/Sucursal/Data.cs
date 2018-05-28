using library.Impl.Data.Repository;
using library.Impl.Data.Sql;
using library.Interface.Data.Model;
using library.Interface.Data.Query;
using library.Interface.Data.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;

namespace test.Sucursal
{
    [TestClass]
    public class Data
    {
        public static entities.Model.Sucursal Entity;

        public Data(entities.Model.Sucursal Sucursal)
        {
            Entity = Sucursal;
        }
        public Data()
            : this(new entities.Model.Sucursal()
            {
                Id = 1,
                Nombre = "nombre",
                Fecha = DateTime.Now,
                Activo = true,
                IdEmpresa = 1,
            })
        {
        }

        [TestInitialize]
        public void BeforeEachTest()
        {

        }

        public Mock<IDbCommand> GetCommand(Mock<ISqlBuilder<entities.Model.Sucursal>> mockBuilder)
        {
            //var mockMapper = new Moq.Mock<IMapperTable<domain.Model.Sucursal, data.Model.Sucursal>>();

            //mockMapper.Setup(x => x.Read(It.IsAny<data.Model.Sucursal>(), It.IsAny<IDataReader>(), It.IsAny<IList<string>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            //    .Returns((data.Model.Sucursal data, IDataReader reader, IList<string> prefixname, string aliasseparator, int maxdepth, int depth) =>
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

        public (data.Model.Sucursal data, Mock<ISqlBuilder<entities.Model.Sucursal>> mockBuilder, Mock<IDbCommand> mockCommand) Data_Table_Select()
        {
            var mockBuilder = new Moq.Mock<ISqlBuilder<entities.Model.Sucursal>>();
            var mockCommand = GetCommand(mockBuilder);

            bool read = true;
            var mockDataReader = new Moq.Mock<IDataReader>();
            mockDataReader.Setup(x => x.Read()).Returns(() => read).Callback(() => read = false);
            mockDataReader.Setup(x => x["Sucursal.Id"]).Returns(Entity.Id);
            mockDataReader.Setup(x => x["Sucursal.Nombre"]).Returns(Entity.Nombre);
            mockDataReader.Setup(x => x["Sucursal.Fecha"]).Returns(Entity.Fecha);
            mockDataReader.Setup(x => x["Sucursal.Activo"]).Returns(Entity.Activo);
            mockDataReader.Setup(x => x["Sucursal.IdEmpresa"]).Returns(Entity.IdEmpresa);

            mockCommand.Setup(x => x.ExecuteReader())
                .Returns(mockDataReader.Object);

            return (new data.Model.Sucursal(new RepositoryTable<entities.Model.Sucursal, data.Model.Sucursal>(new data.Mapper.Sucursal(), mockBuilder.Object))
            {
                Id = Entity.Id
            }, mockBuilder, mockCommand);
        }
        public (data.Model.Sucursal data, Mock<ISqlBuilder<entities.Model.Sucursal>> mockBuilder, Mock<IDbCommand> mockCommand) Data_Table_Select_NonDbCommand()
        {
            var dataselect = Data_Table_Select();

            dataselect.mockBuilder.Setup(x => x.Select(It.IsAny<IEntityTable<entities.Model.Sucursal>>()))
                .Returns(dataselect.mockCommand.Object);

            return dataselect;
        }
        public (data.Model.Sucursal data, Mock<ISqlBuilder<entities.Model.Sucursal>> mockBuilder, Mock<IDbCommand> mockCommand) Data_Table_Select_DbCommand()
        {
            var dataselect = Data_Table_Select();

            dataselect.data.SelectDbCommand = (true, ("", CommandType.StoredProcedure, new List<DbParameter>()));
            dataselect.mockBuilder.Setup(x => x.GetCommand(It.IsAny<string>(), It.IsAny<CommandType>(), It.IsAny<IList<DbParameter>>()))
                .Returns(dataselect.mockCommand.Object);

            return dataselect;
        }
        [TestMethod]
        public void Data_Table_Select_NonDbCommand_Success()
        {
            var dataselect = Data_Table_Select_NonDbCommand();

            var select = dataselect.data.Select();

            Assert.IsTrue(select.result.Success);
            Assert.AreEqual(Entity.Id, select.data.Id);
            Assert.AreEqual(Entity.Nombre, select.data.Nombre);
            Assert.AreEqual(Entity.Fecha, select.data.Fecha);
            Assert.AreEqual(Entity.Activo, select.data.Activo);
            Assert.AreEqual(Entity.IdEmpresa, select.data.IdEmpresa);
        }
        [TestMethod]
        public void Data_Table_Select_DbCommand_Success()
        {
            var dataselect = Data_Table_Select_DbCommand();

            var select = dataselect.data.Select();

            Assert.IsTrue(select.result.Success);
            Assert.AreEqual(Entity.Id, select.data.Id);
            Assert.AreEqual(Entity.Nombre, select.data.Nombre);
            Assert.AreEqual(Entity.Fecha, select.data.Fecha);
            Assert.AreEqual(Entity.Activo, select.data.Activo);
            Assert.AreEqual(Entity.IdEmpresa, select.data.IdEmpresa);
        }

        public (data.Model.Sucursal data, Mock<ISqlBuilder<entities.Model.Sucursal>> mockBuilder, Mock<IDbCommand> mockCommand)Data_Table_Insert()
        {
            var mockBuilder = new Moq.Mock<ISqlBuilder<entities.Model.Sucursal>>();
            var mockCommand = GetCommand(mockBuilder);

            mockCommand.Setup(x => x.ExecuteScalar())
                .Returns(Entity.Id);

            return (new data.Model.Sucursal(new RepositoryTable<entities.Model.Sucursal, data.Model.Sucursal>(new data.Mapper.Sucursal(), mockBuilder.Object))
            {
                Nombre = Entity.Nombre,
                Fecha = Entity.Fecha,
                Activo = Entity.Activo,
                IdEmpresa = Entity.IdEmpresa
            }, mockBuilder, mockCommand);
        }
        public (data.Model.Sucursal data, Mock<ISqlBuilder<entities.Model.Sucursal>> mockBuilder, Mock<IDbCommand> mockCommand) Data_Table_Insert_NonDbCommand()
        {
            var datainsert = Data_Table_Insert();

            datainsert.mockBuilder.Setup(x => x.Insert(It.IsAny<IEntityTable<entities.Model.Sucursal>>()))
                .Returns(datainsert.mockCommand.Object);

            return datainsert;
        }
        public (data.Model.Sucursal data, Mock<ISqlBuilder<entities.Model.Sucursal>> mockBuilder, Mock<IDbCommand> mockCommand) Data_Table_Insert_DbCommand()
        {
            var datainsert = Data_Table_Insert();

            datainsert.data.InsertDbCommand = (true, ("", CommandType.StoredProcedure, new List<DbParameter>()));
            datainsert.mockBuilder.Setup(x => x.GetCommand(It.IsAny<string>(), It.IsAny<CommandType>(), It.IsAny<IList<DbParameter>>()))
                .Returns(datainsert.mockCommand.Object);

            return datainsert;
        }
        [TestMethod]
        public void Data_Table_Insert_NonDbCommand_Success()
        {
            var datainsert = Data_Table_Insert_NonDbCommand();

            var insert = datainsert.data.Insert();

            Assert.IsTrue(insert.result.Success);
            Assert.AreEqual(Entity.Id, insert.data.Id);
            Assert.AreEqual(Entity.Nombre, insert.data.Nombre);
            Assert.AreEqual(Entity.Fecha, insert.data.Fecha);
            Assert.AreEqual(Entity.Activo, insert.data.Activo);
            Assert.AreEqual(Entity.IdEmpresa, insert.data.IdEmpresa);
        }
        [TestMethod]
        public void Data_Table_Insert_DbCommand_Success()
        {
            var datainsert = Data_Table_Insert_DbCommand();

            var insert = datainsert.data.Insert();       

            Assert.IsTrue(insert.result.Success);
            Assert.AreEqual(Entity.Id, insert.data.Id);
            Assert.AreEqual(Entity.Nombre, insert.data.Nombre);
            Assert.AreEqual(Entity.Fecha, insert.data.Fecha);
            Assert.AreEqual(Entity.Activo, insert.data.Activo);
            Assert.AreEqual(Entity.IdEmpresa, insert.data.IdEmpresa);
        }

        public (data.Model.Sucursal data, Mock<ISqlBuilder<entities.Model.Sucursal>> mockBuilder, Mock<IDbCommand> mockCommand) Data_Table_Update()
        {
            var mockBuilder = new Moq.Mock<ISqlBuilder<entities.Model.Sucursal>>();
            var mockCommand = GetCommand(mockBuilder);

            mockCommand.Setup(x => x.ExecuteNonQuery())
                .Returns(1);

            return (new data.Model.Sucursal(new RepositoryTable<entities.Model.Sucursal, data.Model.Sucursal>(new data.Mapper.Sucursal(), mockBuilder.Object))
            {
                Id = Entity.Id,
                Nombre = Entity.Nombre,
                Fecha = Entity.Fecha,
                Activo = Entity.Activo,
                IdEmpresa = Entity.IdEmpresa,
            }, mockBuilder, mockCommand);
        }
        public (data.Query.Sucursal data, Mock<ISqlBuilder<entities.Model.Sucursal>> mockBuilder, Mock<IDbCommand> mockCommand) Data_Query_Update()
        {
            var mockBuilder = new Moq.Mock<ISqlBuilder<entities.Model.Sucursal>>();
            var mockCommand = GetCommand(mockBuilder);

            mockCommand.Setup(x => x.ExecuteNonQuery())
                .Returns(1);

            return (new data.Query.Sucursal(new RepositoryQuery<entities.Model.Sucursal, data.Model.Sucursal>(new data.Mapper.Sucursal(), mockBuilder.Object)), mockBuilder, mockCommand);
        }
        public (data.Model.Sucursal data, Mock<ISqlBuilder<entities.Model.Sucursal>> mockBuilder, Mock<IDbCommand> mockCommand) Data_Table_Update_NonDbCommand()
        {
            var dataupdate = Data_Table_Update();

            dataupdate.mockBuilder.Setup(x => x.Update(It.IsAny<IEntityTable<entities.Model.Sucursal>>()))
                .Returns(dataupdate.mockCommand.Object);

            return dataupdate;
        }
        public (data.Model.Sucursal data, Mock<ISqlBuilder<entities.Model.Sucursal>> mockBuilder, Mock<IDbCommand> mockCommand) Data_Table_Update_DbCommand()
        {
            var dataupdate = Data_Table_Update();

            dataupdate.data.UpdateDbCommand = (true, ("", CommandType.StoredProcedure, new List<DbParameter>()));
            dataupdate.mockBuilder.Setup(x => x.GetCommand(It.IsAny<string>(), It.IsAny<CommandType>(), It.IsAny<IList<DbParameter>>()))
                .Returns(dataupdate.mockCommand.Object);

            return dataupdate;
        }
        [TestMethod]
        public void Data_Table_Update_NonDbCommand_Success()
        {
            var dataupdate = Data_Table_Update_NonDbCommand();

            var update = dataupdate.data.Update();

            Assert.IsTrue(update.result.Success);
            Assert.AreEqual(Entity.Id, update.data.Id);
            Assert.AreEqual(Entity.Nombre, update.data.Nombre);
            Assert.AreEqual(Entity.Fecha, update.data.Fecha);
            Assert.AreEqual(Entity.Activo, update.data.Activo);
            Assert.AreEqual(Entity.IdEmpresa, update.data.IdEmpresa);
        }
        [TestMethod]
        public void Data_Table_Update_DbCommand_Success()
        {
            var dataupdate = Data_Table_Update_DbCommand();

            var update = dataupdate.data.Update();

            Assert.IsTrue(update.result.Success);
            Assert.AreEqual(Entity.Id, update.data.Id);
            Assert.AreEqual(Entity.Nombre, update.data.Nombre);
            Assert.AreEqual(Entity.Fecha, update.data.Fecha);
            Assert.AreEqual(Entity.Activo, update.data.Activo);
            Assert.AreEqual(Entity.IdEmpresa, update.data.IdEmpresa);
        }

        public (data.Model.Sucursal data, Mock<ISqlBuilder<entities.Model.Sucursal>> mockBuilder, Mock<IDbCommand> mockCommand) Data_Table_Delete()
        {
            var mockBuilder = new Moq.Mock<ISqlBuilder<entities.Model.Sucursal>>();
            var mockCommand = GetCommand(mockBuilder);

            mockCommand.Setup(x => x.ExecuteNonQuery())
                .Returns(1);

            return (new data.Model.Sucursal(new RepositoryTable<entities.Model.Sucursal, data.Model.Sucursal>(new data.Mapper.Sucursal(), mockBuilder.Object))
            {
                Id = Entity.Id,
                Nombre = Entity.Nombre,
                Fecha = Entity.Fecha,
                Activo = Entity.Activo,
                IdEmpresa = Entity.IdEmpresa
            }, mockBuilder, mockCommand);
        }
        public (data.Query.Sucursal data, Mock<ISqlBuilder<entities.Model.Sucursal>> mockBuilder, Mock<IDbCommand> mockCommand) Data_Query_Delete()
        {
            var mockBuilder = new Moq.Mock<ISqlBuilder<entities.Model.Sucursal>>();
            var mockCommand = GetCommand(mockBuilder);

            mockCommand.Setup(x => x.ExecuteNonQuery())
                .Returns(1);

            return (new data.Query.Sucursal(new RepositoryQuery<entities.Model.Sucursal, data.Model.Sucursal>(new data.Mapper.Sucursal(), mockBuilder.Object)), mockBuilder, mockCommand);
        }
        public (data.Model.Sucursal data, Mock<ISqlBuilder<entities.Model.Sucursal>> mockBuilder, Mock<IDbCommand> mockCommand) Data_Table_Delete_NonDbCommand()
        {
            var datadelete = Data_Table_Delete();

            datadelete.mockBuilder.Setup(x => x.Delete(It.IsAny<IEntityTable<entities.Model.Sucursal>>()))
                .Returns(datadelete.mockCommand.Object);

            datadelete.mockBuilder.Setup(x => x.Delete(It.IsAny<IQueryTable>(), It.IsAny<int>()))
                .Returns(datadelete.mockCommand.Object);

            return datadelete;
        }
        public (data.Model.Sucursal data, Mock<ISqlBuilder<entities.Model.Sucursal>> mockBuilder, Mock<IDbCommand> mockCommand) Data_Table_Delete_DbCommand()
        {
            var datadelete = Data_Table_Delete();

            datadelete.data.DeleteDbCommand = (true, ("", CommandType.StoredProcedure, new List<DbParameter>()));
            datadelete.mockBuilder.Setup(x => x.GetCommand(It.IsAny<string>(), It.IsAny<CommandType>(), It.IsAny<IList<DbParameter>>()))
                .Returns(datadelete.mockCommand.Object);

            return datadelete;
        }
        [TestMethod]
        public void Data_Table_Delete_NonDbCommand_Success()
        {
            var datadelete = Data_Table_Delete_NonDbCommand();

            var delete = datadelete.data.Delete();

            Assert.IsTrue(delete.result.Success);
        }
        [TestMethod]
        public void Data_Table_Delete_DbCommand_Success()
        {
            var datadelete = Data_Table_Delete_DbCommand();

            var delete = datadelete.data.Delete();

            Assert.IsTrue(delete.result.Success);
        }
    }
}
