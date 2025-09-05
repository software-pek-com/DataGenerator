using System.Diagnostics;
using System.Linq.Expressions;

namespace DataGenerator.Core
{
  /// <summary>
  /// Gets property name using lambda expressions. This is useful for refactor-safe property name extraction.
  /// </summary>
  public static class PropertyName
  {
    /// <summary>
    /// Returns the property name for the lambda expression.
    /// </summary>
    [DebuggerNonUserCode]
    [DebuggerStepThrough]
    public static string For<T>(Expression<Func<T>>? expression)
    {
      Guard.ArgumentNotNull(expression, "expression");

      Expression body = expression.Body;

      return GetMemberName(body);
    }

    /// <summary>
    /// Returns the property name for the lambda expression.
    /// </summary>
    [DebuggerNonUserCode]
    [DebuggerStepThrough]
    public static string For<T>(Expression<Action<T>>? expression)
    {
      Guard.ArgumentNotNull(expression, nameof(expression));

      Expression body = expression.Body;

      return GetMemberName(body);
    }

    /// <summary>
    /// Returns the property name for the lambda expression.
    /// </summary>
    [DebuggerNonUserCode]
    [DebuggerStepThrough]
    public static string For<T>(Expression<Func<T, object>>? expression)
    {
      Guard.ArgumentNotNull(expression, nameof(expression));

      Expression body = expression.Body;

      return GetMemberName(body);
    }

    /// <summary>
    /// Returns the property name for the lambda expression.
    /// </summary>
    [DebuggerNonUserCode]
    [DebuggerStepThrough]
    public static string For<TInput, TResult>(Expression<Func<TInput, TResult>>? expression)
    {
      Guard.ArgumentNotNull(expression, nameof(expression));

      Expression body = expression.Body;

      return GetMemberName(body);
    }

    /// <summary>
    /// Returns the property name for the lambda expression.
    /// </summary>
    [DebuggerNonUserCode]
    [DebuggerStepThrough]
    public static string For(Expression<Func<object>>? expression)
    {
      Guard.ArgumentNotNull(expression, nameof(expression));

      Expression body = expression.Body;

      return GetMemberName(body);
    }

    /// <summary>
    /// Returns the unqualified property name from the lambda expression.
    /// </summary>
    [DebuggerNonUserCode]
    [DebuggerStepThrough]
    public static string From<T>(Expression<Func<T, object>>? expression)
    {
      Guard.ArgumentNotNull(expression, nameof(expression));

      Expression body = expression.Body;

      return GetUnqualifiedMemberName(body);
    }

    /// <summary>
    /// Returns the unqualified property name from the lambda expression.
    /// </summary>
    [DebuggerNonUserCode]
    [DebuggerStepThrough]
    public static string From(Expression<Func<object>>? expression)
    {
      Guard.ArgumentNotNull(expression, nameof(expression));

      Expression body = expression.Body;

      return GetUnqualifiedMemberName(body);
    }

    /// <summary>
    /// Returns the property name for the lambda expression.
    /// </summary>
    [DebuggerNonUserCode]
    [DebuggerStepThrough]
    public static string GetMemberName(Expression? expression)
    {
      Guard.ArgumentNotNull(expression, nameof(expression));

      if (expression is MemberExpression memberExpression)
      {
        if (memberExpression.Expression?.NodeType == ExpressionType.MemberAccess)
        {
          return GetMemberName(memberExpression.Expression)
              + "."
              + memberExpression.Member.Name;
        }

        return memberExpression.Member.Name;
      }

      if (expression is UnaryExpression unaryExpression)
      {
        if (unaryExpression.NodeType != ExpressionType.Convert)
        {
          throw new InvalidOperationException($"Cannot interpret member from '{expression}'.");
        }

        return GetMemberName(unaryExpression.Operand);
      }

      throw new InvalidOperationException($"Could not determine member from '{expression}'.");
    }

    /// <summary>
    /// Returns the property short name for the lambda expression.
    /// </summary>
    [DebuggerNonUserCode]
    [DebuggerStepThrough]
    public static string GetUnqualifiedMemberName<T>(Expression<Func<T>>? expression)
    {
      Guard.ArgumentNotNull(expression, nameof(expression));

      Expression body = expression.Body;

      return GetUnqualifiedMemberName(body);
    }

    /// <summary>
    /// Returns the property short name for the lambda expression.
    /// </summary>
    [DebuggerNonUserCode]
    [DebuggerStepThrough]
    public static string GetUnqualifiedMemberName(Expression? expression)
    {
      Guard.ArgumentNotNull(expression, nameof(expression));

      if (expression is MemberExpression memberExpression)
      {
        return memberExpression.Member.Name;
      }

      if (expression is UnaryExpression unaryExpression)
      {
        if (unaryExpression.NodeType != ExpressionType.Convert)
        {
          throw new InvalidOperationException($"Cannot interpret member from '{expression}'.");
        }

        return GetUnqualifiedMemberName(unaryExpression.Operand);
      }

      throw new InvalidOperationException($"Could not determine member from '{expression}'");
    }
  }
}
