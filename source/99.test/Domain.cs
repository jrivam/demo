using library.Impl.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test
{
    [TestClass]
    public class Domain
    {
        public static test.Data Data;

        public Domain(test.Data data)
        {
            Data = data;
        }
        public Domain()
            : this(new test.Data())
        {
        }

        [TestInitialize]
        public void BeforeEachTest()
        {

        }

        private domain.Model.Empresa Domain_Load()
        {
            var dataselect = Data.Data_Select_Query();

            return new domain.Model.Empresa(new Logic<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa>(new domain.Mapper.Empresa()), dataselect.data)
            {
            };
        }
        [TestMethod]
        public void Domain_Load_Success()
        {
            var domainload = Domain_Load().Load();

            Assert.IsTrue(domainload.result.Success);
            Assert.AreEqual(Data.Entity.Id, domainload.domain.Id);
            Assert.AreEqual(Data.Entity.RazonSocial, domainload.domain.RazonSocial);
            Assert.AreEqual(Data.Entity.Activo, domainload.domain.Activo);
        }

        private domain.Model.Empresa Domain_Erase()
        {
            var datadelete = Data.Data_Delete_Query();

            return new domain.Model.Empresa(new Logic<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa>(new domain.Mapper.Empresa()), datadelete.data)
            {
            };
        }
        [TestMethod]
        public void Domain_Erase_Success()
        {
            var domainerase = Domain_Erase().Erase();

            Assert.IsTrue(domainerase.result.Success);
            Assert.IsTrue(domainerase.domain.Deleted);
        }

        private domain.Model.Empresa Domain_Save_Insert()
        {
            var datainsert = Data.Data_Insert_Query();

            return new domain.Model.Empresa(new Logic<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa>(new domain.Mapper.Empresa()), datainsert.data)
            {
            };
        }
        [TestMethod]
        public void Domain_Save_Insert_Success()
        {
            var domainsave = Domain_Save_Insert();

            domainsave.RazonSocial += "1";
            domainsave.Activo = false;

            var save = domainsave.Save();

            Assert.IsTrue(save.result.Success);
            Assert.IsTrue(!save.domain.Changed);
            Assert.AreEqual(Data.Entity.Id, save.domain.Id);
            Assert.AreEqual(Data.Entity.RazonSocial + "1", save.domain.RazonSocial);
            Assert.AreEqual(!Data.Entity.Activo, false);
        }

        private domain.Model.Empresa Domain_Save_Update()
        {
            var datainsert = Data.Data_Update_Query();

            return new domain.Model.Empresa(new Logic<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa>(new domain.Mapper.Empresa()), datainsert.data)
            {
            };
        }
        [TestMethod]
        public void Domain_Save_Update_Success()
        {
            var domainsave = Domain_Save_Insert();

            domainsave.RazonSocial += "1";
            domainsave.Activo = false;

            var save = domainsave.Save();

            Assert.IsTrue(save.result.Success);
            Assert.IsTrue(!save.domain.Changed);
            Assert.AreEqual(Data.Entity.Id, save.domain.Id);
            Assert.AreEqual(Data.Entity.RazonSocial + "1", save.domain.RazonSocial);
            Assert.AreEqual(!Data.Entity.Activo, false);
        }

    }
}
