using library.Interface.Data.Sql;
using System.Configuration;

namespace library.Impl.Data.Sql
{
    public abstract class AbstractSqlSyntaxSign : ISqlSyntaxSign
    {
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
    }
}
