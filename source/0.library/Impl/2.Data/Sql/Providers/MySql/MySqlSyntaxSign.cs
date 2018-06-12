﻿using System.Configuration;

namespace library.Impl.Data.Sql.Providers.MySql
{
    public class MySqlSyntaxSign : AbstractSqlSyntaxSign
    {
        public override string ParameterPrefix
        {
            get
            {
                if (_parameterprefix == null)
                    _parameterprefix = ConfigurationManager.AppSettings["mysql.parameter.prefix"] ?? "?";

                return _parameterprefix;
            }
        }
        public override string ParameterSeparator
        {
            get
            {
                if (_parameterseparator == null)
                    _parameterseparator = ConfigurationManager.AppSettings["mysql.parameter.separator"] ?? base.ParameterSeparator;

                return _parameterseparator;
            }
        }
        public override string ParameterAssignment
        {
            get
            {
                if (_parameterassignment == null)
                    _parameterassignment = ConfigurationManager.AppSettings["mysql.parameter.assignment"] ?? ":=";

                return _parameterassignment;
            }
        }

        public override string AliasSeparatorColumn
        {
            get
            {
                if (_aliasseparatorcolumn == null)
                    _aliasseparatorcolumn = ConfigurationManager.AppSettings["mysql.alias.separator.column"] ?? base.AliasSeparatorColumn;

                return _aliasseparatorcolumn;
            }
        }
        public override string AliasSeparatorColumnKeyword
        {
            get
            {
                if (_aliasseparatorcolumnkeyword == null)
                    _aliasseparatorcolumnkeyword = ConfigurationManager.AppSettings["mysql.alias.separator.column.keyword"] ?? "as";

                return _aliasseparatorcolumnkeyword;
            }
        }
        public override string AliasSeparatorTable
        {
            get
            {
                if (_aliasseparatortable == null)
                    _aliasseparatortable = ConfigurationManager.AppSettings["mysql.alias.separator.table"] ?? base.AliasSeparatorTable;

                return _aliasseparatortable;
            }
        }
        public override string AliasSeparatorTableKeyword
        {
            get
            {
                if (_aliasseparatortablekeyword == null)
                    _aliasseparatortablekeyword = ConfigurationManager.AppSettings["mysql.alias.separator.table.keyword"] ?? "as";

                return _aliasseparatortablekeyword;
            }
        }

        public override string AliasEnclosureColumnOpen
        {
            get
            {
                if (_aliasenclosurecolumnopen == null)
                    _aliasenclosurecolumnopen = ConfigurationManager.AppSettings["mysql.alias.enclosure.column.open"] ?? "'";

                return _aliasenclosurecolumnopen;
            }
        }
        public override string AliasEnclosureColumnClose
        {
            get
            {
                if (_aliasenclosurecolumnclose == null)
                    _aliasenclosurecolumnclose = ConfigurationManager.AppSettings["mysql.alias.enclosure.column.close"] ?? "'";

                return _aliasenclosurecolumnclose;
            }
        }
        public override string AliasEnclosureTableOpen
        {
            get
            {
                if (_aliasenclosuretableopen == null)
                    _aliasenclosuretableopen = ConfigurationManager.AppSettings["mysql.alias.enclosure.table.open"] ?? "";

                return _aliasenclosuretableopen;
            }
        }
        public override string AliasEnclosureTableClose
        {
            get
            {
                if (_aliasenclosuretableclose == null)
                    _aliasenclosuretableclose = ConfigurationManager.AppSettings["mysql.alias.enclosure.table.close"] ?? "";

                return _aliasenclosuretableclose;
            }
        }
    }
}