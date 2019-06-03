using Library.Impl.Persistence.Query;
using Library.Impl.Persistence.Sql;
using Library.Impl.Persistence.Table;
using Library.Interface.Persistence.Database;
using Library.Interface.Persistence.Sql.Builder;
using Library.Interface.Persistence.Sql.Database;
using Library.Interface.Persistence.Sql.Providers;
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
        public static Entities.Table.Sucursal Entity;

        public Data(Entities.Table.Sucursal Sucursal)
        {
            Entity = Sucursal;
        }
        public Data()
            : this(new Entities.Table.Sucursal()
            {
                Id = 1,
                Codigo = "codigo",
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

        public (Mock<IDbCommand> mockCommand, Mock<ISqlSyntaxSign> mockSyntaxSign, Mock<ISqlCreator> mockCreator) 
            GetDatabaseMock()
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

            var mockSyntaxSign = new Moq.Mock<ISqlSyntaxSign>();
            mockSyntaxSign.SetupGet<string>(x => x.AliasSeparatorColumn)
                .Returns(".");

            var mockObjectCreator = new Moq.Mock<IDbObjectCreator>();

            var mockCreator = new Moq.Mock<ISqlCreator>();
            mockCreator.Setup(x => x.GetCommand(It.IsAny<string>(), It.IsAny<CommandType>(), It.IsAny<IList<SqlParameter>>()))
                .Returns(mockCommand.Object);

            return (mockCommand, mockSyntaxSign, mockCreator);
        }

        public Persistence.Table.Sucursal Data_Table_Select()
        {
            var mockDatabase = GetDatabaseMock();

            bool read = true;
            var mockDataReader = new Moq.Mock<IDataReader>();
            mockDataReader.Setup(x => x.Read()).Returns(() => read).Callback(() => read = false);
            mockDataReader.Setup(x => x["Sucursal.Id"]).Returns(Entity.Id);
            mockDataReader.Setup(x => x["Sucursal.Codigo"]).Returns(Entity.Codigo);
            mockDataReader.Setup(x => x["Sucursal.Nombre"]).Returns(Entity.Nombre);
            mockDataReader.Setup(x => x["Sucursal.Fecha"]).Returns(Entity.Fecha);
            mockDataReader.Setup(x => x["Sucursal.Activo"]).Returns(Entity.Activo);
            mockDataReader.Setup(x => x["Sucursal.IdEmpresa"]).Returns(Entity.IdEmpresa);

            mockDatabase.mockCommand.Setup(x => x.ExecuteReader())
                .Returns(mockDataReader.Object);

            var mockCommandBuilder = new Moq.Mock<ISqlCommandBuilder>();

            return new Persistence.Table.Sucursal(new Entities.Table.Sucursal()
            {
                Id = Entity.Id
            },
            new RepositoryTable<Entities.Table.Sucursal, Persistence.Table.Sucursal>(
                new Entities.Reader.Sucursal(mockDatabase.mockSyntaxSign.Object),
                new Persistence.Mapper.Sucursal(),
                mockDatabase.mockSyntaxSign.Object,
                mockCommandBuilder.Object,
                mockDatabase.mockCreator.Object));
        }
        public Persistence.Table.Sucursal Data_Table_Select_NonDbCommand()
        {
            var dataselect = Data_Table_Select();

            return dataselect;
        }
        public Persistence.Table.Sucursal Data_Table_Select_DbCommand()
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
            Assert.AreEqual(Entity.Nombre, select.data.Nombre);
            Assert.AreEqual(Entity.Fecha, select.data.Fecha);
            Assert.AreEqual(Entity.Activo, select.data.Activo);
            Assert.AreEqual(Entity.IdEmpresa, select.data.IdEmpresa);
        }
        [TestMethod]
        public void Data_Table_Select_DbCommand_Success()
        {
            var dataselect = Data_Table_Select_DbCommand();

            var select = dataselect.Select();

            Assert.IsTrue(select.result.Success);
            Assert.AreEqual(Entity.Id, select.data.Id);
            Assert.AreEqual(Entity.Nombre, select.data.Nombre);
            Assert.AreEqual(Entity.Fecha, select.data.Fecha);
            Assert.AreEqual(Entity.Activo, select.data.Activo);
            Assert.AreEqual(Entity.IdEmpresa, select.data.IdEmpresa);
        }

        public Persistence.Table.Sucursal Data_Table_Insert()
        {
            var mockDatabase = GetDatabaseMock();

            mockDatabase.mockCommand.Setup(x => x.ExecuteScalar())
                .Returns(Entity.Id);

            var mockDataReader = new Moq.Mock<IDataReader>();
            mockDataReader.Setup(x => x.Read()).Returns(() => false);
            mockDatabase.mockCommand.Setup(x => x.ExecuteReader())
                .Returns(mockDataReader.Object);

            var mockCommandBuilder = new Moq.Mock<ISqlCommandBuilder>();

            return new Persistence.Table.Sucursal(new Entities.Table.Sucursal()
            {
                Nombre = Entity.Nombre,
                Codigo = Entity.Codigo,
                Fecha = Entity.Fecha,
                Activo = Entity.Activo,
                IdEmpresa = Entity.IdEmpresa
            },
            new RepositoryTable<Entities.Table.Sucursal, Persistence.Table.Sucursal>(
                new Entities.Reader.Sucursal(mockDatabase.mockSyntaxSign.Object),
                new Persistence.Mapper.Sucursal(),
                mockDatabase.mockSyntaxSign.Object,
                mockCommandBuilder.Object,
                mockDatabase.mockCreator.Object),
            new Persistence.Query.Sucursal(new RepositoryQuery<Entities.Table.Sucursal, Persistence.Table.Sucursal>(new Entities.Reader.Sucursal(mockDatabase.mockSyntaxSign.Object),
                new Persistence.Mapper.Sucursal(),
                mockDatabase.mockSyntaxSign.Object,
                mockCommandBuilder.Object,
                mockDatabase.mockCreator.Object)));
        }
        public Persistence.Table.Sucursal Data_Table_Insert_NonDbCommand()
        {
            var datainsert = Data_Table_Insert();

            return datainsert;
        }
        public Persistence.Table.Sucursal Data_Table_Insert_DbCommand()
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
            Assert.AreEqual(Entity.Nombre, insert.data.Nombre);
            Assert.AreEqual(Entity.Fecha, insert.data.Fecha);
            Assert.AreEqual(Entity.Activo, insert.data.Activo);
            Assert.AreEqual(Entity.IdEmpresa, insert.data.IdEmpresa);
        }
        [TestMethod]
        public void Data_Table_Insert_DbCommand_Success()
        {
            var datainsert = Data_Table_Insert_DbCommand();

            var insert = datainsert.Insert();       

            Assert.IsTrue(insert.result.Success);
            Assert.AreEqual(Entity.Id, insert.data.Id);
            Assert.AreEqual(Entity.Nombre, insert.data.Nombre);
            Assert.AreEqual(Entity.Fecha, insert.data.Fecha);
            Assert.AreEqual(Entity.Activo, insert.data.Activo);
            Assert.AreEqual(Entity.IdEmpresa, insert.data.IdEmpresa);
        }

        public Persistence.Table.Sucursal Data_Table_Update()
        {
            var mockDatabase = GetDatabaseMock();

            mockDatabase.mockCommand.Setup(x => x.ExecuteNonQuery())
                .Returns(1);

            var mockCommandBuilder = new Moq.Mock<ISqlCommandBuilder>();

            return new Persistence.Table.Sucursal(new Entities.Table.Sucursal()
            {
                Id = Entity.Id,
                Nombre = Entity.Nombre,
                Fecha = Entity.Fecha,
                Activo = Entity.Activo,
                IdEmpresa = Entity.IdEmpresa,
            },
            new RepositoryTable<Entities.Table.Sucursal, Persistence.Table.Sucursal>(
                new Entities.Reader.Sucursal(mockDatabase.mockSyntaxSign.Object),
                new Persistence.Mapper.Sucursal(),
                mockDatabase.mockSyntaxSign.Object,
                mockCommandBuilder.Object,
                mockDatabase.mockCreator.Object));
        }
        public Persistence.Query.Sucursal Data_Query_Update()
        {
            var mockDatabase = GetDatabaseMock();

            mockDatabase.mockCommand.Setup(x => x.ExecuteNonQuery())
                .Returns(1);

            var mockCommandBuilder = new Moq.Mock<ISqlCommandBuilder>();

            return new Persistence.Query.Sucursal(new RepositoryQuery<Entities.Table.Sucursal, Persistence.Table.Sucursal>(
                new Entities.Reader.Sucursal(mockDatabase.mockSyntaxSign.Object),
                new Persistence.Mapper.Sucursal(),
                mockDatabase.mockSyntaxSign.Object,
                mockCommandBuilder.Object,
                mockDatabase.mockCreator.Object));
        }
        public Persistence.Table.Sucursal Data_Table_Update_NonDbCommand()
        {
            var dataupdate = Data_Table_Update();

            return dataupdate;
        }
        public Persistence.Table.Sucursal Data_Table_Update_DbCommand()
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
            Assert.AreEqual(Entity.Nombre, update.data.Nombre);
            Assert.AreEqual(Entity.Fecha, update.data.Fecha);
            Assert.AreEqual(Entity.Activo, update.data.Activo);
            Assert.AreEqual(Entity.IdEmpresa, update.data.IdEmpresa);
        }
        [TestMethod]
        public void Data_Table_Update_DbCommand_Success()
        {
            var dataupdate = Data_Table_Update_DbCommand();

            var update = dataupdate.Update();

            Assert.IsTrue(update.result.Success);
            Assert.AreEqual(Entity.Id, update.data.Id);
            Assert.AreEqual(Entity.Nombre, update.data.Nombre);
            Assert.AreEqual(Entity.Fecha, update.data.Fecha);
            Assert.AreEqual(Entity.Activo, update.data.Activo);
            Assert.AreEqual(Entity.IdEmpresa, update.data.IdEmpresa);
        }

        public Persistence.Table.Sucursal Data_Table_Delete()
        {
            var mockDatabase = GetDatabaseMock();

            mockDatabase.mockCommand.Setup(x => x.ExecuteNonQuery())
                .Returns(1);

            var mockCommandBuilder = new Moq.Mock<ISqlCommandBuilder>();

            return new Persistence.Table.Sucursal(new Entities.Table.Sucursal()
            {
                Id = Entity.Id,
                Nombre = Entity.Nombre,
                Fecha = Entity.Fecha,
                Activo = Entity.Activo,
                IdEmpresa = Entity.IdEmpresa
            },
            new RepositoryTable<Entities.Table.Sucursal, Persistence.Table.Sucursal>(
                new Entities.Reader.Sucursal(mockDatabase.mockSyntaxSign.Object),
                new Persistence.Mapper.Sucursal(),
                mockDatabase.mockSyntaxSign.Object,
                mockCommandBuilder.Object,
                mockDatabase.mockCreator.Object));
        }
        public Persistence.Query.Sucursal Data_Query_Delete()
        {
            var mockRepository = GetDatabaseMock();

            mockRepository.mockCommand.Setup(x => x.ExecuteNonQuery())
                .Returns(1);

            var mockBuilderQuery = new Moq.Mock<ISqlBuilderQuery>();

            return new Persistence.Query.Sucursal();
        }
        public Persistence.Table.Sucursal Data_Table_Delete_NonDbCommand()
        {
            var datadelete = Data_Table_Delete();

            return datadelete;
        }
        public Persistence.Table.Sucursal Data_Table_Delete_DbCommand()
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
