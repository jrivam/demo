namespace library.Impl.Data
{
    public enum WhereOperator
    {
        Not = 1,
        Equals = 2,
        Greater = 4,
        Less = 8,
        LikeBegin = 16,
        LikeEnd = 32,
        EqualOrGreater = Equals | Greater,
        EqualOrLess = Equals | Less,
        Like = LikeBegin | LikeEnd,
        NotEquals = Not | Equals,
        NotGreater = Not | Greater,
        NotLess = Not | Less,
        NotLikeBegin = Not | LikeBegin,
        NotLikeEnd = Not | LikeEnd,
        NotLike = Not | Like
    }
}
