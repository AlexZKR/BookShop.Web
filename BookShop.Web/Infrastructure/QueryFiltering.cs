using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace BookShop.Web.Infrastructure;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class QueryParameterConstraintAttribute : Attribute, IActionConstraint
{
    private readonly string[] parameterNames;

    public QueryParameterConstraintAttribute(params string[] parameterNames)
    {
        this.parameterNames = parameterNames;
    }

    public bool Accept(ActionConstraintContext context)
    {
        var keys = context.RouteContext.HttpContext.Request.Query.Keys.ToArray();
        for (int i = 0; i < parameterNames.Length; i++)
        {
            if(keys[i] != parameterNames[i]) return false;
        }
        return true;
    }
    public int Order { get; }
}