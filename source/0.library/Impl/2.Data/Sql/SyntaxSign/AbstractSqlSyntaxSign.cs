using library.Interface.Data.Sql;
using System.Configuration;

namespace library.Impl.Data.Sql.SyntaxSign
{
    public abstract class AbstractSqlSyntaxSign : ISqlSyntaxSign
    {
        protected string _parameterprefix;
        public virtual string ParameterPrefix
        {
            get
            {
                if (_parameterprefix == null)
                    _parameterprefix = ConfigurationManager.AppSettings["sql.parameter.prefix"] ?? "@";

                return _parameterprefix;
            }
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
        public virtual string ParameterAssignment
        {
            get
            {
                if (_parameterassignment == null)
                    _parameterassignment = ConfigurationManager.AppSettings["sql.parameter.assignment"] ?? "=";

                return _parameterassignment;
            }
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
        public virtual string AliasSeparatorColumnKeyword
        {
            get
            {
                if (_aliasseparatorcolumnkeyword == null)
                    _aliasseparatorcolumnkeyword = ConfigurationManager.AppSettings["sql.alias.separator.column.keyword"] ?? "as";

                return _aliasseparatorcolumnkeyword;
            }
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
        public virtual string AliasSeparatorTableKeyword
        {
            get
            {
                if (_aliasseparatortablekeyword == null)
                    _aliasseparatortablekeyword = ConfigurationManager.AppSettings["sql.alias.separator.table.keyword"] ?? "as";

                return _aliasseparatortablekeyword;
            }
        }


        protected string _aliasenclosurecolumnopen;
        public virtual string AliasEnclosureColumnOpen
        {
            get
            {
                if (_aliasenclosurecolumnopen == null)
                    _aliasenclosurecolumnopen = ConfigurationManager.AppSettings["sql.alias.enclosure.column.open"] ?? "[";

                return _aliasenclosurecolumnopen;
            }
        }

        protected string _aliasenclosurecolumnclose;
        public virtual string AliasEnclosureColumnClose
        {
            get
            {
                if (_aliasenclosurecolumnclose == null)
                    _aliasenclosurecolumnclose = ConfigurationManager.AppSettings["sql.alias.enclosure.column.close"] ?? "]";

                return _aliasenclosurecolumnclose;
            }
        }

        protected string _aliasenclosuretableopen;
        public virtual string AliasEnclosureTableOpen
        {
            get
            {
                if (_aliasenclosuretableopen == null)
                    _aliasenclosuretableopen = ConfigurationManager.AppSettings["sql.alias.enclosure.table.open"] ?? "[";

                return _aliasenclosuretableopen;
            }
        }

        protected string _aliasenclosuretableclose;
        public virtual string AliasEnclosureTableClose
        {
            get
            {
                if (_aliasenclosuretableclose == null)
                    _aliasenclosuretableclose = ConfigurationManager.AppSettings["sql.alias.enclosure.table.close"] ?? "]";

                return _aliasenclosuretableclose;
            }
        }
    }
}
