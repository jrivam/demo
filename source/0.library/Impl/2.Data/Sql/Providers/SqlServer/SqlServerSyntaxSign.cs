using System.Configuration;

namespace library.Impl.Data.Sql.Providers.SqlServer
{
    public class SqlServerSyntaxSign : AbstractSqlSyntaxSign
    {
        public override string ParameterPrefix
        {
            get
            {
                if (_parameterprefix == null)
                    _parameterprefix = ConfigurationManager.AppSettings["sqlserver.parameter.prefix"] ?? base.ParameterPrefix;

                return _parameterprefix;
            }
        }
        public override string ParameterSeparator
        {
            get
            {
                if (_parameterseparator == null)
                    _parameterseparator = ConfigurationManager.AppSettings["sqlserver.parameter.separator"] ?? base.ParameterSeparator;

                return _parameterseparator;
            }
        }
        public override string ParameterAssignment
        {
            get
            {
                if (_parameterassignment == null)
                    _parameterassignment = ConfigurationManager.AppSettings["sqlserver.parameter.assignment"] ?? base.ParameterAssignment;

                return _parameterassignment;
            }
        }

        public override string AliasSeparatorColumn
        {
            get
            {
                if (_aliasseparatorcolumn == null)
                    _aliasseparatorcolumn = ConfigurationManager.AppSettings["sqlserver.alias.separator.column"] ?? base.AliasSeparatorColumn;

                return _aliasseparatorcolumn;
            }
        }
        public override string AliasSeparatorColumnKeyword
        {
            get
            {
                if (_aliasseparatorcolumnkeyword == null)
                    _aliasseparatorcolumnkeyword = ConfigurationManager.AppSettings["sqlserver.alias.separator.column.keyword"] ?? base.AliasSeparatorColumnKeyword;

                return _aliasseparatorcolumnkeyword;
            }
        }
        public override string AliasSeparatorTable
        {
            get
            {
                if (_aliasseparatortable == null)
                    _aliasseparatortable = ConfigurationManager.AppSettings["sqlserver.alias.separator.table"] ?? base.AliasSeparatorTable;

                return _aliasseparatortable;
            }
        }
        public override string AliasSeparatorTableKeyword
        {
            get
            {
                if (_aliasseparatortablekeyword == null)
                    _aliasseparatortablekeyword = ConfigurationManager.AppSettings["sqlserver.alias.separator.table.keyword"] ?? base.AliasSeparatorTableKeyword;

                return _aliasseparatortablekeyword;
            }
        }

        public override string AliasEnclosureColumnOpen
        {
            get
            {
                if (_aliasenclosurecolumnopen == null)
                    _aliasenclosurecolumnopen = ConfigurationManager.AppSettings["sqlserver.alias.enclosure.column.open"] ?? base.AliasEnclosureColumnOpen;

                return _aliasenclosurecolumnopen;
            }
        }
        public override string AliasEnclosureColumnClose
        {
            get
            {
                if (_aliasenclosurecolumnclose == null)
                    _aliasenclosurecolumnclose = ConfigurationManager.AppSettings["sqlserver.alias.enclosure.column.close"] ?? base.AliasEnclosureColumnClose;

                return _aliasenclosurecolumnclose;
            }
        }
        public override string AliasEnclosureTableOpen
        {
            get
            {
                if (_aliasenclosuretableopen == null)
                    _aliasenclosuretableopen = ConfigurationManager.AppSettings["sqlserver.alias.enclosure.table.open"] ?? base.AliasEnclosureTableOpen;

                return _aliasenclosuretableopen;
            }
        }
        public override string AliasEnclosureTableClose
        {
            get
            {
                if (_aliasenclosuretableclose == null)
                    _aliasenclosuretableclose = ConfigurationManager.AppSettings["sqlserver.alias.enclosure.table.close"] ?? base.AliasEnclosureTableClose;

                return _aliasenclosuretableclose;
            }
        }
    }
}
