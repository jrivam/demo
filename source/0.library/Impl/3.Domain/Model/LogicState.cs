using library.Interface.Data.Model;
using library.Interface.Domain.Mapper;
using library.Interface.Domain.Model;
using library.Interface.Entities;
using System.Collections.Generic;

namespace library.Impl.Domain.Model
{
    public class LogicState<T, U, V> : Logic<T, U, V>, ILogicState<T, U, V> 
        where T : IEntity
        where U : IEntityTable<T>
        where V : IEntityState<T, U>
    {
        public LogicState(IMapperState<T, U, V> mapper)
            : base(mapper)
        {
        }

        public virtual V Clear(V domain, IEntityRepository<T, U> entityrepository)
        {
            entityrepository.Clear();

            domain.Changed = false;
            domain.Deleted = false;

            _mapper.Map(domain);

            return domain;
        }

        public virtual (Result result, V domain) Load(V domain, IEntityRepository<T, U> entityrepository, bool usedbcommand = false)
        {
            if (domain.Data.Entity.Id != null)
            {
                var select = entityrepository.Select(usedbcommand);

                if (select.result.Success && select.data != null)
                {
                    _mapper.Clear(domain);
                    _mapper.Map(domain);

                    domain.Changed = false;
                    domain.Deleted = false;

                    return (select.result, domain);
                }

                return (select.result, default(V));
            }

            return (new Result() { Messages = new List<(ResultCategory, string)>() { (ResultCategory.Error, "Load: Id cannot be null") } }, domain);
        }
        public virtual (Result result, V domain) Save(V domain, IEntityRepository<T, U> entityrepository, bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            if (domain.Changed)
            {
                var updateinsert = (domain.Data.Entity.Id != null ? entityrepository.Update(useupdatedbcommand) : entityrepository.Insert(useinsertdbcommand));

                if (updateinsert.result.Success)
                {
                    _mapper.Map(domain);

                    domain.Changed = false;
                }

                return (updateinsert.result, domain);
            }

            return (new Result() { Messages = new List<(ResultCategory, string)>() { (ResultCategory.Information, "Save: no changes to persist") } }, domain);
        }
        public virtual (Result result, V domain) Erase(V domain, IEntityRepository<T, U> entityrepository, bool usedbcommand = false)
        {
            if (domain.Data.Entity.Id != null)
            {
                if (!domain.Deleted)
                {
                    var delete = entityrepository.Delete(usedbcommand);

                    if (delete.result.Success)
                    {
                        _mapper.Map(domain);

                        domain.Deleted = true;
                    }

                    return (delete.result, domain);
                }

                return (new Result() { Messages = new List<(ResultCategory, string)>() { (ResultCategory.Information, "Erase: already deleted") } }, domain);
            }

            return (new Result() { Messages = new List<(ResultCategory, string)>() { (ResultCategory.Error, "Erase: Id cannot be null") } }, domain);
        }
    }
}
