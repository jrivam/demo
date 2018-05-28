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

        public domain.Model.Empresa Domain_Load_Query()
        {
            var dataselect = Data.Data_Table_Select_NonDbCommand();

            return new domain.Model.Empresa(dataselect.data)
            {
            };
        }
        [TestMethod]
        public void Domain_Load_Query_Success()
        {
            var domainload = Domain_Load_Query().Load();

            Assert.IsTrue(domainload.result.Success);
            Assert.AreEqual(Data.Entity.Id, domainload.domain.Id);
            Assert.AreEqual(Data.Entity.RazonSocial, domainload.domain.RazonSocial);
            Assert.AreEqual(Data.Entity.Activo, domainload.domain.Activo);
        }

        public domain.Model.Empresa Domain_Erase_Query()
        {
            var datadelete = Data.Data_Table_Delete_NonDbCommand();

            return new domain.Model.Empresa(datadelete.data)
            {
            };
        }
        [TestMethod]
        public void Domain_Erase_Query_Success()
        {
            var domainerase = Domain_Erase_Query().Erase();

            Assert.IsTrue(domainerase.result.Success);
            Assert.IsTrue(domainerase.domain.Deleted);
        }

        public domain.Model.Empresa Domain_Save_Query_Insert()
        {
            var datainsert = Data.Data_Table_Insert_NonDbCommand();

            return new domain.Model.Empresa(datainsert.data)
            {
            };
        }
        [TestMethod]
        public void Domain_Save_Insert_Query_Success()
        {
            var domainsave = Domain_Save_Query_Insert();

            domainsave.RazonSocial += "1";
            domainsave.Activo = false;

            var save = domainsave.Save();

            Assert.IsTrue(save.result.Success);
            Assert.IsTrue(!save.domain.Changed);
            Assert.AreEqual(Data.Entity.Id, save.domain.Id);
            Assert.AreEqual(Data.Entity.RazonSocial + "1", save.domain.RazonSocial);
            Assert.AreEqual(!Data.Entity.Activo, false);
        }

        public domain.Model.Empresa Domain_Save_Query_Update()
        {
            var datainsert = Data.Data_Table_Update_NonDbCommand();

            return new domain.Model.Empresa(datainsert.data)
            {
            };
        }
        [TestMethod]
        public void Domain_Save_Update_Query_Success()
        {
            var domainsave = Domain_Save_Query_Insert();

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
