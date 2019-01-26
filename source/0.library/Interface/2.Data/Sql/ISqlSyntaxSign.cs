using library.Impl.Data.Sql;

namespace library.Interface.Data.Sql
{
    public interface ISqlSyntaxSign
    {
        string ProviderName { get; }

        string ParameterPrefix { get; }
        string ParameterSeparator { get; }
        string ParameterAssignment { get; }

        string AliasSeparatorColumn { get; }
        string AliasSeparatorColumnKeyword { get; }
        string AliasSeparatorTable { get; }
        string AliasSeparatorTableKeyword { get; }
        string AliasEnclosureColumnOpen { get; }
        string AliasEnclosureColumnClose { get; }
        string AliasEnclosureTableOpen { get; }
        string AliasEnclosureTableClose { get; }

        bool UpdateSetUseAlias { get; }
        bool UpdateWhereUseAlias { get; }

        string GetOperator(WhereOperator? whereoperator);

        string WhereWildcardSingle { get; }
        string WhereWildcardAny { get; }
    }
}
