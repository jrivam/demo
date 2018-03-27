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
    public class Empresa
    {
        private entities.Model.Empresa empresa = new entities.Model.Empresa()
        {
            Id = 1,
            RazonSocial = "razon_social",
            Activo = true
        };

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

        private data.Model.Empresa Data_Select(Mock<ISqlBuilder<entities.Model.Empresa>> mockBuilder, Mock<IDbCommand> mockCommand)
        {
            bool read = true;
            var mockDataReader = new Moq.Mock<IDataReader>();
            mockDataReader.Setup(x => x.Read()).Returns(() => read).Callback(() => read = false);
            mockDataReader.Setup(x => x["Id"]).Returns(empresa.Id);
            mockDataReader.Setup(x => x["RazonSocial"]).Returns(empresa.RazonSocial);
            mockDataReader.Setup(x => x["Activo"]).Returns(empresa.Activo);

            mockCommand.Setup(x => x.ExecuteReader())
                .Returns(mockDataReader.Object);

            return new data.Model.Empresa(new Repository<entities.Model.Empresa, data.Model.Empresa>(new data.Mapper.Empresa(), mockBuilder.Object), "", "")
            {
                Id = empresa.Id
            };
        }
        [TestMethod]
        public void Data_Select_Success()
        {
            var mockBuilder = new Moq.Mock<ISqlBuilder<entities.Model.Empresa>>();
            var mockCommand = GetCommand(mockBuilder);

            var data = Data_Select(mockBuilder, mockCommand);

            mockBuilder.Setup(x => x.Select(It.IsAny<IEntityTable<entities.Model.Empresa>>()))
                .Returns(mockCommand.Object);

            var select = data.Select();

            Assert.IsTrue(select.result.Success);
            Assert.AreEqual(empresa.Id, select.data.Id);
            Assert.AreEqual(empresa.RazonSocial, select.data.RazonSocial);
            Assert.AreEqual(empresa.Activo, select.data.Activo);
        }
        [TestMethod]
        public void Data_Select_DbCommand_Success()
        {
            var mockBuilder = new Moq.Mock<ISqlBuilder<entities.Model.Empresa>>();
            var mockCommand = GetCommand(mockBuilder);

            var data = Data_Select(mockBuilder, mockCommand);

            data.SelectDbCommand = ("", CommandType.StoredProcedure, new List<DbParameter>());
            mockBuilder.Setup(x => x.GetCommand(It.IsAny<string>(), It.IsAny<CommandType>(), It.IsAny<IList<DbParameter>>()))
                .Returns(mockCommand.Object);

            var select = data.Select();

            Assert.IsTrue(select.result.Success);
            Assert.AreEqual(empresa.Id, select.data.Id);
            Assert.AreEqual(empresa.RazonSocial, select.data.RazonSocial);
            Assert.AreEqual(empresa.Activo, select.data.Activo);
        }

        private data.Model.Empresa Data_Insert(Mock<ISqlBuilder<entities.Model.Empresa>> mockBuilder, Mock<IDbCommand> mockCommand)
        {
            mockCommand.Setup(x => x.ExecuteScalar())
                .Returns(empresa.Id);

            return new data.Model.Empresa(new Repository<entities.Model.Empresa, data.Model.Empresa>(new data.Mapper.Empresa(), mockBuilder.Object), "", "")
            {
                RazonSocial = empresa.RazonSocial,
                Activo = empresa.Activo
            };
        }
        [TestMethod]
        public void Data_Insert_Success()
        {
            var mockBuilder = new Moq.Mock<ISqlBuilder<entities.Model.Empresa>>();
            var mockCommand = GetCommand(mockBuilder);

            var data = Data_Insert(mockBuilder, mockCommand);

            mockBuilder.Setup(x => x.Insert(It.IsAny<IEntityTable<entities.Model.Empresa>>()))
                .Returns(mockCommand.Object);

            var insert = data.Insert();

            Assert.IsTrue(insert.result.Success);
            Assert.AreEqual(empresa.Id, insert.data.Id);
            Assert.AreEqual(empresa.RazonSocial, insert.data.RazonSocial);
            Assert.AreEqual(empresa.Activo, insert.data.Activo);
        }
        [TestMethod]
        public void Data_Insert_DbCommand_Success()
        {
            var mockBuilder = new Moq.Mock<ISqlBuilder<entities.Model.Empresa>>();
            var mockCommand = GetCommand(mockBuilder);

            var data = Data_Insert(mockBuilder, mockCommand);

            data.InsertDbCommand = ("", CommandType.StoredProcedure, new List<DbParameter>());
            mockBuilder.Setup(x => x.GetCommand(It.IsAny<string>(), It.IsAny<CommandType>(), It.IsAny<IList<DbParameter>>()))
                .Returns(mockCommand.Object);

            var insert = data.Insert();       

            Assert.IsTrue(insert.result.Success);
            Assert.AreEqual(empresa.Id, insert.data.Id);
            Assert.AreEqual(empresa.RazonSocial, insert.data.RazonSocial);
            Assert.AreEqual(empresa.Activo, insert.data.Activo);
        }

        private data.Model.Empresa Data_Update(Mock<ISqlBuilder<entities.Model.Empresa>> mockBuilder, Mock<IDbCommand> mockCommand)
        {
            mockCommand.Setup(x => x.ExecuteNonQuery())
                .Returns(1);

            return new data.Model.Empresa(new Repository<entities.Model.Empresa, data.Model.Empresa>(new data.Mapper.Empresa(), mockBuilder.Object), "", "")
            {
                Id = empresa.Id,
                RazonSocial = empresa.RazonSocial,
                Activo = empresa.Activo
            };
        }
        [TestMethod]
        public void Data_Update_Success()
        {
            var mockBuilder = new Moq.Mock<ISqlBuilder<entities.Model.Empresa>>();
            var mockCommand = GetCommand(mockBuilder);

            var data = Data_Update(mockBuilder, mockCommand);

            mockBuilder.Setup(x => x.Update(It.IsAny<IEntityTable<entities.Model.Empresa>>()))
                .Returns(mockCommand.Object);

            var update = data.Update();

            Assert.IsTrue(update.result.Success);
            Assert.AreEqual(empresa.Id, update.data.Id);
            Assert.AreEqual(empresa.RazonSocial, update.data.RazonSocial);
            Assert.AreEqual(empresa.Activo, update.data.Activo);
        }
        [TestMethod]
        public void Data_Update_DbCommand_Success()
        {
            var mockBuilder = new Moq.Mock<ISqlBuilder<entities.Model.Empresa>>();
            var mockCommand = GetCommand(mockBuilder);

            var data = Data_Update(mockBuilder, mockCommand);

            data.UpdateDbCommand = ("", CommandType.StoredProcedure, new List<DbParameter>());
            mockBuilder.Setup(x => x.GetCommand(It.IsAny<string>(), It.IsAny<CommandType>(), It.IsAny<IList<DbParameter>>()))
                .Returns(mockCommand.Object);

            var update = data.Update();

            Assert.IsTrue(update.result.Success);
            Assert.AreEqual(empresa.Id, update.data.Id);
            Assert.AreEqual(empresa.RazonSocial, update.data.RazonSocial);
            Assert.AreEqual(empresa.Activo, update.data.Activo);
        }

        private data.Model.Empresa Data_Delete(Mock<ISqlBuilder<entities.Model.Empresa>> mockBuilder, Mock<IDbCommand> mockCommand)
        {
            mockCommand.Setup(x => x.ExecuteNonQuery())
                .Returns(1);

            return new data.Model.Empresa(new Repository<entities.Model.Empresa, data.Model.Empresa>(new data.Mapper.Empresa(), mockBuilder.Object), "", "")
            {
                Id = empresa.Id,
                RazonSocial = empresa.RazonSocial,
                Activo = empresa.Activo
            };
        }
        [TestMethod]
        public void Data_Delete_Success()
        {
            var mockBuilder = new Moq.Mock<ISqlBuilder<entities.Model.Empresa>>();
            var mockCommand = GetCommand(mockBuilder);

            var data = Data_Delete(mockBuilder, mockCommand);

            mockBuilder.Setup(x => x.Delete(It.IsAny<IEntityTable<entities.Model.Empresa>>()))
                .Returns(mockCommand.Object);

            var delete = data.Delete();

            Assert.IsTrue(delete.result.Success);
        }
        [TestMethod]
        public void Data_Delete_DbCommand_Success()
        {
            var mockBuilder = new Moq.Mock<ISqlBuilder<entities.Model.Empresa>>();
            var mockCommand = GetCommand(mockBuilder);

            var data = Data_Delete(mockBuilder, mockCommand);

            data.DeleteDbCommand = ("", CommandType.StoredProcedure, new List<DbParameter>());
            mockBuilder.Setup(x => x.GetCommand(It.IsAny<string>(), It.IsAny<CommandType>(), It.IsAny<IList<DbParameter>>()))
                .Returns(mockCommand.Object);

            var delete = data.Delete();

            Assert.IsTrue(delete.result.Success);
        }
    }
}
