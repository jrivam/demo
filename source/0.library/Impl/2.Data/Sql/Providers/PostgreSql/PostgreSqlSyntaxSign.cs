﻿using library.Impl.Data.Sql.Builder;
using System.Configuration;

namespace library.Impl.Data.Sql.Providers.PostgreSql
{
    public class PostgreSqlSyntaxSign : AbstractSqlSyntaxSign
    {
        public override string ParameterPrefix
        {
            get
            {
                if (_parameterprefix == null)
                    _parameterprefix = ConfigurationManager.AppSettings["postgresql.parameter.prefix"] ?? "@";

                return _parameterprefix;
            }
        }
        public override string ParameterSeparator
        {
            get
            {
                if (_parameterseparator == null)
                    _parameterseparator = ConfigurationManager.AppSettings["postgresql.parameter.separator"] ?? base.ParameterSeparator;

                return _parameterseparator;
            }
        }
        public override string ParameterAssignment
        {
            get
            {
                if (_parameterassignment == null)
                    _parameterassignment = ConfigurationManager.AppSettings["postgresql.parameter.assignment"] ?? ":=";

                return _parameterassignment;
            }
        }

        public override string AliasSeparatorColumn
        {
            get
            {
                if (_aliasseparatorcolumn == null)
                    _aliasseparatorcolumn = ConfigurationManager.AppSettings["postgresql.alias.separator.column"] ?? base.AliasSeparatorColumn;

                return _aliasseparatorcolumn;
            }
        }
        public override string AliasSeparatorColumnKeyword
        {
            get
            {
                if (_aliasseparatorcolumnkeyword == null)
                    _aliasseparatorcolumnkeyword = ConfigurationManager.AppSettings["postgresql.alias.separator.column.keyword"] ?? "as";

                return _aliasseparatorcolumnkeyword;
            }
        }
        public override string AliasSeparatorTable
        {
            get
            {
                if (_aliasseparatortable == null)
                    _aliasseparatortable = ConfigurationManager.AppSettings["postgresql.alias.separator.table"] ?? base.AliasSeparatorTable;

                return _aliasseparatortable;
            }
        }
        public override string AliasSeparatorTableKeyword
        {
            get
            {
                if (_aliasseparatortablekeyword == null)
                    _aliasseparatortablekeyword = ConfigurationManager.AppSettings["postgresql.alias.separator.table.keyword"] ?? "as";

                return _aliasseparatortablekeyword;
            }
        }

        public override string AliasEnclosureColumnOpen
        {
            get
            {
                if (_aliasenclosurecolumnopen == null)
                    _aliasenclosurecolumnopen = ConfigurationManager.AppSettings["postgresql.alias.enclosure.column.open"] ?? "\"";

                return _aliasenclosurecolumnopen;
            }
        }
        public override string AliasEnclosureColumnClose
        {
            get
            {
                if (_aliasenclosurecolumnclose == null)
                    _aliasenclosurecolumnclose = ConfigurationManager.AppSettings["postgresql.alias.enclosure.column.close"] ?? "\"";

                return _aliasenclosurecolumnclose;
            }
        }
        public override string AliasEnclosureTableOpen
        {
            get
            {
                if (_aliasenclosuretableopen == null)
                    _aliasenclosuretableopen = ConfigurationManager.AppSettings["postgresql.alias.enclosure.table.open"] ?? "";

                return _aliasenclosuretableopen;
            }
        }
        public override string AliasEnclosureTableClose
        {
            get
            {
                if (_aliasenclosuretableclose == null)
                    _aliasenclosuretableclose = ConfigurationManager.AppSettings["postgresql.alias.enclosure.table.close"] ?? "";

                return _aliasenclosuretableclose;
            }
        }

        public override string GetOperator(WhereOperator whereoperator)
        {
            var sign = string.Empty;

            switch (whereoperator)
            {
                case WhereOperator.NotEquals:
                case WhereOperator.Equals:
                    sign = "=";
                    break;
                case WhereOperator.NotGreater:
                case WhereOperator.Greater:
                    sign = ">";
                    break;
                case WhereOperator.NotLess:
                case WhereOperator.Less:
                    sign = "<";
                    break;
                case WhereOperator.EqualOrGreater:
                    sign = ">=";
                    break;
                case WhereOperator.EqualOrLess:
                    sign = "<=";
                    break;
                case WhereOperator.NotLikeBegin:
                case WhereOperator.NotLikeEnd:
                case WhereOperator.NotLike:
                case WhereOperator.LikeBegin:
                case WhereOperator.LikeEnd:
                case WhereOperator.Like:
                    sign = "like";
                    break;
                default:
                    break;
            }

            return sign;
        }

        public override string WhereWildcardSingle
        {
            get
            {
                if (_wherewildcardsingle == null)
                    _wherewildcardsingle = ConfigurationManager.AppSettings["postgresql.where.wildcard.single"] ?? "_";

                return _wherewildcardsingle;
            }
        }

        public override string WhereWildcardAny
        {
            get
            {
                if (_wherewildcardany == null)
                    _wherewildcardany = ConfigurationManager.AppSettings["postgresql.where.wildcard.any"] ?? "%";

                return _wherewildcardany;
            }
        }
    }
}
