using Library.Interface.Data.Sql.Builder;
using System;
using System.Configuration;

namespace Library.Impl.Data.Sql.Builder
{
    public abstract class AbstractSqlSyntaxSign : ISqlSyntaxSign
    {
        public abstract string ProviderName
        {
            get;
        }


        protected string _parameterprefix;
        public abstract string ParameterPrefix
        {
            get;
        }

        protected string _parameterseparator;
        public virtual string ParameterSeparator
        {
            get
            {
                if (_parameterseparator == null)
                    _parameterseparator = ConfigurationManager.AppSettings["sql.parameter.separator"] ?? "";

                return _parameterseparator;
            }
        }

        protected string _parameterassignment;
        public abstract string ParameterAssignment
        {
            get;
        }


        protected string _aliasseparatorcolumn;
        public virtual string AliasSeparatorColumn
        {
            get
            {
                if (_aliasseparatorcolumn == null)
                    _aliasseparatorcolumn = ConfigurationManager.AppSettings["sql.alias.separator.column"] ?? ".";

                return _aliasseparatorcolumn;
            }
        }

        protected string _aliasseparatorcolumnkeyword;
        public abstract string AliasSeparatorColumnKeyword
        {
            get;
        }

        protected string _aliasseparatortable;
        public virtual string AliasSeparatorTable
        {
            get
            {
                if (_aliasseparatortable == null)
                    _aliasseparatortable = ConfigurationManager.AppSettings["sql.alias.separator.table"] ?? "_";

                return _aliasseparatortable;
            }
        }

        protected string _aliasseparatortablekeyword;
        public abstract string AliasSeparatorTableKeyword
        {
            get;
        }


        protected string _aliasenclosurecolumnopen;
        public abstract string AliasEnclosureColumnOpen
        {
            get;
        }

        protected string _aliasenclosurecolumnclose;
        public abstract string AliasEnclosureColumnClose
        {
            get;
        }

        protected string _aliasenclosuretableopen;
        public abstract string AliasEnclosureTableOpen
        {
            get;
        }

        protected string _aliasenclosuretableclose;
        public abstract string AliasEnclosureTableClose
        {
            get;
        }

        protected bool _updatesetusealias;
        public virtual bool UpdateSetUseAlias
        {
            get
            {
                var updatesetusealias = ConfigurationManager.AppSettings["sql.update.set.use.alias"];
                _updatesetusealias = !string.IsNullOrWhiteSpace(updatesetusealias) ? Convert.ToBoolean(updatesetusealias): true;

                return _updatesetusealias;
            }
        }

        protected bool _updatewhereusealias;
        public virtual bool UpdateWhereUseAlias
        {
            get
            {
                var updatewhereusealias = ConfigurationManager.AppSettings["sql.update.where.use.alias"];
                _updatewhereusealias = !string.IsNullOrWhiteSpace(updatewhereusealias) ? Convert.ToBoolean(updatewhereusealias) : true;

                return _updatewhereusealias;
            }
        }

        public abstract string GetOperator(WhereOperator? whereoperator);

        protected string _wherewildcardsingle;
        public abstract string WhereWildcardSingle
        {
            get;
        }

        protected string _wherewildcardany;
        public abstract string WhereWildcardAny
        {
            get;
        }
    }
}
