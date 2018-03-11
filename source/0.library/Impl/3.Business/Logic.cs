using library.Interface.Business;
using library.Interface.Data;
using library.Interface.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace library.Impl.Business
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

        public virtual V Clear(V business, IEntityRepository<T, U> repository, int maxdepth = 1)
        {
            business.Data = repository.Clear();

            business.Loaded = false;
            business.Changed = false;
            business.Deleted = false;

            _mapper.Map(business, maxdepth, 0);

            return business;
        }

        public virtual (Result result, V business) Load(V business, IEntityRepository<T, U> repository)
        {
            _mapper.Clear(business, 1, 0);

            var select = repository.Select();
            business.Data = select.data;

            if (select.result.Success && select.result.Passed)
            {
                _mapper.Map(business, 1, 0);

                business.Loaded = true;
                business.Changed = false;
                business.Deleted = false;
            }

            return (select.result, business);
        }
        public virtual (Result result, V business) Save(V business, IEntityRepository<T, U> repository)
        {
            if (business.Changed)
            {
                var updateinsert = (business.Loaded ? repository.Update() : repository.Insert());
                business.Data = updateinsert.data;

                if (updateinsert.result.Success && updateinsert.result.Passed)
                {
                    _mapper.Map(business, 1, 0);

                    business.Loaded = true;
                    business.Changed = false;
                    business.Deleted = false;
                }

                return (updateinsert.result, business);
            }

            return (new Result() { Success = true, Messages = new List<(ResultCategory, string)>() { (ResultCategory.Information, "Save: without changes") } }, business);
        }
        public virtual (Result result, V business) Erase(V business, IEntityRepository<T, U> repository)
        {
            if (!business.Deleted)
            {
                var delete = repository.Delete();
                business.Data = delete.data;

                if (delete.result.Success && delete.result.Passed)
                {
                    _mapper.Map(business, 1, 0);

                    business.Loaded = false;
                    business.Deleted = true;
                }

                return (delete.result, business);
            }

            return (new Result() { Success = true, Messages = new List<(ResultCategory, string)>() { (ResultCategory.Information, "Erase: already deleted") } }, business);
        }

        public virtual (Result result, V business) Retrieve(IQueryRepository<T, U> repository, int maxdepth = 1, V business = default(V))
        {
            if (business == null)
                business = (V)Activator.CreateInstance(typeof(V),
                    BindingFlags.CreateInstance |
                    BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.OptionalParamBinding, null, null, CultureInfo.CurrentCulture);

            var selectsingle = repository.SelectSingle(maxdepth, business.Data);
            business.Data = selectsingle.data;

            if (selectsingle.result.Success && selectsingle.result.Passed)
            {
                _mapper.Map(business, maxdepth, 0);

                business.Loaded = true;
                business.Changed = false;
                business.Deleted = false;
            }

            return (selectsingle.result, business);
        }
        public virtual (Result result, IEnumerable<V> businesses) List(IQueryRepository<T, U> repository, int maxdepth = 1, int top = 0)
        {
            var list = new List<V>();

            var selectmultiple = repository.SelectMultiple(maxdepth, top);
            if (selectmultiple.result.Success && selectmultiple.result.Passed)
            {
                foreach (var iterator in selectmultiple.datas)
                {
                    var business = (V)Activator.CreateInstance(typeof(V),
                    BindingFlags.CreateInstance |
                    BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.OptionalParamBinding, null, null, CultureInfo.CurrentCulture);
                    business.Data = iterator;

                    _mapper.Map(business, maxdepth, 0);

                    business.Loaded = true;
                    business.Changed = false;
                    business.Deleted = false;

                    list.Add(business);
                }
            }

            return (selectmultiple.result, list);
        }
    }
}
