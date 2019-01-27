using library.Impl.Data.Sql.Builder;
using System;
using System.Configuration;

namespace library.Impl.Data.Sql.Providers.SqlServer
{
    public class SqlServerSyntaxSign : AbstractSqlSyntaxSign
    {
        public override string ProviderName
        {
            get
            {
                return "sqlserver";
            }
        }

        public override string ParameterPrefix
        {
            get
            {
                if (_parameterprefix == null)
                    _parameterprefix = ConfigurationManager.AppSettings[$"{ProviderName}.parameter.prefix"] ?? "@";

                return _parameterprefix;
            }
        }
        public override string ParameterSeparator
        {
            get
            {
                if (_parameterseparator == null)
                    _parameterseparator = ConfigurationManager.AppSettings[$"{ProviderName}.parameter.separator"] ?? base.ParameterSeparator;

                return _parameterseparator;
            }
        }
        public override string ParameterAssignment
        {
            get
            {
                if (_parameterassignment == null)
                    _parameterassignment = ConfigurationManager.AppSettings[$"{ProviderName}.parameter.assignment"] ?? "=";

                return _parameterassignment;
            }
        }

        public override string AliasSeparatorColumn
        {
            get
            {
                if (_aliasseparatorcolumn == null)
                    _aliasseparatorcolumn = ConfigurationManager.AppSettings[$"{ProviderName}.alias.separator.column"] ?? base.AliasSeparatorColumn;

                return _aliasseparatorcolumn;
            }
        }
        public override string AliasSeparatorColumnKeyword
        {
            get
            {
                if (_aliasseparatorcolumnkeyword == null)
                    _aliasseparatorcolumnkeyword = ConfigurationManager.AppSettings[$"{ProviderName}.alias.separator.column.keyword"] ?? "as";

                return _aliasseparatorcolumnkeyword;
            }
        }
        public override string AliasSeparatorTable
        {
            get
            {
                if (_aliasseparatortable == null)
                    _aliasseparatortable = ConfigurationManager.AppSettings[$"{ProviderName}.alias.separator.table"] ?? base.AliasSeparatorTable;

                return _aliasseparatortable;
            }
        }
        public override string AliasSeparatorTableKeyword
        {
            get
            {
                if (_aliasseparatortablekeyword == null)
                    _aliasseparatortablekeyword = ConfigurationManager.AppSettings[$"{ProviderName}.alias.separator.table.keyword"] ?? "as";

                return _aliasseparatortablekeyword;
            }
        }

        public override string AliasEnclosureColumnOpen
        {
            get
            {
                if (_aliasenclosurecolumnopen == null)
                    _aliasenclosurecolumnopen = ConfigurationManager.AppSettings[$"{ProviderName}.alias.enclosure.column.open"] ?? "[";

                return _aliasenclosurecolumnopen;
            }
        }
        public override string AliasEnclosureColumnClose
        {
            get
            {
                if (_aliasenclosurecolumnclose == null)
                    _aliasenclosurecolumnclose = ConfigurationManager.AppSettings[$"{ProviderName}.alias.enclosure.column.close"] ?? "]";

                return _aliasenclosurecolumnclose;
            }
        }
        public override string AliasEnclosureTableOpen
        {
            get
            {
                if (_aliasenclosuretableopen == null)
                    _aliasenclosuretableopen = ConfigurationManager.AppSettings[$"{ProviderName}.alias.enclosure.table.open"] ?? "[";

                return _aliasenclosuretableopen;
            }
        }
        public override string AliasEnclosureTableClose
        {
            get
            {
                if (_aliasenclosuretableclose == null)
                    _aliasenclosuretableclose = ConfigurationManager.AppSettings[$"{ProviderName}.alias.enclosure.table.close"] ?? "]";

                return _aliasenclosuretableclose;
            }
        }

        public override bool UpdateWhereUseAlias
        {
            get
            {
                var updatewhereusealias = ConfigurationManager.AppSettings[$"{ProviderName}.update.where.use.alias"];
                _updatewhereusealias = string.IsNullOrWhiteSpace(updatewhereusealias) ? true : Convert.ToBoolean(updatewhereusealias);

                return _updatewhereusealias;
            }
        }

        public override string GetOperator(WhereOperator? whereoperator)
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
                    _wherewildcardsingle = ConfigurationManager.AppSettings[$"{ProviderName}.where.wildcard.single"] ?? "_";

                return _wherewildcardsingle;
            }
        }

        public override string WhereWildcardAny
        {
            get
            {
                if (_wherewildcardany == null)
                    _wherewildcardany = ConfigurationManager.AppSettings[$"{ProviderName}.where.wildcard.any"] ?? "%";

                return _wherewildcardany;
            }
        }
    }
}
