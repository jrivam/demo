using library.Interface.Data;
using library.Interface.Domain;
using library.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace library.Impl.Domain
{
    public class Logic<T, U, V> : ILogic<T, U, V> where T : IEntity
                                                  where U : IEntityTable<T>
                                                  where V : IEntityState<T, U>
    {
        protected readonly IMapperState<T, U, V> _mapper;

        public Logic(IMapperState<T, U, V> mapper)
        {
            _mapper = mapper;
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

                if (select.result.Success)
                {
                    _mapper.Clear(domain);
                    _mapper.Map(domain);

                    domain.Changed = false;
                    domain.Deleted = false;
                }

                return (select.result, domain);
            }

            return (new Result() { Messages = new List<(ResultCategory, string)>() { (ResultCategory.Information, "Load: empty primary key") } }, domain);
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

        public virtual (Result result, V domain) Retrieve(IQueryRepository<T, U> queryrepository, int maxdepth = 1, V domain = default(V))
        {
            if (domain == null)
            {
                domain = (V)Activator.CreateInstance(typeof(V),
                    BindingFlags.CreateInstance |
                    BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.OptionalParamBinding, null, null, CultureInfo.CurrentCulture);
            }

            var selectsingle = queryrepository.SelectSingle(maxdepth, domain.Data);

            if (selectsingle.result.Success)
            {
                _mapper.Clear(domain);
                _mapper.Map(domain);

                domain.Changed = false;
                domain.Deleted = false;
            }

            return (selectsingle.result, domain);
        }
        public virtual (Result result, IEnumerable<V> domains) List(IQueryRepository<T, U> queryrepository, int maxdepth = 1, int top = 0, IList<V> domains = null)
        {
            var enumeration = new List<V>();
            var iterator = (domains ?? new List<V>()).GetEnumerator();

            var selectmultiple = queryrepository.SelectMultiple(maxdepth, top);
            if (selectmultiple.result.Success)
            {
                foreach (var data in selectmultiple.datas)
                {
                    var domain = iterator.MoveNext() ? iterator.Current : (V)Activator.CreateInstance(typeof(V),
                        BindingFlags.CreateInstance |
                        BindingFlags.Public |
                        BindingFlags.Instance |
                        BindingFlags.OptionalParamBinding, null, new object[] { data }, CultureInfo.CurrentCulture);

                    _mapper.Clear(domain);
                    _mapper.Map(domain);

                    domain.Changed = false;
                    domain.Deleted = false;

                    enumeration.Add(domain);
                }
            }

            return (selectmultiple.result, enumeration);
        }
    }
}
