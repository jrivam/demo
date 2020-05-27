using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace demo.Tests.Sucursal
{
    [TestClass]
    public class Domain
    {
        public static Tests.Sucursal.Data Data;

        public Domain(Tests.Sucursal.Data data)
        {
            Data = data;
        }
        public Domain()
            : this(new Tests.Sucursal.Data())
        {
        }

        [TestInitialize]
        public void BeforeEachTest()
        {

        }

        public Business.Table.Sucursal Domain_NonDbCommand_Load()
        {
            var dataselect = Data.Data_Table_Select_NonDbCommand();

            return new Business.Table.Sucursal(dataselect)
            {
            };
        }
        [TestMethod]
        public void Domain_Load_NonDbCommand_Success()
        {
            var domainload = Domain_NonDbCommand_Load().Load();

            Assert.IsTrue(domainload.result.Success);
            Assert.AreEqual(Data.Entity.Id, domainload.domain.Id);
            Assert.AreEqual(Data.Entity.Nombre, domainload.domain.Nombre);
            Assert.AreEqual(Data.Entity.Fecha, domainload.domain.Fecha);
            Assert.AreEqual(Data.Entity.Activo, domainload.domain.Activo);
            Assert.AreEqual(Data.Entity.IdEmpresa, domainload.domain.IdEmpresa);
        }

        public Business.Table.Sucursal Domain_NonDbCommand_Erase()
        {
            var datadelete = Data.Data_Table_Delete_NonDbCommand();

            return new Business.Table.Sucursal(datadelete)
            {
            };
        }
        [TestMethod]
        public void Domain_Erase_NonDbCommand_Success()
        {
            var domainerase = Domain_NonDbCommand_Erase().Erase();

            Assert.IsTrue(domainerase.result.Success);
            Assert.IsTrue(domainerase.domain.Deleted);
        }

        public Business.Table.Sucursal Domain_Save_NonDbCommand_Insert()
        {
            var datainsert = Data.Data_Table_Insert_NonDbCommand();

            return new Business.Table.Sucursal(datainsert)
            {
            };
        }
        [TestMethod]
        public void Domain_Save_Insert_NonDbCommand_Success()
        {
            var domainsave = Domain_Save_NonDbCommand_Insert();

            domainsave.Codigo = "codigo";
            domainsave.Nombre += "1";
            domainsave.Activo = false;

            var save = domainsave.Save();

            Assert.IsTrue(save.result.Success);
            Assert.IsTrue(!save.domain.Changed);
            Assert.AreEqual(Data.Entity.Id, save.domain.Id);
            Assert.AreEqual(Data.Entity.Codigo, save.domain.Codigo);
            Assert.AreEqual($"{Data.Entity.Nombre}1", save.domain.Nombre);
            Assert.AreEqual(!Data.Entity.Activo, false);
        }

        public Business.Table.Sucursal Domain_Save_NonDbCommand_Update()
        {
            var datainsert = Data.Data_Table_Update_NonDbCommand();

            return new Business.Table.Sucursal(datainsert)
            {
            };
        }
        [TestMethod]
        public void Domain_Save_Update_NonDbCommand_Success()
        {
            var domainsave = Domain_Save_NonDbCommand_Insert();

            domainsave.Nombre += "1";
            domainsave.Activo = false;

            var save = domainsave.Save();

            Assert.IsTrue(save.result.Success);
            Assert.IsTrue(!save.domain.Changed);
            Assert.AreEqual(Data.Entity.Id, save.domain.Id);
            Assert.AreEqual(Data.Entity.Nombre + "1", save.domain.Nombre);
            Assert.AreEqual(!Data.Entity.Activo, false);
        }

    }
}
